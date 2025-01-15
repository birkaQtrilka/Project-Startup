using UnityEngine;
/// <summary>
/// Used to set sounds in the inspector. For example for OnClickEvents
/// </summary>
public class SoundAdapter : MonoBehaviour
{
    //instead of enums to avoid recompiling the project on name addition
    [SerializeField] SoundName _soundName;
    [SerializeField] float _volume = 1;

    //used to stop the sounds that are actively playing
    int _soundID = -1;

    public void PlaySound()
    {
        if(SoundManager.Instance != null)
            _soundID = SoundManager.Instance.PlaySound(_soundName,_volume);
    }

    public void StopSound()
    {
        if(SoundManager.Instance != null)
            SoundManager.Instance.StopSound(_soundID);
    }
}
