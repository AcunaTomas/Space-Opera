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
    public bool PLAYER_COMBAT = false;
    public string OBJECTIVE;
    public int LEVEL;
    public float MUSIC_VOLUME = 1f;
    public float SFX_VOLUME = 1f;

    //LEVEL 1
    public bool[] CANVAS_WS_LVL1_GENERAL = {true, true, true, true, true};
    public bool[] CANVAS_WS_LVL1_ACTIVATE_EVENTS = {false, false, false, false, false};
    public bool[] TUTO_TRIG_LVL1 = {true, true, true, false};
    public bool[] CHECKPOINTS_LVL1 = {true, true, true, true, true, true, true, true};
    public bool[] DIALOGUES_LVL1 = {true, true, true, true, true, true, true, true, true, true, true, true, true, true, false};
    public bool[] CHANGE_MUSIC_LVL1 = {false, true};
    public bool[] EVENTS_GO_HERE_LVL1 = {false, true, true, true, false, false, true, false, true, true, true, false, false};
    public bool INVISIBLE_TROLL_LVL1 = true;
    public Vector3[] ELEVATORS_LVL1 = {new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f)};
    public bool ELEVATOR_DOOR_LVL1 = true;
    public bool[] DUMMIES_LVL1 = {false, false, false};
    public int[] DUMMIES_LAYER_LVL1 = {7, 7, 7};
    public bool PILOT_QUILOMB_LVL1 = false;
    public bool EVENTS_QUILOMB_LVL1 = false;
    public bool[] EVENTS_ALL_QUILOMB_LVL1 = { true, true, true, true, true, true, true, true, true, true, true, true, true, true};
    public Vector3 ELEVATOR_SHIP_LVL1 = new Vector3(0f, 0f, 0f);
    public Vector3 SPIKES_LVL1 = new Vector3(0f, 0.32f, 0f);
    public bool BTTON_QUILOMB_LVL1 = true;
    public bool NPCS_LVL1 = true;

    public GameData()
    {
        PLAYER_POSITION = new Vector3(1.21f, 13.87508f, 0f);
        PLAYER_MAX_HP = 5;
        PLAYER_ACTUAL_HP = 5;
        OBJECTIVE = "objective01_explore";
        LEVEL = 1;

        //LEVEL 1
    }
}
