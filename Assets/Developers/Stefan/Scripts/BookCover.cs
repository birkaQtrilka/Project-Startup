using UnityEngine;
using UnityEngine.UI;

public class BookCover : MonoBehaviour
{
    static PageManager _rootPageManager;

    [SerializeField] Image _image;
    [SerializeField] bool _updateOnEnable = true;
    [field: SerializeField]public BookDataSO BookData { get; set; }

    public void SendToBookOverview()
    {
        if(_rootPageManager == null)
            _rootPageManager = GameObject.FindFirstObjectByType<Canvas>().GetComponent<PageManager>();

        Page page = _rootPageManager.SwitchToPageAndGet("DetailedBookPage");
        DetailedBookUI bookUI = page.GetComponentInChildren<DetailedBookUI>();
        
        if(BookData != null )
            bookUI.UpdateUI(BookData);
    }

    void OnEnable()
    {
        if(BookData != null && _updateOnEnable)
            UpdateUI();
    }

    public void UpdateUI()
    {
        if (BookData == null) return;

        _image.sprite = BookData.Cover;
    }
}
