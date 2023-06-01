using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBoxController : MonoBehaviour
{
    private GameObject player;
    private GameObject bombMaker;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        bombMaker = GameObject.FindWithTag("BombMaker");
    }

    void ActivateAttack()
    {
        player.GetComponent<PlayerCombat>().enabled = true;

    }


    
}
