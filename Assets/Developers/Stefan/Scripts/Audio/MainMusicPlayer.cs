using UnityEngine;

public class MainMusicPlayer : MonoBehaviour
{
    static int _musicID = -1;

    [SerializeField] SoundName _musicName;

    void Start()
    {
        if(_musicID == -1)
        {
            _musicID = SoundManager.Instance.PlaySound(_musicName);
        }
    }
}
