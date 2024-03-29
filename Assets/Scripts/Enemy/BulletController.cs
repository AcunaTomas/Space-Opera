using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    private GameObject _target;
    private Rigidbody2D _bulletRig;


    void Start()
    {
        _bulletRig = GetComponent<Rigidbody2D>();
        _target = GameObject.FindWithTag("Player");
        //Vector2 moveDir = (_target.transform.position - transform.position).normalized * speed;
        if (_target.transform.position.x > transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (_target.transform.position.x > transform.position.x)
        {
            _bulletRig.velocity = new Vector2(1, 0);
        }
        else
        {
            _bulletRig.velocity = new Vector2(-1, 0);
        }
        //_bulletRig.velocity = new Vector2(moveDir.x, 0);
        Destroy(this.gameObject, 2);

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }
}