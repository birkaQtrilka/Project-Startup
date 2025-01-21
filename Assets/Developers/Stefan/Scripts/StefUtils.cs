using System;
using System.Collections.Generic;

public static class StefUtils 
{
    public static T GetRandomItem<T>(this List<T> list, Random randomGen)
    {
        if (list.Count == 0) return default;
        return list[randomGen.Next(0, list.Count)];
    }

    public static object GetRandomItem(this Array list, Random randomGen)
    {
        if (list.Length == 0) return default;

        return list.GetValue(randomGen.Next(0, list.Length));
    }

    public static string GetRandomItem(this string[] list, Random randomGen)
    {
        if (list.Length == 0) return default;

        return list[randomGen.Next(0, list.Length)];
    }
}
