using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HurtBoxFly : MonoBehaviour
{
    int attackDamage = 1;
    float orientation = 1f;
    private GameObject player;

    private float _coolDown = 2f;

    private float _timeStamp;

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

    public void GetPlayer()
    {
        player = GameObject.FindWithTag("Player");
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
            Debug.Log(orientation);
            Invoke("MoveAgain", 0.2f);
            Debug.Log("hit");
            GetComponent<BoxCollider2D>().enabled = false;
            Cooldown();
        }
    }

    private void MoveAgain()
    {
        Debug.Log("hfjlkahfjkasjkhaf");
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        if (player.GetComponent<Player>().GetHP() > 0)
        {
            player.GetComponent<PlayerCombat>().enabled = true;
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
