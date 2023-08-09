using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public GameManager GM;

    public GameData()
    {
        GM = GameManager.INSTANCE;
    }
}
