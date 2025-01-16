using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookDataUI : MonoBehaviour
{
    [SerializeField] BookGetter _bookGetter;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] Image _bookImage;
    [SerializeField] Button _leftButton;
    [SerializeField] Button _rightButton;
    int _pos;
    BookData[] _books;

    void Start()
    {
        _leftButton.onClick.AddListener(DecrementPos);
        _rightButton.onClick.AddListener(IncrementPos);
        _ = ShowUIAfterGet();
    }

    async Task ShowUIAfterGet()
    {
        _books = await _bookGetter.FetchData();
        _pos = 0;
        UpdateUI();

    }

    public void IncrementPos()
    {
        if (_books == null || _books.Length < _pos + 2) return;
        _pos++; 
        UpdateUI();
    }

    public void DecrementPos()
    {
        if (_books == null || _pos <= 0) return;

        _pos--;
        UpdateUI();
    }

    void UpdateUI()
    {
        _text.text = _books[_pos].ToString();
        _bookImage.sprite = _books[_pos].Cover;
    }
}
