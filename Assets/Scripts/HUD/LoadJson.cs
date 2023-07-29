using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class LoadJson
{
    private static string JSON_NAME = "lvl1_cinematic";
    private static string LOCATIONJSON = "Text/" + JSON_NAME;
    public static string CONTENT = Resources.Load<TextAsset>(LOCATIONJSON).text;

    public static string LVL1 = Resources.Load<TextAsset>("Text/lvl1").text;
    public static string LVL1_CINEMATIC = Resources.Load<TextAsset>("Text/lvl1_cinematic").text;
}
