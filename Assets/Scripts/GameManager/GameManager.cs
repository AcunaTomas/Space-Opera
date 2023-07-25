using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public static GameManager INSTANCE;
    public GameObject PLAYER;
    public Vector3 CHECKPOINT;
    public bool PLAYER_COMBAT = false;

    //estas últimas cosas se borran después, solo es para probar
    /* public KeyCode MORIR;
    private bool _ePressed = false;
    public Slider SLIDER; */

    private void Awake()
    {
        INSTANCE = this;
    }

    void Start()
    {
        /* CHECKPOINT = PLAYER.transform.localPosition;
        SLIDER.maxValue = int.Parse(PLAYER.GetComponent<Player>().GetHP());
        SLIDER.value = SLIDER.maxValue */;
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
