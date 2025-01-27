using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="NoteGetStrategy/Manual")]
public class ManualNoteGetter : NoteGetStrategy
{
    [SerializeField] List<PostData> _posts;

    public override List<PostData> GetNotes()
    {
        return _posts;
    }
}
