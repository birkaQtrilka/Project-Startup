using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Manager in unity playmode to play sounds in a simple way
/// </summary>
public class SoundManager : MonoBehaviour
{
    #region Singleton
    public static SoundManager Instance { get; private set; }
    void Awake()
    {
        //Debug.Log("song awake");
        if (Instance != null && Instance != this)
        {
            Debug.Log("destroying song manager");
            Destroy(this);
            return;
        }
        else
        {
            _inactiveSources = new Stack<AudioSource>(GetComponents<AudioSource>());
            foreach (AudioSource source in _inactiveSources)
                InitSource(source);
            Instance = this;

            DontDestroyOnLoad(this);
        }

    }
    #endregion

    //my object pool
    readonly List<SourceInfo> _activeSources = new();
    Stack<AudioSource> _inactiveSources;

    
    [SerializeField, Range(0, 1)] float _masterVolume =1;
    //the container with sounds
    [SerializeField] Sounds _data;

    //to stop actively playing sounds
    int _currentID;

    //SoundData from here is general, so you don't have to put the same volume of a sound in 100 different scripts.
    //However, you CAN have custom volume/soundData in each object in case you want to make one more quiet than the others
    //Use SoundAdapter if you want to play sounds onclick or on other UnityEvent calls
    public static int PlayRandomSound(SoundName[] names, float volumeMult = 1)
    {
        return Instance.PlaySound(names[UnityEngine.Random.Range(0, names.Length)], volumeMult);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="names"></param>
    /// <param name="onComplete">gets called on sound end/stop</param>
    /// <param name="volumeMult"></param>
    /// <returns>the id</returns>
    public static int PlayRandomSound(SoundName[] names, Action onComplete, float volumeMult = 1)
    {
        return Instance.PlaySound(names[UnityEngine.Random.Range(0, names.Length)], onComplete, volumeMult);
    }

    public bool ContainsSoundWithID(int id)
    {
        return _activeSources.Any(s => s.ID == id);
    }

    /// <summary>
    /// Used internally to simpler acces of sounds
    /// </summary>
    /// <param name="soundName"></param>
    /// <param name="volumeMult"></param>
    /// <returns>the id and the unity native component</returns>
    (int, AudioSource) PlaySound(string soundName, float volumeMult = 1, float pitch = 1)
    {
        if (!_data.Contains(soundName))
        {
            _data.UdpateDictionary();
            if (!_data.Contains(soundName))
            {
                Debug.LogError($"There is no sound named {soundName}");
                return (-1, null);
            }
        }
        int soundID = _currentID++;
        AudioSource source = GetSource(soundID);
        SoundData data = _data[soundName];
        source.clip = data.Clip;
        source.volume = data.Volume * volumeMult * _masterVolume;
        source.pitch = data.Pitch * pitch;
        source.loop = data.Loop;
        source.Play();

        return (soundID, source);
    }

    //using coroutines so it reacts to timeScale
    IEnumerator WaitForSoundEnd(AudioSource source, Action onComplete)
    {
        yield return new WaitUntil(() => source.time >= source.clip.length || (source.time == 0 && !source.isPlaying));
        onComplete?.Invoke();
    }

    /// <summary>
    /// Play a preset sound from the inspector
    /// </summary>
    /// <param name="soundName"></param>
    /// <param name="volumeMult"></param>
    /// <returns></returns>
    public int PlaySound(SoundName soundName, float volumeMult = 1, float pitch = 1)
    {
        (int id, _) = PlaySound(soundName.Name, volumeMult, pitch);
        return id;
    }
    /// <summary>
    /// Play a preset sound from the inspector
    /// </summary>
    /// <param name="soundName"></param>
    /// <param name="onComplete"> callback when the sound ended, also when it got stopped</param>
    /// <param name="volumeMult"></param>
    /// <returns></returns>
    public int PlaySound(SoundName soundName, Action onComplete, float volumeMult = 1, float pitch = 1)
    {
        (int id, AudioSource audioSource) = PlaySound(soundName.Name, volumeMult, pitch);
        StartCoroutine(WaitForSoundEnd(audioSource, onComplete));
        return id;
    }
    /// <summary>
    /// Stops sound by ID
    /// </summary>
    /// <param name="id"></param>
    public void StopSound(int id)
    {
        SourceInfo source = FindSourceInfoByID(id);
        if (source != null)
        {
            source?.AudioSource.Stop();
            ReleaseSource(source);
        }
    }

    public AudioSource GetSoundSource(int id)
    {
        return FindSourceInfoByID(id).AudioSource;
    }

    // to set sources on default settings
    void InitSource(AudioSource source)
    {
        source.playOnAwake = false;
    }

    //gets source from object pool
    AudioSource GetSource(int id)
    {
        AudioSource source;
        if (_inactiveSources.Count == 0)
        {
            source = gameObject.AddComponent<AudioSource>();
            InitSource(source);
        }
        else
            source = _inactiveSources.Pop();

        _activeSources.Add(new SourceInfo(source, id));

        source.loop = false;
        source.volume = _masterVolume;
        source.enabled = true;

        return source;

    }

    SourceInfo FindSourceInfoByID(int id)
    {
        return _activeSources.FirstOrDefault(s => s.ID == id);
    }

    //releases soource from object pool
    void ReleaseSource(SourceInfo audioSource)
    {
        _activeSources.Remove(audioSource);
        audioSource.AudioSource.enabled = false;
        _inactiveSources.Push(audioSource.AudioSource);
    }

    //releases sounds that ended
    void Update()
    {
        for (int i = 0; i < _activeSources.Count; i++)
        {
            var source = _activeSources[i];
            bool soundEnded = source.AudioSource.time >= source.AudioSource.clip.length || (source.AudioSource.time == 0 && !source.AudioSource.isPlaying);

            if (soundEnded)
            {
                ReleaseSource(source);
                i--;
            }
        }
    }
}