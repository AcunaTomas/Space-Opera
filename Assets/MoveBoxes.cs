using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoxes : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D box) //for when you hit the ground
    {
        if(box.gameObject.CompareTag("Movible"))
        {
            box.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
    void OnCollisionExit2D(Collision2D box) //for when you hit the ground
    {
        if(box.gameObject.CompareTag("Movible"))
        {
            //gameObject.GetComponent<Player>().canIjump = true;
            box.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }
}
