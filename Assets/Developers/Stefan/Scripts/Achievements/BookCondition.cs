using System;

[Serializable]
public abstract class BookCondition : IBookCondition//for serialization
{
    public abstract bool CanApply(OwnedBook data);
}
