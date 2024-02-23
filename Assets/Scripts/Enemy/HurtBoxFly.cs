using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HurtBoxFly : HurtBox
{

    private float _coolDown = 2f;

    private float _timeStamp;
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = GameObject.FindWithTag("Player");
            other.GetComponent<Player>().LoseHP(attackDamage);
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 2), ForceMode2D.Impulse);
            other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            other.GetComponent<PlayerCombat>().enabled = false;
            //other.GetComponent<Rigidbody2D>().AddForce(new Vector2(orientation * 5, 0), ForceMode2D.Impulse);
            Debug.Log(orientation);
            DelayMoveAgain(0.2f);
            Debug.Log("hit");
            GetComponent<BoxCollider2D>().enabled = false;
            Cooldown();
        }
    }
    

    private void FixedUpdate()
    {
        if (_timeStamp <= Time.time)
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }

    }

    private void Cooldown()
    {
      _timeStamp = Time.time + _coolDown;
    }
}
