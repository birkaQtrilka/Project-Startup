using UnityEngine;
using UnityEngine.Events;

public class OwnedBook : ScriptableObject
{
    public UnityEvent<OwnedBook> OnProgressUpdate;
    [field: SerializeField] public BookData BookData { get; private set; }

    public int CurrentPage;

#if UNITY_EDITOR
    void OnValidate()
    {
        OnProgressUpdate?.Invoke(this);

    }
#endif

    public void SetProgress(int currentPage)
    {
        CurrentPage = currentPage;

        OnProgressUpdate?.Invoke(this);
    }

    public void Init(BookData bookData)
    {
        BookData = bookData;
    }
}
