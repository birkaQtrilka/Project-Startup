using UnityEngine;

public class FriendsUIManager : MonoBehaviour
{
    public static UserData CurrentCheckedOutUser;

    [SerializeField] FriendItem _friendUIPrefab;
    [SerializeField] Transform _container;
    [SerializeField] PageManager _singleChatPage;
    [SerializeField] Page _infoPage;
    public UserData[] Friends;


    public void UpdateUI()
    {
        _container.DestroyAllChildren();

        _container.InstantiateMultiple(_friendUIPrefab, Friends.Length,
            (inst, i) =>
            {
                //inst.Init(Friends[i]);
                UserData copy = Friends[i];
                inst.Button.onClick.AddListener(() => CurrentCheckedOutUser = copy);
                inst.Button.onClick.AddListener(() => _singleChatPage.SwitchToPage(_infoPage));

            }
        );
    }

    private void OnEnable()
    {
        UpdateUI    ();
    }
}
