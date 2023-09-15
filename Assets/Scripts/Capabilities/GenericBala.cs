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
       Debug.Log(player);
       Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
       

       if (player.GetComponent<SpriteRenderer>().flipX == true)
       {
            GetComponent<SpriteRenderer>().flipX = true;
       }


       // Acá empieza el tema disparo, es un quilombo y seguramente se llena mas de if statements xd

       if (directionX == 10 && player.GetComponent<SpriteRenderer>().flipX == true)
       {
            body.AddForce(new Vector2(directionX * -15 ,directionY * 150));
       }
       else if (directionX == 10)
       {
            body.AddForce(new Vector2(directionX * 15 ,directionY * 150));
       }
       else
       {
            body.AddForce(new Vector2(directionX * 150 ,directionY * 150));
       }
       
       
    }

    private void OnCollisionEnter2D(Collision2D other) {
        other.gameObject.SendMessage("TakeDamage",damages,SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);
    }
}
