using System;
using UnityEngine;

[Serializable]
public class PostData 
{
    public BookDataSO Book;

    public string PublishTime;
    public bool IsEdited;

    public string Content;
    public int Page;

    public UserData UserData;

    public int ID;
}

public struct PublishTime
{
    public int Year;
    public int Month;
    public int Date;
    public int Time;
}

[Serializable]
public class ReviewData
{
    public string PublishTime;
    public bool IsEdited;
    [TextArea]
    public string Content;
    [Range(0, StefUtils.MAX_RATING)]
    public int Rating;
    public UserData UserData;

}
