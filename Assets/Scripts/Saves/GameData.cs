using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 PLAYER_POSITION;
    public int PLAYER_MAX_HP;
    public int PLAYER_ACTUAL_HP;
    public bool PLAYER_FLIP_X = false;
    public string OBJECTIVE;

    //LEVEL 1
    public bool[] CANVAS_WS_LVL1_GENERAL = {true, true, true, true, true};
    public bool[] CANVAS_WS_LVL1_ACTIVATE_EVENTS = {false, false, false, false, false};
    public bool[] TUTO_TRIG_LVL1 = {true, true};
    public bool[] CHECKPOINTS_LVL1 = {true, true, true, true, true, true, true};
    public bool[] DIALOGUES_LVL1 = {true, true, true, true, true, true, true, true, true, true, true, true, true, true, false};
    public bool[] CHANGE_MUSIC_LVL1 = {false, true};
    public bool[] EVENTS_GO_HERE_LVL1 = {false, true, true, false, false, false, true, true, true, false, false};
    public bool INVISIBLE_TROLL_LVL1 = true;

    public GameData()
    {
        PLAYER_POSITION = new Vector3(1.21f, 13.87508f, 0f);
        PLAYER_MAX_HP = 5;
        PLAYER_ACTUAL_HP = 5;
        OBJECTIVE = "objective01_explore";

        //LEVEL 1
    }
}
