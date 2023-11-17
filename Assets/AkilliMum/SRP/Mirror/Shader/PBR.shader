﻿Shader "AkilliMum/URP/Mirrors/PBR"
{
    Properties
    {
        // Specular vs Metallic workflow
        _WorkflowMode("WorkflowMode", Float) = 1.0

        [MainTexture] _BaseMap("Albedo", 2D) = "white" {}
        [MainColor] _BaseColor("Color", Color) = (1,1,1,1)
			 
        _Cutoff("Alpha Cutoff", Range(0.0, 1.0)) = 0.5

        _Smoothness("Smoothness", Range(0.0, 1.0)) = 0.5
        _SmoothnessTextureChannel("Smoothness texture channel", Float) = 0
			 
        _Metallic("Metallic", Range(0.0, 1.0)) = 0.0
        _MetallicGlossMap("Metallic", 2D) = "white" {}

        _SpecColor("Specular", Color) = (0.2, 0.2, 0.2)
        _SpecGlossMap("Specular", 2D) = "white" {}

        [ToggleOff] _SpecularHighlights("Specular Highlights", Float) = 1.0
        [ToggleOff] _EnvironmentReflections("Environment Reflections", Float) = 1.0

        _BumpScale("Scale", Float) = 1.0
        _BumpMap("Normal Map", 2D) = "bump" {}

        _Parallax("Scale", Range(0.005, 0.08)) = 0.005
        _ParallaxMap("Height Map", 2D) = "black" {}

        _OcclusionStrength("Strength", Range(0.0, 1.0)) = 1.0
        _OcclusionMap("Occlusion", 2D) = "white" {}

        [HDR] _EmissionColor("Color", Color) = (0,0,0)
        _EmissionMap("Emission", 2D) = "white" {}

        _DetailMask("Detail Mask", 2D) = "white" {}
        _DetailAlbedoMapScale("Scale", Range(0.0, 2.0)) = 1.0
        _DetailAlbedoMap("Detail Albedo x2", 2D) = "linearGrey" {}
        _DetailNormalMapScale("Scale", Range(0.0, 2.0)) = 1.0
        [Normal] _DetailNormalMap("Normal Map", 2D) = "bump" {}

        [ToggleUI] _ClearCoat("Clear Coat", Float) = 0.0
        _ClearCoatMap("Clear Coat Map", 2D) = "white" {}
        _ClearCoatMask("Clear Coat Mask", Range(0.0, 1.0)) = 0.0
        _ClearCoatSmoothness("Clear Coat Smoothness", Range(0.0, 1.0)) = 1.0

        // Blending state
        _Surface("__surface", Float) = 0.0
        _Blend("__blend", Float) = 0.0
        _Cull("__cull", Float) = 2.0
        [ToggleUI] _AlphaClip("__clip", Float) = 0.0
        [HideInInspector] _SrcBlend("__src", Float) = 1.0
        [HideInInspector] _DstBlend("__dst", Float) = 0.0
        [HideInInspector] _SrcBlendAlpha("__srcA", Float) = 1.0
        [HideInInspector] _DstBlendAlpha("__dstA", Float) = 0.0
        [HideInInspector] _ZWrite("__zw", Float) = 1.0
        [HideInInspector] _BlendModePreserveSpecular("_BlendModePreserveSpecular", Float) = 1.0
        [HideInInspector] _AlphaToMask("__alphaToMask", Float) = 0.0

        [ToggleUI] _ReceiveShadows("Receive Shadows", Float) = 1.0
        // Editmode props
        _QueueOffset("Queue offset", Float) = 0.0

        // ObsoleteProperties
        [HideInInspector] _MainTex("BaseMap", 2D) = "white" {}
        [HideInInspector] _Color("Base Color", Color) = (1, 1, 1, 1)
        [HideInInspector] _GlossMapScale("Smoothness", Float) = 0.0
        [HideInInspector] _Glossiness("Smoothness", Float) = 0.0
        [HideInInspector] _GlossyReflections("EnvironmentReflections", Float) = 0.0

        [HideInInspector][NoScaleOffset]unity_Lightmaps("unity_Lightmaps", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_LightmapsInd("unity_LightmapsInd", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_ShadowMasks("unity_ShadowMasks", 2DArray) = "" {}



        //new values
		[HideInInspector]_UV0TileOffset("UV0 Tile and Offset", Vector) = (1,1,0,0)
		[HideInInspector]_UV1TileOffset("UV1 Tile and Offset", Vector) = (1,1,0,0)
		[HideInInspector]_UV2TileOffset("UV2 Tile and Offset", Vector) = (1,1,0,0)
		[HideInInspector]_UV3TileOffset("UV3 Tile and Offset", Vector) = (1,1,0,0)

		[Enum(UV0, 0, UV1, 1, UV2, 2, UV3, 3)] _BaseMapUV("", Float) = 0.0
		[Enum(UV0, 0, UV1, 1, UV2, 2, UV3, 3)] _SpecularUV("", Float) = 0.0
		[Enum(UV0, 0, UV1, 1, UV2, 2, UV3, 3)] _MetallicUV("", Float) = 0.0
		[Enum(UV0, 0, UV1, 1, UV2, 2, UV3, 3)] _NormalUV("", Float) = 0.0
		[Enum(UV0, 0, UV1, 1, UV2, 2, UV3, 3)] _ParallaxUV("", Float) = 0.0
		[Enum(UV0, 0, UV1, 1, UV2, 2, UV3, 3)] _OcclusionUV("", Float) = 0.0
		[Enum(UV0, 0, UV1, 1, UV2, 2, UV3, 3)] _ClearCoatUV("", Float) = 0.0
		[Enum(UV0, 0, UV1, 1, UV2, 2, UV3, 3)] _EmissionUV("", Float) = 0.0

		[Toggle(O_REFLECTION_PROBE_BLENDING)] O_REFLECTION_PROBE_BLENDING("REFLECTION_PROBE_BLENDING", Float) = 0
		[Toggle(O_REFLECTION_PROBE_BOX_PROJECTION)] O_REFLECTION_PROBE_BOX_PROJECTION("_REFLECTION_PROBE_BOX_PROJECTION", Float) = 0
		[Toggle(O_LIGHT_COOKIES)] O_LIGHT_COOKIES("_LIGHT_COOKIES", Float) = 0
		//[Toggle(O_WRITE_RENDERING_LAYERS)] O_WRITE_RENDERING_LAYERS("_WRITE_RENDERING_LAYERS", Float) = 1
		[Toggle(O_LOD_FADE_CROSSFADE)] O_LOD_FADE_CROSSFADE("LOD_FADE_CROSSFADE", Float) = 0
		[Toggle(O_DEBUG_DISPLAY)] O_DEBUG_DISPLAY("DEBUG_DISPLAY", Float) = 0
		////[Toggle(O_LIGHTMAP_ON)] O_LIGHTMAP_ON("LIGHTMAP_ON", Float) = 1
		//[Toggle(O_DYNAMICLIGHTMAP_ON)] O_DYNAMICLIGHTMAP_ON("DYNAMICLIGHTMAP_ON", Float) = 0
		////[Toggle(O_LIGHT_LAYERS)] O_LIGHT_LAYERS("LIGHT_LAYERS", Float) = 1
		//[Toggle(O_LIGHTMAP_SHADOW_MIXING)] O_LIGHTMAP_SHADOW_MIXING("LIGHTMAP_SHADOW_MIXING", Float) = 1
		//[Toggle(O_SHADOWS_SHADOWMASK)] O_SHADOWS_SHADOWMASK("SHADOWS_SHADOWMASK", Float) = 1
		//[Toggle(O_DIRLIGHTMAP_COMBINED)] O_DIRLIGHTMAP_COMBINED("DIRLIGHTMAP_COMBINED", Float) = 0
		////[Toggle(O_DOTS_INSTANCING_ON)] O_DOTS_INSTANCING_ON("DOTS_INSTANCING_ON", Float) = 1
		////[Toggle(O_ADDITIONAL_LIGHT_SHADOWS)] O_ADDITIONAL_LIGHT_SHADOWS("ADDITIONAL_LIGHT_SHADOWS", Float) = 1
		//[Toggle(O_SHADOWS_SOFT)] O_SHADOWS_SOFT("SHADOWS_SOFT", Float) = 1
		////[Toggle(O_EMISSION)] O_EMISSION("EMISSION", Float) = 0

        [HideInInspector]_IsMultiPass("IsMultiPass Mobile", Float) = 0.

        [HideInInspector]_LeftOrCenterTexture("Left or Center Texture", 2D) = "white" { } //left or all
        [HideInInspector]_RightTexture("Right Texture", 2D) = "white" { } //right
        [HideInInspector]_ReflectionIntensity("Reflection Intensity", Range(0.0, 1.0)) = 0.5
        [HideInInspector]_DisableGI("Disable GI", Float) = 0.
        [HideInInspector]_UseFresnel("Fresnel Like Reflection", Float) = 0.
        //[HideInInspector]_UseFresnelPower("Fresnel Like Reflection Power", Range(0.0, 1.0)) = 0.2
        [HideInInspector]_LODLevel("Mip Level", Range(0, 10)) = 0
        //[HideInInspector]_WetLevel("WetLevel", Float) = 0.
        [HideInInspector]_WorkType("Work Type", Float) = 1.
        [HideInInspector]_DeviceType("Device Type", Float) = 1.
        [HideInInspector]_MixBlackColor("Mix Black Color", Float) = 0.
        [HideInInspector][Enum(UV0, 0, UV1, 1, UV2, 2, UV3, 3, None, 99)] _ClipUV("Clip UV", int) = 99
        [HideInInspector][Enum(None, 0, Left, 1, Right, 2)] _ClipEye("Clip Eye", int) = 0
        [HideInInspector]_ClipPercentage("Clipping Percentage", int) = 20

        //[HideInInspector]_EnableRefraction("Enable Refraction", Float) = 0.
        [HideInInspector]_RefractionTex("Refraction", 2D) = "bump" {}
        [HideInInspector]_ReflectionRefraction("Reflection Refraction", Float) = 0.0
        
        [HideInInspector]_EnableDepthBlur("Enable Depth Blur", Float) = -1.0
        
        [HideInInspector]_EnableSimpleDepth("Enable Simple Depth", Float) = -1.0
        [HideInInspector]_SimpleDepthCutoff("Simple Depth Cutoff", Range(0.0, 50.0)) = 0.5
        //[HideInInspector]_DepthBlur("Depth Blur", Float) = 1
        [HideInInspector]_LeftOrCenterDepthTexture("Left or Center Depth Texture", 2D) = "white" { } //left or all
        [HideInInspector]_RightDepthTexture("Right Depth Texture", 2D) = "white" { } //right
        [HideInInspector]_NearClip("Near Clip", Float) = 0.3
        [HideInInspector]_FarClip("Far Clip", Float) = 1000
        
        //[HideInInspector]_EnableMask("Enable Mask", Float) = -1.0
        [HideInInspector]_MaskTex("Mask", 2D) = "white" {}
        [HideInInspector]_MaskCutoff("Mask Cutoff", Range(0.0, 1.0)) = 0.5
        [HideInInspector]_MaskEdgeDarkness("Mask Edge Darkness", Range(1.0, 50.0)) = 1.
        [HideInInspector]_MaskTiling("Mask Tiling", Vector) = (1,1,1,1)
        
        //[HideInInspector]_EnableWave("Enable Waves", Float) = -1.0
        [HideInInspector]_WaveNoiseTex("Wave Noise Tex", 2D) = "white" {}
        [HideInInspector]_WaveSize("Wave Size", float) = 12.0
        [HideInInspector]_WaveDistortion("Wave Distortion", Float) = 0.02
        [HideInInspector]_WaveSpeed("Wave Speed", Float) = 3.0
        
        //[HideInInspector]_EnableRipple("Enable Ripples", Float) = -1.0
        [HideInInspector]_RippleTex("Ripple", 2D) = "bump" {}
        [HideInInspector]_RippleSize("Ripple Size", Float) = 2.0
        [HideInInspector]_RippleRefraction("Ripple Refraction", Float) = 0.02
        [HideInInspector]_RippleDensity("Ripple Density", Float) = 1.0
        [HideInInspector]_RippleSpeed("Ripple Speed", Float) = 0.3

        [HideInInspector]_EnableLocallyCorrection("Enable Locally Correction", Float) = 0.
        [HideInInspector]_BBoxMin("BBox Min", Vector) = (0,0,0,0)
        [HideInInspector]_BBoxMax("BBox Max", Vector) = (0,0,0,0)
        [HideInInspector]_EnviCubeMapPos("CubeMap Pos", Vector) = (0,0,0,0)
        [HideInInspector]_EnableRotation("Enable Rotation", Float) = 0
        [HideInInspector]_EnviRotation("Environment Rotation", Vector) = (0,0,0,0)
        [HideInInspector]_EnviPosition("Environment Position", Vector) = (0,0,0,0)

        //[Header(Stencil Masking)]
		// https://docs.unity3d.com/ScriptReference/Rendering.CompareFunction.html
		_StencilRef("_StencilRef", Float) = 0
		//[Enum(UnityEngine.Rendering.CompareFunction)]_StencilComp("_StencilComp (default = Disable) _____Set to NotEqual if you want to mask by specific _StencilRef value, else set to Disable", Float) = 0 //0 = disable
        //[Header(Blending)]
        // https://docs.unity3d.com/ScriptReference/Rendering.BlendMode.html
        [Enum(UnityEngine.Rendering.BlendMode)] _SrcBlendEx("_SrcBlendEx (default = SrcAlpha)", Float) = 5 // 5 = SrcAlpha
        [Enum(UnityEngine.Rendering.BlendMode)]_DstBlendEx("_DstBlendEx (default = OneMinusSrcAlpha)", Float) = 10 // 10 = OneMinusSrcAlpha
        //[Header(ZTest)]
        // https://docs.unity3d.com/ScriptReference/Rendering.CompareFunction.html
        // default need to be Disable, because we need to make sure decal render correctly even if camera goes into decal cube volume, although disable ZTest by default will prevent EarlyZ (bad for GPU performance)
        [Enum(UnityEngine.Rendering.CompareFunction)]_ZTestEx("_ZTest (default = Disable or LessEqual", Float) = 0 //0 = disable
        //[Header(Cull)]
        // https://docs.unity3d.com/ScriptReference/Rendering.CullMode.html
        // default need to be Front, because we need to make sure decal render correctly even if camera goes into decal cube
        [Enum(UnityEngine.Rendering.CullMode)]_CullEx("_Cull (default = Back)", Float) = 2 //1 = Front, 2 Back
        [Toggle(_ZWriteEx)] _ZWriteEx("_ZWriteEx (default = On)", Float) = 1
    }

    SubShader
    {
        // Universal Pipeline tag is required. If Universal render pipeline is not set in the graphics settings
        // this Subshader will fail. One can add a subshader below or fallback to Standard built-in to make this
        // material work with both Universal Render Pipeline and Builtin Unity Pipeline
        // ComplexLit does not run with deferred
        Tags
        {
            "RenderType" = "Opaque"
            "RenderPipeline" = "UniversalPipeline"
            "UniversalMaterialType" = "Lit"
            "IgnoreProjector" = "True"
        }
        LOD 300

        Stencil
		{
			Ref[_StencilRef]  //so our shader will use this stencil, we can adjust it for decal like effects
			Comp Always
			Pass Replace
		}

         // ------------------------------------------------------------------
        //  Forward pass. Shades all light in a single pass. GI + emission + Fog
        Pass
        {
            // Lightmode matches the ShaderPassName set in UniversalRenderPipeline.cs. SRPDefaultUnlit and passes with
            // no LightMode tag are also rendered by Universal Render Pipeline
            Name "ForwardLit"
            Tags
            {
                "LightMode" = "UniversalForward"
            }

            // -------------------------------------
            // Render State Commands
            Blend[_SrcBlendEx][_DstBlendEx], [_SrcBlendAlpha][_DstBlendAlpha]
            ZWrite[_ZWriteEx]
            Cull[_CullEx]
            AlphaToMask[_AlphaToMask]

            HLSLPROGRAM
            #pragma target 2.0

            // -------------------------------------
            // Shader Stages
            #pragma vertex LitPassVertex
            #pragma fragment LitPassFragment

            //todo: a bug?
            //spot etc lights does not work on deferred if we do not put it here!!!!
            #define _ADDITIONAL_LIGHT_SHADOWS
            
            // -------------------------------------
            // Material Keywords
            #pragma shader_feature_local _NORMALMAP
            #pragma shader_feature_local _PARALLAXMAP
            #pragma shader_feature_local _RECEIVE_SHADOWS_OFF
            //#pragma shader_feature_local _ _DETAIL_MULX2 _DETAIL_SCALED
            #pragma shader_feature_local_fragment _SURFACE_TYPE_TRANSPARENT
            #pragma shader_feature_local_fragment _ALPHATEST_ON
            #pragma shader_feature_local_fragment _ _ALPHAPREMULTIPLY_ON _ALPHAMODULATE_ON
            #pragma shader_feature_local_fragment _EMISSION
            #pragma shader_feature_local_fragment _METALLICSPECGLOSSMAP
            #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
            #pragma shader_feature_local_fragment _OCCLUSIONMAP
            #pragma shader_feature_local_fragment _ _CLEARCOAT _CLEARCOATMAP
            #pragma shader_feature_local_fragment _SPECULARHIGHLIGHTS_OFF
            #pragma shader_feature_local_fragment _ENVIRONMENTREFLECTIONS_OFF
            #pragma shader_feature_local_fragment _SPECULAR_SETUP

            // -------------------------------------
            // Universal Pipeline keywords
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
            #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
            //#pragma multi_compile _ EVALUATE_SH_MIXED EVALUATE_SH_VERTEX
            #pragma multi_compile_fragment _ _ADDITIONAL_LIGHT_SHADOWS
            //#pragma multi_compile_fragment _ _REFLECTION_PROBE_BLENDING
            #pragma shader_feature_local_fragment _REFLECTION_PROBE_BLENDING
            //#pragma multi_compile_fragment _ _REFLECTION_PROBE_BOX_PROJECTION
            #pragma shader_feature_local_fragment _REFLECTION_PROBE_BOX_PROJECTION
            #pragma multi_compile_fragment _ _SHADOWS_SOFT
            #pragma multi_compile_fragment _ _SCREEN_SPACE_OCCLUSION
            #pragma multi_compile_fragment _ _DBUFFER_MRT1 _DBUFFER_MRT2 _DBUFFER_MRT3
            #pragma multi_compile_fragment _ _LIGHT_LAYERS
            //#pragma multi_compile_fragment _ _LIGHT_COOKIES
            #pragma shader_feature_local_fragment _LIGHT_COOKIES
            #pragma multi_compile _ _FORWARD_PLUS
            #pragma multi_compile_fragment _ _WRITE_RENDERING_LAYERS
            //#include_with_pragmas "../../Pipeline/ShaderLibrary/RenderingLayers.hlsl"


            // -------------------------------------
            // Unity defined keywords
            #pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
            #pragma multi_compile _ SHADOWS_SHADOWMASK
            #pragma multi_compile _ DIRLIGHTMAP_COMBINED
            #pragma multi_compile _ LIGHTMAP_ON
            #pragma multi_compile _ DYNAMICLIGHTMAP_ON
            //#pragma multi_compile_fragment _ LOD_FADE_CROSSFADE
            #pragma shader_feature_local_fragment LOD_FADE_CROSSFADE
            #pragma multi_compile_fog
            //#pragma multi_compile_fragment _ DEBUG_DISPLAY
            #pragma shader_feature_local_fragment DEBUG_DISPLAY

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing
            #pragma instancing_options renderinglayer
            #pragma multi_compile _ DOTS_INSTANCING_ON
            //#include_with_pragmas "../../Pipeline/ShaderLibrary/DOTS.hlsl"

            #define _AKMU_MIRROR

			#pragma multi_compile _ _FULLMIRROR //my custom keys
			#pragma shader_feature _LOCALLYCORRECTION
			//#pragma shader_feature _FULLWATER
			#pragma multi_compile _ _AKMU_MIRROR_REFRACTION
			#pragma multi_compile _ _AKMU_MIRROR_MASK
			#pragma shader_feature _AKMU_MIRROR_RIPPLE
			#pragma multi_compile _ _AKMU_MIRROR_WAVE

            // -------------------------------------
            // Includes
            #include "../../Pipeline/Shaders/LitInput.hlsl"
            #include "../../Pipeline/Shaders/LitForwardPass.hlsl"
            ENDHLSL
        }

        Pass
        {
            Name "ShadowCaster"
            Tags
            {
                "LightMode" = "ShadowCaster"
            }

            // -------------------------------------
            // Render State Commands
            ZWrite On
            ZTest LEqual
            ColorMask 0
            Cull[_CullEx]

            HLSLPROGRAM
            #pragma target 2.0

            // -------------------------------------
            // Shader Stages
            #pragma vertex ShadowPassVertex
            #pragma fragment ShadowPassFragment

            // -------------------------------------
            // Material Keywords
            #pragma shader_feature_local_fragment _ALPHATEST_ON
            #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing
            #pragma multi_compile _ DOTS_INSTANCING_ON
            //#include_with_pragmas "../../Pipeline/ShaderLibrary/DOTS.hlsl"

            // -------------------------------------
            // Universal Pipeline keywords

            // -------------------------------------
            // Unity defined keywords
            //#pragma multi_compile_fragment _ LOD_FADE_CROSSFADE
            #pragma shader_feature_local_fragment LOD_FADE_CROSSFADE

            // This is used during shadow map generation to differentiate between directional and punctual light shadows, as they use different formulas to apply Normal Bias
            #pragma multi_compile_vertex _ _CASTING_PUNCTUAL_LIGHT_SHADOW

            #define _AKMU_MIRROR

            // -------------------------------------
            // Includes
            #include "../../Pipeline/Shaders/LitInput.hlsl"
            #include "../../Pipeline/Shaders/ShadowCasterPass.hlsl"
            ENDHLSL
        }

        Pass
        {
            // Lightmode matches the ShaderPassName set in UniversalRenderPipeline.cs. SRPDefaultUnlit and passes with
            // no LightMode tag are also rendered by Universal Render Pipeline
            Name "GBuffer"
            Tags
            {
                "LightMode" = "UniversalGBuffer"
            }

            // -------------------------------------
            // Render State Commands
            ZWrite[_ZWriteEx]
            ZTest LEqual
            Cull[_CullEx]

            HLSLPROGRAM
            #pragma target 4.5

            // Deferred Rendering Path does not support the OpenGL-based graphics API:
            // Desktop OpenGL, OpenGL ES 3.0, WebGL 2.0.
            #pragma exclude_renderers gles3 glcore

            // -------------------------------------
            // Shader Stages
            #pragma vertex LitGBufferPassVertex
            #pragma fragment LitGBufferPassFragment

            //todo: a bug?
            //spot etc lights does not work on deferred if we do not put it here!!!!
            //#define _ADDITIONAL_LIGHT_SHADOWS //commented below on gbuffer, hmm
            
            // -------------------------------------
            // Material Keywords
            #pragma shader_feature_local _NORMALMAP
            #pragma shader_feature_local_fragment _ALPHATEST_ON
            //#pragma shader_feature_local_fragment _ALPHAPREMULTIPLY_ON
            #pragma shader_feature_local_fragment _EMISSION
            #pragma shader_feature_local_fragment _METALLICSPECGLOSSMAP
            #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
            #pragma shader_feature_local_fragment _OCCLUSIONMAP
            #pragma shader_feature_local _PARALLAXMAP
            #pragma shader_feature_local _ _DETAIL_MULX2 _DETAIL_SCALED

            #pragma shader_feature_local_fragment _SPECULARHIGHLIGHTS_OFF
            #pragma shader_feature_local_fragment _ENVIRONMENTREFLECTIONS_OFF
            #pragma shader_feature_local_fragment _SPECULAR_SETUP
            #pragma shader_feature_local _RECEIVE_SHADOWS_OFF

            // -------------------------------------
            // Universal Pipeline keywords
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
            //#pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
            //#pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
            //#pragma multi_compile_fragment _ _REFLECTION_PROBE_BLENDING
            #pragma shader_feature_local_fragment _REFLECTION_PROBE_BLENDING
            //#pragma multi_compile_fragment _ _REFLECTION_PROBE_BOX_PROJECTION
            #pragma shader_feature_local_fragment _REFLECTION_PROBE_BOX_PROJECTION
            #pragma multi_compile_fragment _ _SHADOWS_SOFT
            #pragma multi_compile_fragment _ _DBUFFER_MRT1 _DBUFFER_MRT2 _DBUFFER_MRT3
            #pragma multi_compile_fragment _ _RENDER_PASS_ENABLED
            #pragma multi_compile_fragment _ _WRITE_RENDERING_LAYERS
            //#include_with_pragmas "../../Pipeline/ShaderLibrary/RenderingLayers.hlsl"

            // -------------------------------------
            // Unity defined keywords
            #pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
            #pragma multi_compile _ SHADOWS_SHADOWMASK
            #pragma multi_compile _ DIRLIGHTMAP_COMBINED
            #pragma multi_compile _ LIGHTMAP_ON
            #pragma multi_compile _ DYNAMICLIGHTMAP_ON
            //#pragma multi_compile_fragment _ LOD_FADE_CROSSFADE
            //#pragma shader_feature_local_fragment LOD_FADE_CROSSFADE
            #pragma multi_compile_fragment _ LOD_FADE_CROSSFADE
            #pragma multi_compile_fragment _ _GBUFFER_NORMALS_OCT

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing
            #pragma instancing_options renderinglayer
            //#include_with_pragmas "../../Pipeline/ShaderLibrary/DOTS.hlsl"
            #pragma multi_compile _ DOTS_INSTANCING_ON


			#define _AKMU_MIRROR

			#pragma multi_compile _ _FULLMIRROR //my custom keys
			#pragma shader_feature _LOCALLYCORRECTION
			//#pragma shader_feature _FULLWATER
			#pragma multi_compile _ _AKMU_MIRROR_REFRACTION
			#pragma multi_compile _ _AKMU_MIRROR_MASK
			#pragma shader_feature _AKMU_MIRROR_RIPPLE
			#pragma multi_compile _ _AKMU_MIRROR_WAVE

            // -------------------------------------
            // Includes
            #include "../../Pipeline/Shaders/LitInput.hlsl"
            #include "../../Pipeline/Shaders/LitGBufferPass.hlsl"
            ENDHLSL
        }

        Pass
        {
            Name "DepthOnly"
            Tags
            {
                "LightMode" = "DepthOnly"
            }

            // -------------------------------------
            // Render State Commands
            ZWrite On
            ColorMask R
            Cull[_CullEx]

            HLSLPROGRAM
            #pragma target 2.0

            // -------------------------------------
            // Shader Stages
            #pragma vertex DepthOnlyVertex
            #pragma fragment DepthOnlyFragment

            // -------------------------------------
            // Material Keywords
            #pragma shader_feature_local_fragment _ALPHATEST_ON
            #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

            // -------------------------------------
            // Unity defined keywords
            //#pragma multi_compile_fragment _ LOD_FADE_CROSSFADE
            #pragma multi_compile_fragment _ LOD_FADE_CROSSFADE
            //#pragma shader_feature_local_fragment LOD_FADE_CROSSFADE

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing
            //#include_with_pragmas "../../Pipeline/ShaderLibrary/DOTS.hlsl"
            #pragma multi_compile _ DOTS_INSTANCING_ON


            #define _AKMU_MIRROR

            // -------------------------------------
            // Includes
            #include "../../Pipeline/Shaders/LitInput.hlsl"
            #include "../../Pipeline/Shaders/DepthOnlyPass.hlsl"
            ENDHLSL
        }

        // This pass is used when drawing to a _CameraNormalsTexture texture
        Pass
        {
            Name "DepthNormals"
            Tags
            {
                "LightMode" = "DepthNormals"
            }

            // -------------------------------------
            // Render State Commands
            ZWrite On
            Cull[_CullEx]

            HLSLPROGRAM
            #pragma target 2.0

            // -------------------------------------
            // Shader Stages
            #pragma vertex DepthNormalsVertex
            #pragma fragment DepthNormalsFragment

            //todo: a bug?
            //spot etc lights does not work on deferred if we do not put it here!!!!
            //#define _ADDITIONAL_LIGHT_SHADOWS
            
            // -------------------------------------
            // Material Keywords
            #pragma shader_feature_local _NORMALMAP
            #pragma shader_feature_local _PARALLAXMAP
            #pragma shader_feature_local _ _DETAIL_MULX2 _DETAIL_SCALED
            #pragma shader_feature_local_fragment _ALPHATEST_ON
            #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A

            // -------------------------------------
            // Unity defined keywords
            //#pragma multi_compile_fragment _ LOD_FADE_CROSSFADE
            #pragma multi_compile _ DOTS_INSTANCING_ON
            //#pragma shader_feature_local_fragment LOD_FADE_CROSSFADE

            // -------------------------------------
            // Universal Pipeline keywords
            #pragma multi_compile_fragment _ _WRITE_RENDERING_LAYERS
            //#include_with_pragmas "../../Pipeline/ShaderLibrary/RenderingLayers.hlsl"

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing
            #pragma multi_compile _ DOTS_INSTANCING_ON
            //#include_with_pragmas "../../Pipeline/ShaderLibrary/DOTS.hlsl"


            #define _AKMU_MIRROR

            #pragma multi_compile _ _FULLMIRROR //my custom keys
			#pragma shader_feature _LOCALLYCORRECTION
			//#pragma shader_feature _FULLWATER
			#pragma multi_compile _ _AKMU_MIRROR_REFRACTION
			#pragma multi_compile _ _AKMU_MIRROR_MASK
			#pragma shader_feature _AKMU_MIRROR_RIPPLE
			#pragma multi_compile _ _AKMU_MIRROR_WAVE
            
            // -------------------------------------
            // Includes
            #include "../../Pipeline/Shaders/LitInput.hlsl"
            #include "../../Pipeline/Shaders/LitDepthNormalsPass.hlsl"
            ENDHLSL
        }

        // This pass it not used during regular rendering, only for lightmap baking.
        Pass
        {
            Name "Meta"
            Tags
            {
                "LightMode" = "Meta"
            }

            // -------------------------------------
            // Render State Commands
            Cull Off

            HLSLPROGRAM
            #pragma target 2.0

            // -------------------------------------
            // Shader Stages
            #pragma vertex UniversalVertexMeta
            #pragma fragment UniversalFragmentMetaLit

            //todo: a bug?
            //spot etc lights does not work on deferred if we do not put it here!!!!
            //#define _ADDITIONAL_LIGHT_SHADOWS
            
            // -------------------------------------
            // Material Keywords
            #pragma shader_feature_local_fragment _SPECULAR_SETUP
            #pragma shader_feature_local_fragment _EMISSION
            #pragma shader_feature_local_fragment _METALLICSPECGLOSSMAP
            #pragma shader_feature_local_fragment _ALPHATEST_ON
            #pragma shader_feature_local_fragment _ _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
            #pragma shader_feature_local _ _DETAIL_MULX2 _DETAIL_SCALED
            #pragma shader_feature_local_fragment _SPECGLOSSMAP
            #pragma shader_feature EDITOR_VISUALIZATION

            #define _AKMU_MIRROR

            // -------------------------------------
            // Includes
            #include "../../Pipeline/Shaders/LitInput.hlsl"
            #include "../../Pipeline/Shaders/LitMetaPass.hlsl"
            ENDHLSL
        }

        Pass
        {
            Name "Universal2D"
            Tags
            {
                "LightMode" = "Universal2D"
            }

            // -------------------------------------
            // Render State Commands
            Blend[_SrcBlendEx][_DstBlendEx]
            ZWrite[_ZWriteEx]
            Cull[_CullEx]

            HLSLPROGRAM
            #pragma target 2.0

            // -------------------------------------
            // Shader Stages
            #pragma vertex vert
            #pragma fragment frag

             // -------------------------------------
            // Material Keywords
            #pragma shader_feature_local_fragment _ALPHATEST_ON
            #pragma shader_feature_local_fragment _ALPHAPREMULTIPLY_ON

            #define _AKMU_MIRROR
            
            // -------------------------------------
            // Includes
            #include "../../Pipeline/Shaders/LitInput.hlsl"
            #include "../../Pipeline/Shaders/Utils/Universal2D.hlsl"
            ENDHLSL
        }
    }

    //FallBack "Hidden/Universal Render Pipeline/Lit"
    FallBack "Hidden/Universal Render Pipeline/FallbackError"
    //CustomEditor "UnityEditor.Rendering.Universal.ShaderGUI.LitShader"
    CustomEditor "AkilliMum.SRP.Mirror.ComplexEditor"
}
