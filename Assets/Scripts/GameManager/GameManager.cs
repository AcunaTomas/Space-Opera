using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager INSTANCE;
    public GameObject PLAYER;
    public Vector3 CHECKPOINT;

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
        
    }
}
