using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dust : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer _sp;

    void Start()
    {
        _sp = GetComponent<SpriteRenderer>();
              
    }

    public void Initialize(Player.parama a)
    {
        transform.position = a.b;
        _sp = GetComponent<SpriteRenderer>();
        _sp.flipX = a.a; 
        Destroy(gameObject, 0.8f);  
    }

}
