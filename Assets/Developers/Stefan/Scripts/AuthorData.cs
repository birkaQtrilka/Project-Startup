using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
[Serializable]
public class AuthorData 
{
    public List<UserData> Friends;
    public List<OwnedBook> OwnedBooks;//library
    public List<BookData> WishList;
    public List<PostData> Posts;
    public List<PostData> Achievements;

}
