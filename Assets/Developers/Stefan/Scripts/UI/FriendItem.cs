using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FriendItem : MonoBehaviour
{
    [field: SerializeField] public Button Button;
    [SerializeField] TextMeshProUGUI _name;
    [SerializeField] Image _icon;

    public void Init(UserData data)
    {
        _name.text = data.NickName;
        _icon.sprite = data.ProfilePicture;
    }
}
