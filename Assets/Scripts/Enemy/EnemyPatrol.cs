using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float _speed;
    private Rigidbody2D _rb;
    public float _circleRadius;
    public GameObject _groundCheck;
    public LayerMask _groundLayer;
    public bool _facingRight;
    public bool _isGrounded;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _rb.velocity = Vector2.right * _speed * Time.deltaTime;
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.transform.position,_circleRadius,_groundLayer);
        if (!_isGrounded && _facingRight)
        {
            Flip();
        }
        else if(!_isGrounded && !_facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        _facingRight = !_facingRight;
        transform.Rotate(new Vector3 (0, 180, 0));
        _speed = -_speed;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_groundCheck.transform.position,_circleRadius);
    }
}
