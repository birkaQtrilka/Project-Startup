using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatHeaderUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _name;
    [SerializeField] Image _icon;

    public void UpdateUI()
    {
        UserData userData = FriendsUIManager.CurrentCheckedOutUser;

        if (userData == null) return;

        _name.text = userData.NickName;
        _icon.sprite = userData.ProfilePicture;


    }

    void OnEnable()
    {
        
        UpdateUI();
    }
}
