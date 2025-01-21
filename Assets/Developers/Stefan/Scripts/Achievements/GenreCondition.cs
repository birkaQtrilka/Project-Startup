using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class GenreCondition : BookCondition
{
    [SerializeField] string _genre;

    public override bool CanApply(OwnedBook data)
    {
        return data.BookData.Genres.Contains(_genre);
    }
}