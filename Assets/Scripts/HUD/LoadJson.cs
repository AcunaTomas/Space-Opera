using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class LoadJson
{
    private static string _jsonName = "dialogues.json";
    public static string LOCATIONJSON = "Assets/Text/" + _jsonName;
    public static string CONTENT = File.ReadAllText(LOCATIONJSON);

}
