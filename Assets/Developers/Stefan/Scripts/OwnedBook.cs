using UnityEngine;

public class OwnedBook : ScriptableObject
{
    [field: SerializeField] public BookData BookData { get; private set; }

    public int CurrentPage;

    public void SetProgress(int currentPage)
    {
        CurrentPage = currentPage;

        //
    }

    public void Init(BookData bookData)
    {
        BookData = bookData;
    }
}
