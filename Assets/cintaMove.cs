using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cintaMove : MonoBehaviour
{
    private Rigidbody2D rb ;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate ()
    {
        rb.velocity = new Vector2 (-1.2f, 0f);
    }
    
}
