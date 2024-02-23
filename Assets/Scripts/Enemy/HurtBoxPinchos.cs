using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HurtBoxPinchos : HurtBox
{
    
    public override void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player>().LoseHP(attackDamage);
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 2), ForceMode2D.Impulse);
            other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            other.GetComponent<PlayerCombat>().enabled = false;
            Debug.Log(orientation);
            Invoke("MoveAgain", 0.2f);
            Debug.Log("hit");
        }
    }

    public override void MoveAgain()
    {
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        if (player.GetComponent<Player>().GetHP() > 0)
        {
            player.GetComponent<PlayerCombat>().enabled = true;
        }
        player.transform.position = new Vector2(6.4f, 9.1f);
    }

}
