using System.IO;
using UnityEditor;
using UnityEngine;

public class SoundNamesContainer : ScriptableObject
{

    private static SoundNamesContainer _instance; 
    public static SoundNamesContainer Instance 
    {
        get
        {
            if (_instance == null) 
            {
                _instance = Resources.Load<SoundNamesContainer>("SoundNames");
                
                if (_instance == null) 
                { 
                    _instance = CreateInstance<SoundNamesContainer>();
                    string path = "Assets/Resources"; 
                    if (!Directory.Exists(path))
                    { 
                        Directory.CreateDirectory(path); 
                        Debug.Log("Directory created at: " + path); 
                    } 
                    AssetDatabase.CreateAsset(_instance, "Assets/Resources/SoundNames.asset");
                    AssetDatabase.SaveAssets();
                } 
            } 
            return _instance; 
        }
    }

    public string[] Names;
}
