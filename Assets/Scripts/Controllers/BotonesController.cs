using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonesController : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bomb"))
        {
            Destroy(other.gameObject);
        }
            
    }
}
