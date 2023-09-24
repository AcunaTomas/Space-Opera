using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController2 : MonoBehaviour
{
    private GameObject _target;
    public float _speed;
    private Rigidbody2D _bulletRB;
    private int damage = 1;

    void Start()
    {
        _bulletRB = GetComponent<Rigidbody2D>();
        _target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir= (_target.transform.position - transform.position). normalized * _speed;
        _bulletRB.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(this.gameObject, 4);

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "Enemy")
        {
            col.gameObject.SendMessage("LoseHP",damage,SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
        
        
    }
}
