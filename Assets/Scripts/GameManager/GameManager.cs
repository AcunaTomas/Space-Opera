using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager INSTANCE;
    public GameObject PLAYER;
    public Vector3 CHECKPOINT;

    //estas últimas cosas se borran después, solo es para probar
    public KeyCode MORIR;
    private bool _ePressed = false;

    private void Awake()
    {
        INSTANCE = this;
    }

    void Start()
    {
        CHECKPOINT = PLAYER.transform.localPosition;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(MORIR) && !_ePressed)
        {
            _ePressed = true;
            PLAYER.transform.localPosition = new Vector3 (CHECKPOINT.x, CHECKPOINT.y, CHECKPOINT.z);
        }

        if (Input.GetKeyUp(MORIR))
        {
            _ePressed = false;
        }
    }
}
