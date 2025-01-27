using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AuthorData 
{
    public string Name;
    public string OpenLibraryLink;
    public Sprite Image;
    [TextArea]
    public string Description;

    public List<string> OwnedBookOLLIDS;//library
    public List<PostData> Posts;

}
