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
    [SerializeField] BookCover _cover;
    [SerializeField] NoteUI _oneNote;

    void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (Profile == null) return;

        _nickName.text = Profile.NickName;
        _birthDate.text = Profile.BirthDate;
        _title.text = Profile.Title;
        if(_favBookName!= null && Profile.FavBook !=null)
            _favBookName.text = Profile.FavBook.BookData.Title;
        _bio.text = Profile.Bio;
        _pfp.sprite = Profile.ProfilePicture;

        if(_cover != null)
            _cover.BookData = Profile.OwnedBooks[0].BookData;
        if(_oneNote != null)
            _oneNote.Init(Profile.Posts[0]);

    }
}
