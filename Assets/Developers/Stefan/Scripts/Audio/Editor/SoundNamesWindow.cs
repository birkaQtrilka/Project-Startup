using UnityEditor;
using UnityEngine;
//easier way to acces SoundNames. You can use the scriptable object too
public class SoundNamesWindow : EditorWindow
{
    [MenuItem("Stefan/SoundNames")]
    public static void ShowWindow()
    {
        GetWindow<SoundNamesWindow>("SoundNames");
    }

    public string[] Names = new string[0];

    private void OnGUI()
    {
        SoundNamesContainer container = SoundNamesContainer.Instance;

        SerializedObject so = new (container);
        SerializedProperty stringsProperty = so.FindProperty("Names");

        EditorGUILayout.PropertyField(stringsProperty, true);
        so.ApplyModifiedProperties(); 
    }
}
