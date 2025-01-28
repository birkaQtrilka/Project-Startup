using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoteUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] TextMeshProUGUI _pageNum;
    [SerializeField] TextMeshProUGUI _author;
    [SerializeField] Image _pfp;

    public void Init(PostData post)
    {
        _text.text = post.Content;
        _author.text = post.UserData.NickName;
        _pageNum.text = "Page: " + post.Page.ToString();
        _pfp.sprite = post.UserData.ProfilePicture;
    }
}
