using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GenericBala : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D body;
    private float directionX = 10f;  
    private float directionY = 0f;
    private float bulletTimeout = 6f;

    [SerializeField]
    private float _speed = 1f;

    [SerializeField]
    private int damages = 5;

    [SerializeField]
    GameObject player;

    public void SetDirection(float x, float y)
    {
        directionX = x;
        directionY = y;
    }

    void Awake()
    {

    }

    void OnEnable()
    {
      body.velocity = new Vector2(0,0);
      
      player = GameManager.INSTANCE.GetActivePlayer().gameObject;
      Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
      Debug.Log("Enal");


       // Acá empieza el tema disparo, AVISO QUE ESTO ES CLARAMENTE INEFICIENTE, I HATE MY LIFE
      
/*        if (directionX == 10 && player.GetComponent<SpriteRenderer>().flipX == true)
       {
          body.AddForce(new Vector2(directionX * -1.5f ,directionY * 15f));
       }
       else if (directionX == 10)
       {
          body.AddForce(new Vector2(directionX * 1.5f ,directionY * 15f));
       }
       else if (directionX == 0 && directionY > 0 && directionY <= 1)
       {
         body.AddForce(new Vector2(0, 15f));
       }
       else if (directionX == 0 && directionY < 0)
       {
         body.AddForce(new Vector2(0, -15f));
       }
       else if (directionY == 0 && directionX > 0 && directionX <= 1)
       {
         body.AddForce(new Vector2(15f, 0));
       }
       else if (directionY == 0 && directionX < 0)
       {
         body.AddForce(new Vector2(-15f, 0));
       }
       else if (directionX > 0 && directionX <= 1 && directionY > 0 && directionY <= 1)
       {
         body.AddForce(new Vector2(15f, 15f));
       }
       else if (directionX < 0 && directionY < 0)
       {
         body.AddForce(new Vector2(-15f, -15f));
       }
       else if (directionX > 0 && directionY < 0)
       {
         body.AddForce(new Vector2(15f, -15f));
       }
       else if (directionX < 0 && directionY > 0)
       {
         body.AddForce(new Vector2(-15f, 15f));
       } */



      // Código inicial de Acuña que funcionaba pero las balas tienen velocidad variable

      //So, resulta que la magnitud del vector es igual a √(x² + y²), el cual es el converso del teorema de pitagoras (z² = x² + y²), asi que mi solucion fue clampearla al valor de velocidad de la bala, en fin, la hipotenuza.
      Vector2 Force_a = new Vector2(directionX * _speed ,directionY * _speed);
      body.AddForce(Vector2.ClampMagnitude(Force_a, _speed));



      if (directionX > 0)
      {
        GetComponent<SpriteRenderer>().flipX = false;
      }
      else
      {
        GetComponent<SpriteRenderer>().flipX = true;
      }

       // Rotación no eficiente de la bala

       if (directionY > 0 && directionX > 0)
       {
          transform.rotation = Quaternion.Euler(0, 0, 45);
       }
       else if (directionY > 0 && directionX < 0)
       {
          transform.rotation = Quaternion.Euler(0, 0, -45);
       }
       else if (directionY > 0)
       {
          transform.rotation = Quaternion.Euler(0, 0, -90);
        }



       if (directionY < 0 && directionX > 0)
       {
          transform.rotation = Quaternion.Euler(0, 0, -45);
       }
       else if (directionY < 0 && directionX < 0)
       {
          transform.rotation = Quaternion.Euler(0, 0, 45);
       }
       else if (directionY < 0)
       {
          transform.rotation = Quaternion.Euler(0, 0, 90);

       }


       StartCoroutine(Timeout());

    }

    private IEnumerator Timeout()
    {
      yield return new WaitForSeconds(bulletTimeout);
      gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyRumba")
        {
          try
          {
            other.gameObject.GetComponent<Enemy>().TakeDamage(damages, new Vector2(Math.Abs(other.gameObject.transform.position.x), Math.Abs(other.gameObject.transform.position.y)));
          }
          catch
          {

            gameObject.SetActive(false);
            return;
          }
          
        }
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.gameObject.tag == "Destroyable")
        {
          //other.gameObject.GetComponent<Event>().manualDo();
          other.gameObject.GetComponent<Event>().external = false;
          other.gameObject.BroadcastMessage("manualDo", SendMessageOptions.DontRequireReceiver);
          gameObject.SetActive(false);
        }
    }
}
