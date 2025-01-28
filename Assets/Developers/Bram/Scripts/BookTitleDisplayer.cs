using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BookCover))]
public class BookTitleDisplayer : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI _title;

    BookCover _bookCover;
    
    private void OnEnable()
    {
        _bookCover = gameObject.GetComponent<BookCover>();
        
        _bookCover.OnBookUpdate += UpdateText;

        // Updating a second time because half of the time, UpdateUI is called
        // before subscribing, which means UpdateText misses its call
        _bookCover.UpdateUI();        
    }
    private void OnDisable()
    {
        _bookCover.OnBookUpdate -= UpdateText;
    }
    void UpdateText()
    {
        if (_title != null)
        {
            _title.text = _bookCover.BookData.Title;
        }
        else
        {
            Debug.Log("didn't get title for " + _bookCover.BookData.Title);
        }
    }

    
}
