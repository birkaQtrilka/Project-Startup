using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ProgressScrollwheelSelector : MonoBehaviour
{
    [SerializeField] DetailedBookUI _DBUI;
    [SerializeField] int _currentPage;
    [SerializeField] GameObject _scrollwheelButtonPrefab;
    [SerializeField] TextMeshProUGUI _currentPageText;
    private void OnEnable()
    {
        int a;

        // I need book data so i know the number of pages the book has
        OwnedBook myBook = UserManager.Instance.CurrentUser.OwnedBooks.FirstOrDefault(b => b.BookData.OLID == _DBUI._bookOLID);
        //myBook.CurrentPage = _currentPage;

        a = myBook.BookData.NumberOfPages;

        // make children
        for (int i = 0; i < a; i++)
        {
            GameObject btn = Instantiate(_scrollwheelButtonPrefab, transform);
            btn.GetComponent<ScrollwheelProgressButton>().SetValues(i, this);
        }

        SetCurrentPage(myBook.CurrentPage);

    }

    private void OnDisable()
    {
        transform.DestroyAllChildren();
        
    }

    public void SetCurrentPage(int pNumber)
    {
        _currentPage = pNumber;
        _currentPageText.text = _currentPage.ToString();
    }

    public void ConfirmCurrentPage()
    {
        OwnedBook myBook = UserManager.Instance.CurrentUser.OwnedBooks.FirstOrDefault(b => b.BookData.OLID == _DBUI._bookOLID);
        //myBook.CurrentPage = _currentPage;

        myBook.SetProgress(_currentPage);

    }
}
