using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SoundPair), true)]
public class SoundPairDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.PropertyField(property.FindPropertyRelative("name"));
        EditorGUILayout.PropertyField(property.FindPropertyRelative("data"));

        EditorGUILayout.EndHorizontal();
    }
}
