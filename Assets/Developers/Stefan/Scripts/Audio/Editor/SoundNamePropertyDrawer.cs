using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SoundName), true)]
public class SoundNamePropertyDrawer : PropertyDrawer
{
    
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty nameProperty = property.FindPropertyRelative("Name");
        SerializedProperty indexProperty = property.FindPropertyRelative("PopUpIndexOfChosenName");

        EditorGUI.BeginProperty(position, GUIContent.none, property);

        indexProperty.intValue = EditorGUI.Popup(position, property.displayName, indexProperty.intValue, SoundNamesContainer.Instance.Names);
        nameProperty.stringValue = SoundNamesContainer.Instance.Names[indexProperty.intValue];
        
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }
}
