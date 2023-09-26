using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class LoadJson
{
    public static string LVL1_DIALOGUES = Resources.Load<TextAsset>("Text/lvl1").text;
    public static string LVL1_CINEMATIC = Resources.Load<TextAsset>("Text/lvl1_cinematic").text;
    public static string LVL2 = Resources.Load<TextAsset>("Text/lvl2").text;
    public static string LVL3 = Resources.Load<TextAsset>("Text/lvl3").text;
    public static string LVL2_CINEMATIC = Resources.Load<TextAsset>("Text/lvl2_cinematic").text;
    public static string LVL_SELECT = Resources.Load<TextAsset>("Text/lvl_selection").text;
    public static string LVL_2X = Resources.Load<TextAsset>("Text/lvl2x").text;
}
