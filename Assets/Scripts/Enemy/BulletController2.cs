using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletController2 : MonoBehaviour
{
    private GameObject _target;
    [HideInInspector]
    public Vector3 _targetPos;
    public float _speed;
    private Rigidbody2D _bulletRB;
    private int damage = 1;

    void Start()
    {
        _bulletRB = GetComponent<Rigidbody2D>();
        _target = GameObject.FindGameObjectWithTag("Player");
        if(_targetPos == new Vector3(0, 0, 0))
        {
            _targetPos = _target.transform.position;
        }
        Vector2 moveDir= (_targetPos - transform.position). normalized * _speed;
        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle));
        _bulletRB.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(this.gameObject, 3);

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
