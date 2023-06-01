using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBoxController : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void ActivateAttack()
    {
        player.GetComponent<PlayerCombat>().enabled = true;
    }


    
}
