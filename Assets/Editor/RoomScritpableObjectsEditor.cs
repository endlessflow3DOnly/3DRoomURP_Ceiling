using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RoomInfo_Asset))]
public class RoomScritpableObjectsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RoomInfo_Asset roomInfo = (RoomInfo_Asset)target;

        EditorGUILayout.Space(20);

        EditorGUILayout.LabelField("點選Hireachy，存取座標跟旋轉");

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("取得", GUILayout.Width(200), GUILayout.Height(50)))
        {
            GameObject selectedObject = Selection.activeGameObject;

            if (selectedObject != null)
            {
                Vector3 position = selectedObject.transform.position;
                Quaternion rotation = selectedObject.transform.rotation;

                roomInfo.SetPositionAndRotation(position, rotation);
            }
        }

        EditorGUILayout.EndHorizontal();
    }
}
