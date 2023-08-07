using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorForPlayerCharacterSolutionsTM : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer playererTheFunny;
    private SpriteRenderer theFunny;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playererTheFunny = player.GetComponent<SpriteRenderer>();
        theFunny = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x - 0.1f + Mathf.Clamp(System.Convert.ToSingle(theFunny.flipX), 0, 0.2f) , player.transform.position.y, -1);
        theFunny.flipX = !playererTheFunny.flipX;

    }
}
