using System;
using System.Collections.Generic;

public static class StefUtils
{
    public const int MAX_RATING = 5;
    static Random _defaultRandom = new Random();
    public static T GetRandomItem<T>(this List<T> list, Random randomGen = null)
    {
        _defaultRandom ??= new();
        if (randomGen == null) randomGen = _defaultRandom;
        if (list.Count == 0) return default;
        return list[randomGen.Next(0, list.Count)];
    }

    public static T GetRandomItem<T>(this T[] list, Random randomGen = null)
    {
        _defaultRandom ??= new();
        if (randomGen == null) randomGen = _defaultRandom;
        if (list.Length == 0) return default;

        return list[randomGen.Next(0, list.Length)];
    }
    
    public static string GetRandomItem(this string[] list, Random randomGen = null)
    {
        _defaultRandom ??= new();
        if (randomGen == null) randomGen = _defaultRandom;

        if (list.Length == 0) return default;
        
        return list[randomGen.Next(0, list.Length)];
    }
}
