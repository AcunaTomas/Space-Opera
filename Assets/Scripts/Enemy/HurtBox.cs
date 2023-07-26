using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HurtBox : MonoBehaviour
{
    float attackDamage = 10f;
    float orientation = 1f;
    
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
            //other.GetComponent<Rigidbody2D>().AddForce(new Vector2(orientation * 5, 0), ForceMode2D.Impulse);
            gameObject.SetActive(false);
            Debug.Log(orientation);
        }
    }

}
