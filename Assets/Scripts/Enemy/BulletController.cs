using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public float speedY;
    private GameObject _target;
    private Rigidbody2D _bulletRig;


    void Start()
    {
        _bulletRig = GetComponent<Rigidbody2D>();
        _target = GameObject.FindWithTag("Player");
        Vector2 moveDir = (_target.transform.position - transform.position).normalized;
        if (_target.transform.position.x > transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        _bulletRig.velocity = new Vector2(moveDir.x * speed, moveDir.y * speedY);
        Destroy(this.gameObject, 2);

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }
}
