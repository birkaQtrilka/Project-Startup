using UnityEngine;

public class FriendInfoUserDataProvider : MonoBehaviour
{
    [SerializeField] ProfileUI _profileUI;

    void OnEnable()
    {
        _profileUI.Profile = FriendsUIManager.CurrentCheckedOutUser;
        _profileUI.UpdateUI();
    }
}
