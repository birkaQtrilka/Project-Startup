using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BookCover : MonoBehaviour
{
    static PageManager _rootPageManager;

    [SerializeField] Image _image;
    [SerializeField] BookLocalContainer _container;
    [SerializeField] string _bookOLID;
    
    BookData _bookData;

    public void SendToBookOverview()
    {
        if(_rootPageManager == null)
            _rootPageManager = GameObject.FindFirstObjectByType<Canvas>().GetComponent<PageManager>();

        Page page = _rootPageManager.SwitchToPageAndGet("DetailedBookPage");
        DetailedBookUI bookUI = page.GetComponentInChildren<DetailedBookUI>();
        
        _bookData ??= _container.Books.First(b => b.OLID == _bookOLID);

        if(_bookData != null )
            bookUI.UpdateUI(_bookData);
    }

    void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        _bookData = _container.Books.FirstOrDefault(b => b.OLID == _bookOLID);

        if (_bookData == null) return;

        _image.sprite = _bookData.Cover;
    }
}
