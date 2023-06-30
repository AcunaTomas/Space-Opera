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

    public void ActivateAttack()
    {
        Debug.Log("activa ataque");
        player.GetComponent<PlayerCombat>().enabled = true;
    }


    
}
