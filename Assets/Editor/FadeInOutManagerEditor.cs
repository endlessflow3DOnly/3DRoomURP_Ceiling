using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FadeInOutManager))]
public class FadeInOutManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        FadeInOutManager fadeInOutManager = (FadeInOutManager)target;

        EditorGUILayout.Space(20);

        EditorGUILayout.LabelField("�D�e���H�J�H�X����", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("TestFadeIn_Main", GUILayout.Width(150), GUILayout.Height(50)))
        {
            fadeInOutManager.TestFadeIn_Main();
        }

        if (GUILayout.Button("TestFadeOut_Main", GUILayout.Width(150), GUILayout.Height(50)))
        {
            fadeInOutManager.TestFadeOut_Main();
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(20);

        EditorGUILayout.LabelField("���C�Ҧ��H�J�H�X����", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("TestFadeIn_Roaming", GUILayout.Width(150), GUILayout.Height(50)))
        {
            fadeInOutManager.TestFadeIn_Main();
        }

        if (GUILayout.Button("TestFadeOut_Roaming", GUILayout.Width(150), GUILayout.Height(50)))
        {
            fadeInOutManager.TestFadeOut_Main();
        }
        EditorGUILayout.EndHorizontal();
    }
}
