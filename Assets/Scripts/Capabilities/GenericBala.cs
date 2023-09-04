using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericBala : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D body;
    private float directionX = 10f;  
    private float directionY = 0f;

    [SerializeField]
    private int damages = 5;

    public void SetDirection(float x, float y)
    {
        directionX = x;
        directionY = y;
    }
    void Start()
    {
       GameObject player = GameObject.FindWithTag("Player");
       Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>()); 
       body.AddForce(new Vector2(directionX * 150 ,directionY * 150));
       
    }


    private void OnCollisionEnter2D(Collision2D other) {
        other.gameObject.SendMessage("TakeDamage",damages,SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);
    }
}
