using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class LoadJson
{
    private static string JSON_NAME = "lvl1_cinematic.json";
    private static string LOCATIONJSON = "Assets/Text/" + JSON_NAME;
    public static string CONTENT = File.ReadAllText(LOCATIONJSON);

    public static string LVL1 = File.ReadAllText("Assets/Text/lvl1.json");
    public static string LVL1_CINEMATIC = File.ReadAllText("Assets/Text/lvl1_cinematic.json");
}
