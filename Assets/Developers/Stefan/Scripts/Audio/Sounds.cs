using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//used to see sounds as if in a dictionary
[Serializable]
public struct SoundPair
{
    public SoundName name;
    public SoundData data;
}
/// <summary>
/// A container of sounds, you can have different sound clips per sound name. Allowing for fast testing
/// </summary>
[CreateAssetMenu(menuName ="SoundCollection")]
public class Sounds : ScriptableObject
{
    public SoundData this[string key]
    {
        get => _soundClipsDictionary[key]; 
        set => _soundClipsDictionary[key] = value; 
    }
    Dictionary<string, SoundData> _soundClipsDictionary;
    [SerializeField] List<SoundPair> _inspectorSoundClips = new();

    
    void Reset()
    {
        UdpateDictionary();
    }

    void OnEnable()
    {
        UdpateDictionary();

    }
    /// <summary>
    /// returns true if it contains a sound with the given name
    /// </summary>
    /// <param name="soundName"></param>
    /// <returns></returns>
    public bool Contains(string soundName)
    {
        return _soundClipsDictionary.ContainsKey(soundName);
    }
    /// <summary>
    /// returns true if it contains a sound with the given name
    /// </summary>
    /// <param name="soundName"></param>
    /// <returns></returns>
    public bool Contains(SoundName name)
    {
        return Contains(name.Name);
    }
    /// <summary>
    /// Update Container before using it for the first time or after adding a sound during runtime
    /// </summary>
    public void UdpateDictionary()
    {
        for (int i = 0; i < _inspectorSoundClips.Count - 1; i++)
        {
            for (int j = i + 1; j < _inspectorSoundClips.Count; j++)
            {
                if (_inspectorSoundClips[i].name.Name != _inspectorSoundClips[j].name.Name) continue;

                _inspectorSoundClips.RemoveAt(j);
                Debug.LogError($"item {i} and item {j} have the same name. This isn't allowed. Deleting item {j}");
                j--;
            }
        }
        _soundClipsDictionary = _inspectorSoundClips.ToDictionary(t => t.name.Name, t => t.data);
    }
}
