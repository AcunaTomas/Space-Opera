using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, IDataPersistance
{
    
    public static GameManager INSTANCE;
    public int LEVEL;
    public GameObject PLAYER;
    public GameObject BUTTON_INTERACT;
    public Vector3 CHECKPOINT;
    public GameObject ACTUAL_CHECKPOINT;
    public bool PLAYER_COMBAT = false;
    public ButtonDialogue CANVAS;

    private void Awake()
    {
        INSTANCE = this;
        CANVAS = transform.GetChild(1).gameObject.GetComponent<ButtonDialogue>();
    }

    void Start()
    {
        if (LEVEL < 1 || LEVEL > 4 )
        {
            Debug.LogError("El nivel se encuentra fuera del rango permitido (1-4)");
        }
    }
    
    void Update()
    {
        
    }

    void IDataPersistance.LoadData(GameData data)
    {
        INSTANCE = data.GM;
    }

    void IDataPersistance.SaveData(ref GameData data)
    {
        data.GM = INSTANCE;
    }
}
