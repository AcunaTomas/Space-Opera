using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Vector2 _velocity;
    public bool _leftMovement = true;
    private Rigidbody2D _body;
    public Animator animator;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        animator.SetFloat("Speed", 1);
    }

    private void FixedUpdate()
    {
        _velocity = _body.velocity;

        if(_leftMovement){
            _velocity.x = Mathf.MoveTowards(-1f, -5f, 50f * Time.deltaTime);
        }
        else{
            _velocity.x = Mathf.MoveTowards(1f, 5f, 50f * Time.deltaTime);
        }

        _body.velocity = _velocity;
    }
}
