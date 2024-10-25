using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;


public class MissileController2 : BulletController
{
    public bool _flip;
    private Rigidbody2D _bg;
    [SerializeField] Animator _animator;
    public bool _destroy = true;

    [field: SerializeField] public EventReference ShootSound { get; private set; }
    [field: SerializeField] public EventReference ExplodeSound { get; private set; }


    public override void Start()
    {
        _bg = GetComponent<Rigidbody2D>();
        AudioController.instance.PlayOneShot(ShootSound, this.transform.position);
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
            AudioController.instance.PlayOneShot(ExplodeSound, this.transform.position);
            if(GetComponent<BoxCollider2D>()) GetComponent<BoxCollider2D>().enabled = false;
            _bg.velocity = Vector3.zero;
            Destroy(gameObject, 0.6f);
        }
        
    }
    
}
