using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static string Tokey(this object val)
    {
        return "W_" + val.ToString();
    }
    public static int Fromkey(this string val)
    {
        string[] s = val.Split('_');
        return int.Parse(s[1]);
    }
    public static List<string> ConvertPathToList(this string path)
    {
        string[] array = path.Split('/');
        List<string> paths = new List<string>();
        paths.AddRange(array);

        return paths;

    }
}
