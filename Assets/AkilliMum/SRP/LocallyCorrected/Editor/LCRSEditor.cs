﻿using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor.Rendering.Universal;
using UnityEditor;
using UnityEditor.Rendering.Universal.ShaderGUI;

namespace AkilliMum.SRP.LCRS
{
    internal class LCRSEditor : BaseShaderGUI
    {
        // Properties
        private LitGUI.LitProperties litProperties;

        private bool MenuRotation = true;
        
        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            Material targetMat = materialEditor.target as Material;


            //MaterialProperty _EnviCubeMapBaked = ShaderGUI.FindProperty("_EnviCubeMapBaked", properties);
            //materialEditor.ShaderProperty(_EnviCubeMapBaked, "Custom Cube Map");

            MaterialProperty _EnableRotation = ShaderGUI.FindProperty("_EnableRotation", properties);

            MenuRotation = EditorGUILayout.BeginFoldoutHeaderGroup(MenuRotation, new GUIContent { text = "Rotation" });

            bool enableRotation = false;
            if (MenuRotation)
            {
                enableRotation = _EnableRotation.floatValue > 0.5f;
                enableRotation = EditorGUILayout.Toggle("Enable Rotation", enableRotation);
                _EnableRotation.floatValue = enableRotation ? 1.0f : 0.0f;

                if (enableRotation)
                {
                    MaterialProperty rotation = ShaderGUI.FindProperty("_EnviRotation", properties);
                    materialEditor.ShaderProperty(rotation, "Rotation");

                    MaterialProperty position = ShaderGUI.FindProperty("_EnviPosition", properties);
                    materialEditor.ShaderProperty(position, "Position Correction");

                }
            }

            EditorGUILayout.EndFoldoutHeaderGroup();

            if (enableRotation)
                targetMat.EnableKeyword("_LCRS_PROBE_ROTATION");
            else
                targetMat.DisableKeyword("_LCRS_PROBE_ROTATION");

            // render the default gui
            base.OnGUI(materialEditor, properties);
        }

        // collect properties from the material properties
        public override void FindProperties(MaterialProperty[] properties)
        {
            base.FindProperties(properties);
            litProperties = new LitGUI.LitProperties(properties);
        }

        // material changed check
        [Obsolete]
        public override void MaterialChanged(Material material)
        {
            if (material == null)
                throw new ArgumentNullException("material");

            SetMaterialKeywords(material, LitGUI.SetMaterialKeywords);
        }

        // material main surface options
        public override void DrawSurfaceOptions(Material material)
        {
            if (material == null)
                throw new ArgumentNullException("material");

            // Use default labelWidth
            EditorGUIUtility.labelWidth = 0f;

            // Detect any changes to the material
            EditorGUI.BeginChangeCheck();
            if (litProperties.workflowMode != null)
            {
                DoPopup(LitGUI.Styles.workflowModeText, litProperties.workflowMode, Enum.GetNames(typeof(LitGUI.WorkflowMode)));
            }
            //if (EditorGUI.EndChangeCheck())
            //{
            //    foreach (var obj in blendModeProp.targets)
            //        MaterialChanged((Material)obj);
            //}
            base.DrawSurfaceOptions(material);
        }

        // material main surface inputs
        public override void DrawSurfaceInputs(Material material)
        {
            base.DrawSurfaceInputs(material);
            LitGUI.Inputs(litProperties, materialEditor, material);
            DrawEmissionProperties(material, true);
            DrawTileOffset(materialEditor, baseMapProp);
        }

        // material main advanced options
        public override void DrawAdvancedOptions(Material material)
        {
            if (litProperties.reflections != null && litProperties.highlights != null)
            {
                EditorGUI.BeginChangeCheck();
                materialEditor.ShaderProperty(litProperties.highlights, LitGUI.Styles.highlightsText);
                materialEditor.ShaderProperty(litProperties.reflections, LitGUI.Styles.reflectionsText);
                //if (EditorGUI.EndChangeCheck())
                //{
                //    MaterialChanged(material);
                //}
            }

            base.DrawAdvancedOptions(material);
        }

        public override void AssignNewShaderToMaterial(Material material, Shader oldShader, Shader newShader)
        {
            if (material == null)
                throw new ArgumentNullException("material");

            // _Emission property is lost after assigning Standard shader to the material
            // thus transfer it before assigning the new shader
            if (material.HasProperty("_Emission"))
            {
                material.SetColor("_EmissionColor", material.GetColor("_Emission"));
            }

            base.AssignNewShaderToMaterial(material, oldShader, newShader);

            if (oldShader == null || !oldShader.name.Contains("Legacy Shaders/"))
            {
                SetupMaterialBlendMode(material);
                return;
            }

            SurfaceType surfaceType = SurfaceType.Opaque;
            BlendMode blendMode = BlendMode.Alpha;
            if (oldShader.name.Contains("/Transparent/Cutout/"))
            {
                surfaceType = SurfaceType.Opaque;
                material.SetFloat("_AlphaClip", 1);
            }
            else if (oldShader.name.Contains("/Transparent/"))
            {
                // NOTE: legacy shaders did not provide physically based transparency
                // therefore Fade mode
                surfaceType = SurfaceType.Transparent;
                blendMode = BlendMode.Alpha;
            }
            material.SetFloat("_Surface", (float)surfaceType);
            material.SetFloat("_Blend", (float)blendMode);

            if (oldShader.name.Equals("Standard (Specular setup)"))
            {
                material.SetFloat("_WorkflowMode", (float)LitGUI.WorkflowMode.Specular);
                Texture texture = material.GetTexture("_SpecGlossMap");
                if (texture != null)
                    material.SetTexture("_MetallicSpecGlossMap", texture);
            }
            else
            {
                material.SetFloat("_WorkflowMode", (float)LitGUI.WorkflowMode.Metallic);
                Texture texture = material.GetTexture("_MetallicGlossMap");
                if (texture != null)
                    material.SetTexture("_MetallicSpecGlossMap", texture);
            }

            //MaterialChanged(material);
        }
    }
}
