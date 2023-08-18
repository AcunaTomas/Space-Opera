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
        Vector2 moveDir = (_target.transform.position - transform.position).normalized * speed;
        _bulletRig.velocity = new Vector2(moveDir.x, 0);
        Destroy(this.gameObject, 2);

        //_bulletRig.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }
}
