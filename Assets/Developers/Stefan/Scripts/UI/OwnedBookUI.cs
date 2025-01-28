using UnityEngine;
using UnityEngine.UI;

public class OwnedBookUI : MonoBehaviour
{
    [SerializeField] BookCover _cover;
    [SerializeField] Image _fill;
    
    OwnedBook _ownedBook;

    public void UpdateUI()
    {
        if(_cover.BookData == null)
            _cover.UpdateUI();

        if (_ownedBook == null)
            _ownedBook = Resources.Load<OwnedBook>(_cover.BookData.Title);
        if (_ownedBook == null) return;

            float fill = (float)_ownedBook.CurrentPage / _ownedBook.BookData.NumberOfPages;
        _fill.fillAmount = fill;
    }


    void Start()
    {
        UpdateUI();    
    }
}
