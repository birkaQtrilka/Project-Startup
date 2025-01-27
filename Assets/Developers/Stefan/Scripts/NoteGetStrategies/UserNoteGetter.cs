using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NoteGetStrategy/FromUsers")]
public class UserNoteGetter : NoteGetStrategy
{
    [SerializeField] UserData[] users;

    public override List<PostData> GetNotes()
    {
        var notes = new List<PostData>();
        foreach (var user in users)
        {
            foreach (var note in user.Posts)
            {
                notes.Add(note);
            }
        }

        return notes;
    }
}
