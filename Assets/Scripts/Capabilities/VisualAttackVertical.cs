using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualAttackVertical : MonoBehaviour
{
    private GameObject _player;
    public bool _up = false;
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        Destroy(gameObject, 0.4f);

        if (_up)
        {
            if (_player.GetComponent<PlayerCombat>().spriteRenderer.flipX)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                gameObject.GetComponent<Animator>().SetTrigger("GolpeUp");
            }
            else if (_player.GetComponent<SpriteRenderer>().flipX == false)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                gameObject.GetComponent<Animator>().SetTrigger("GolpeUp");
            }
        }
        else 
        {
            if (_player.GetComponent<PlayerCombat>().spriteRenderer.flipX)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                gameObject.GetComponent<Animator>().SetTrigger("GolpeDown");
            }
            else if (_player.GetComponent<SpriteRenderer>().flipX == false)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                gameObject.GetComponent<Animator>().SetTrigger("GolpeDown");
            }
        }
        
    }
    void FixedUpdate()
    {
        {
            if (_player.GetComponent<PlayerCombat>().spriteRenderer.flipX)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }
}
