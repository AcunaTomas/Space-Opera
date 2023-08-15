using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public string ALTSKIPENABLED = "Disabled";
    public static GameManager INSTANCE;
    public int LEVEL;
    public GameObject PLAYER;
    public GameObject BUTTON_INTERACT;
    public Vector3 CHECKPOINT;
    public GameObject ACTUAL_CHECKPOINT;
    public bool PLAYER_COMBAT = false;
    public ButtonDialogue CANVAS;

    //estas últimas cosas se borran después, solo es para probar
    /* public KeyCode MORIR;
    private bool _ePressed = false;
    public Slider SLIDER; */

    private void Awake()
    {
        INSTANCE = this;
        CANVAS = transform.GetChild(1).gameObject.GetComponent<ButtonDialogue>();
    }

    void Start()
    {
        if (LEVEL < 1 || LEVEL > 4 )
        {
            Debug.Log("El nivel se encuentra fuera del rango permitido (1-4)");
        }
    }
    
    void Update()
    {
        /* if (Input.GetKeyDown(MORIR) && !_ePressed)
        {
            _ePressed = true;
            if (SLIDER.value != 0)
            {
                SLIDER.value-=20;
            }
            if (SLIDER.value <= 0)
            {
                PLAYER.transform.SetParent(null);
                PLAYER.transform.localPosition = new Vector3 (CHECKPOINT.x, CHECKPOINT.y, CHECKPOINT.z);
                SLIDER.value = SLIDER.maxValue;
            }
        }

        if (Input.GetKeyUp(MORIR))
        {
            _ePressed = false;
        } */
    }
}
