using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BookCover : MonoBehaviour
{
    static PageManager _rootPageManager;

    [SerializeField] Image _image;
    [SerializeField] BookLocalContainer _container;
    [SerializeField] string _bookOLID;
    [SerializeField] bool _updateOnEnable = true;
    public BookLocalContainer Container => _container;
    public BookData BookData { get; private set; }

    public void SetOlid(string olid)
    {
        _bookOLID = olid;
    }

    public void SendToBookOverview()
    {
        if(_rootPageManager == null)
            _rootPageManager = GameObject.FindFirstObjectByType<Canvas>().GetComponent<PageManager>();

        Page page = _rootPageManager.SwitchToPageAndGet("DetailedBookPage");
        DetailedBookUI bookUI = page.GetComponentInChildren<DetailedBookUI>();
        
        BookData ??= _container.Books.First(b => b.OLID == _bookOLID);

        if(BookData != null )
            bookUI.UpdateUI(BookData);
    }

    void OnEnable()
    {
        if(BookData == null && _updateOnEnable)
            UpdateUI();
    }

    public void UpdateUI()
    {
        BookData = _container.Books.FirstOrDefault(b => b.OLID == _bookOLID);

        if (BookData == null) return;

        _image.sprite = BookData.Cover;
    }
}
