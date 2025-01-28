using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileUI : MonoBehaviour
{
    public UserData Profile;
    [SerializeField] TextMeshProUGUI _nickName;
    [SerializeField] TextMeshProUGUI _birthDate;
    [SerializeField] TextMeshProUGUI _title;
    [SerializeField] TextMeshProUGUI _favBookName;
    [SerializeField] TextMeshProUGUI _bio;
    [SerializeField] Image _pfp;

    void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        _nickName.text = Profile.NickName;
        _birthDate.text = Profile.BirthDate;
        _title.text = Profile.Title;
        _favBookName.text = Profile.FavBook.name;
        _bio.text = Profile.Bio;
        _pfp.sprite = Profile.FavBook.BookData.Cover;
    }
}
