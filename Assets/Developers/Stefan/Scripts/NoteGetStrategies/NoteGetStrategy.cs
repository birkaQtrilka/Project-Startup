using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class NoteGetStrategy : ScriptableObject
{
    public abstract List<PostData> GetNotes();
}
