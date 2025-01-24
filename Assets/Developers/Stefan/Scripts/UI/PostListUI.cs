using System.Collections.Generic;
using UnityEngine;

public class PostListUI : MonoBehaviour
{
    [SerializeField] NoteUI _notePrefab;
    [SerializeField] Transform _noteContainer; 
    [SerializeField] NoteGetStrategy _noteGetter;

    public void UpdateUI()
    {
        List<PostData> notes = _noteGetter.GetNotes();

        if (_noteContainer == null) return;

        int numChildren = _noteContainer.childCount;
        for (int i = numChildren - 1; i >= 0; i--)
        {
            GameObject.Destroy(_noteContainer.GetChild(i).gameObject);
        }
        for (int i = 0; i < notes.Count; i++)
        {
            PostData noteData = notes[i];

            NoteUI noteUI = Instantiate(_notePrefab, _noteContainer.transform);
            noteUI.Init(noteData);
        }
    }

    void Start()
    {
        UpdateUI();
    }
}
