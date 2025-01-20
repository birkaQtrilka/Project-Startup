using System;
using UnityEngine;

[Serializable]
public class GenreCondition : BookCondition
{
    [SerializeField] string _genre;

    public override bool CanApply(OwnedBook data)
    {
        return data.BookData.Genre == _genre;
    }
}