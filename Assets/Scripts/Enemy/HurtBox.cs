using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HurtBox : MonoBehaviour
{
    int attackDamage = 1;
    float orientation = 1f;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        Physics2D.IgnoreCollision(player.GetComponent<CircleCollider2D>(), GetComponent<Collider2D>());
    }

    public float setOrientation(float orientationValue)
    {
        orientation = orientationValue;
        return orientation;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player>().LoseHP(attackDamage);
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 2), ForceMode2D.Impulse);
            other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            other.GetComponent<PlayerCombat>().enabled = false;
            //other.GetComponent<Rigidbody2D>().AddForce(new Vector2(orientation * 5, 0), ForceMode2D.Impulse);
            gameObject.SetActive(false);
            Invoke("MoveAgain", 0.2f);
            Debug.Log("hit");
        }
    }

    private void MoveAgain()
    {
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        if (player.GetComponent<Player>().HP > 0)
        {
            player.GetComponent<PlayerCombat>().enabled = true;
        }
    }
}
