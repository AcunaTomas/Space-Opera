using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController2 : BulletController
{
    public bool _flip;
    private Rigidbody2D _bg;
    [SerializeField] Animator _animator;
    public bool _destroy = true;
    public override void Start()
    {
        _bg = GetComponent<Rigidbody2D>();
        if (!_flip)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            _bg.velocity = new Vector2(speed, 0);
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
            _bg.velocity = new Vector2(-speed, 0);
        }
        
    }

    public override void OnCollisionEnter2D(Collision2D col)
    {
        if (_destroy)
        {
            _animator.SetTrigger("Explode");
            if(GetComponent<BoxCollider2D>()) GetComponent<BoxCollider2D>().enabled = false;
            _bg.velocity = Vector3.zero;
            Destroy(gameObject, 0.6f);
        }
        
    }
    
}
