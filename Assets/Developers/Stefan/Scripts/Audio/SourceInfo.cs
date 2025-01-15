using System;
using UnityEngine;
/// <summary>
/// simple wrapper class to have a way to access sounds that have the same name 
/// </summary>
[Serializable]
class SourceInfo
{
    public AudioSource AudioSource;
    public int ID;

    public SourceInfo(AudioSource audioSource, int id)
    {
        AudioSource = audioSource;
        ID = id;
    }
}
