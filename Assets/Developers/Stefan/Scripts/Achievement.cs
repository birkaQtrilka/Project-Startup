using System;
using UnityEngine;

[Serializable]
public class Achievement 
{
    public string Name;
    public Texture2D Icon;

    public string _script;

    [PolymorphicListAdder, SerializeField]
    BookConditionArrayWrapper Conditions;

}
[Serializable]
public class BookConditionArrayWrapper
{
    [SerializeReference] 
    public BookCondition[] Conditons;

}

public interface IBookCondition
{
    public bool CanApply(OwnedBook data);
}
[Serializable]
public abstract class BookCondition : IBookCondition//for serialization
{
    public abstract bool CanApply(OwnedBook data);
}
[Serializable]
public class PagesCondition : BookCondition
{
    [SerializeField] int _requiredNum;

    public override bool CanApply(OwnedBook data)
    {
        return data.CurrentPage >= _requiredNum;
    }
}

[Serializable]
public class GenreCondition : BookCondition
{
    [SerializeField] string _genre;

    public override bool CanApply(OwnedBook data)
    {
        return data.BookData.Genre == _genre;
    }
}