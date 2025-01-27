using System;
using UnityEngine;
/// <summary>
/// used in dictionary to preset certain clip values before playing them
/// </summary>
[Serializable]
public class SoundData
{
    [Range(0f, 1f)] public float Volume = 1;
    [Range(-3f, 3f)] public float Pitch = 1;
    public AudioClip Clip;
    public bool Loop;
}
