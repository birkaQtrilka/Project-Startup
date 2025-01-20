using System;
using UnityEngine;

[Serializable]
public class Achievement 
{
    public string Name;
    public Texture2D Icon;

    public string Script;

    [PolymorphicListAdder, SerializeField]
    BookConditionArrayWrapper _conditions;

    public BookCondition[] Conditions => _conditions.Conditons;

}
[Serializable]
public class BookConditionArrayWrapper
{
    [SerializeReference] 
    public BookCondition[] Conditons;

}
