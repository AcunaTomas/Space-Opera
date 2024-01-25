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
    public PersistableEvent[] SavedEvents;



    public GameData()
    {
        PLAYER_POSITION = new Vector3(1.21f, 13.87508f, 0f);
        PLAYER_MAX_HP = 5;
        PLAYER_ACTUAL_HP = 5;
        OBJECTIVE = "objective01_explore";
        LEVEL = 1;

        
    }
}
