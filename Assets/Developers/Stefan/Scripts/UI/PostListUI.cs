using System.Collections.Generic;
using UnityEngine;

public class PostListUI : MonoBehaviour
{
    [SerializeField] NoteUI _notePrefab;
    [SerializeField] Transform _noteContainer; 
    [SerializeField] NoteGetStrategy _noteGetter;

    public void UpdateUI()
    {
        if (_noteGetter == null) return;

        UpdateUI(_noteGetter.GetNotes());
       
    }

    public void UpdateUI(List<PostData> notes)
    {
        if (_noteContainer == null) return;

        _noteContainer.DestroyAllChildren();
        _noteContainer.InstantiateMultiple(_notePrefab, notes.Count, (inst, i) =>
        {
            PostData noteData = notes[i];
            inst.Init(noteData);
        });
    }

    void Start()
    {
        UpdateUI();
    }
}
