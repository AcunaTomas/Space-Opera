using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualAttack : MonoBehaviour
{
    private GameObject _player;
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        Destroy(gameObject, 0.4f);

        if (_player.GetComponent<PlayerCombat>()._attackCount == 1)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Golpe1");
        }
        else if (_player.GetComponent<PlayerCombat>()._attackCount == 2)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Golpe2");
        }
        else if (_player.GetComponent<PlayerCombat>()._attackCount == 3)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Golpe3");
        }

        if (_player.GetComponent<PlayerCombat>().attackPoint.localPosition.x == -0.15f)
        {
            gameObject.transform.localPosition = new Vector2 (0.15f, 0);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (_player.GetComponent<PlayerCombat>().attackPoint.localPosition.x == 0.15f)
        {
            gameObject.transform.localPosition = new Vector2 (-0.15f, 0);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        
    }
}
