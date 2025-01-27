using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NoteGetStrategy/FromBooks")]
public class BookNoteGetter : NoteGetStrategy
{
    [SerializeField] BookLocalContainer _container;
    [SerializeField] string[] _olids;

    public override List<PostData> GetNotes()
    {
        var notes = new List<PostData>();
        foreach (var olid in _olids)
        {
            foreach (var note in _container.GetBookData(olid).Notes)
            {
                notes.Add(note);
            }
        }

        return notes;
    }
}
