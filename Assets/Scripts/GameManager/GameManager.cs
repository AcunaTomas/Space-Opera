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
    private float restartTime;

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
    
    void FixedUpdate()//Reset Button Hack, Binds to R key
    {
        if (Input.GetAxis("Debug Reset") > 0)
        {
            restartTime += 0.01f;
            print(restartTime);
            if (restartTime >= 1.6f)
            {
                ScenesManager.Instance.LoadMainMenu();
            }
        }
        else
        {
            restartTime = 0f;
        }
    }
}
