using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    private Rigidbody2D _bulletRig;

    void Start()
    {
        _bulletRig = GetComponent<Rigidbody2D>();
        _bulletRig.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);        
    }
}
