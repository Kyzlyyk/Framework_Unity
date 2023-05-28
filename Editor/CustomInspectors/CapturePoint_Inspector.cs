using Kyzlyk.LifeSupportModules.Enhancements;
using UnityEngine;

namespace UnityEditor
{
    [CustomEditor(typeof(CapturePoint))]
    public class CapturePoint_Inspector : Editor
    {
        private SerializedProperty _capturingStatus_Bool;
        private SerializedProperty _capturedPercentage_Int;
        private SerializedProperty _pointOwnerGroup_Char;

        private void OnEnable()
        {
            _capturingStatus_Bool = serializedObject.FindProperty("_openCaptureStatus");
            _capturedPercentage_Int = serializedObject.FindProperty("_capturedPercentage");
            _pointOwnerGroup_Char = serializedObject.FindProperty("_pointOwnerGroup");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(10);

            serializedObject.Update();

            _capturingStatus_Bool.boolValue = EditorGUILayout.Foldout(_capturingStatus_Bool.boolValue, "Capture Status");

            if (_capturingStatus_Bool.boolValue)
            {
                //
                GUILayout.BeginHorizontal();
                GUILayout.Space(10);
                GUILayout.Label("Captured");
                EditorGUILayout.IntField("", _capturedPercentage_Int.intValue, GUILayout.Width(30));
                GUILayout.Label("%");
                GUILayout.EndHorizontal();
                //

                //
                GUILayout.BeginHorizontal();
                GUILayout.Space(10);
                GUILayout.Label("Owner");
                GUILayout.Space(40);
                EditorGUILayout.TextField("", ((char)_pointOwnerGroup_Char.intValue).ToString(), GUILayout.Width(30));
                GUILayout.Label("Group");
                GUILayout.EndHorizontal();
                //
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}