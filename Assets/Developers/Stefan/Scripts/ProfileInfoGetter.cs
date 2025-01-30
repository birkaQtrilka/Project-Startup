using System.Collections;
using UnityEngine;

public class ProfileInfoGetter : MonoBehaviour
{
    ProfileUI _profileUI;
    [SerializeField] PageManager _tabsManager;
    [SerializeField] Page _infoPage;

    void Awake()
    {
        _profileUI = GetComponentInChildren<ProfileUI>();       
    }

    public void SetUser()
    {
        FriendsUIManager.CurrentCheckedOutUser = _profileUI.Profile;
        //fuck it
        UserManager.Instance.StartCoroutine(WaitFrame());
    }

    IEnumerator WaitFrame()
    {
        yield return null;
        _tabsManager.SwitchToPage(_infoPage);
    }
}
