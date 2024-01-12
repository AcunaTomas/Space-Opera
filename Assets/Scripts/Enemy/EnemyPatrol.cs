using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float _speed;
    private float _lastSpeed;
    private Rigidbody2D _rb;
    public float _circleRadius;
    public GameObject _groundCheck;
    public LayerMask _groundLayer;
    public bool _facingRight;
    public bool _isGrounded;
    private Animator _animator;

    private float _waitTime = 4f;
    private float _waitCounter = 0f;
    public   bool _waiting = false;

    int _enemyLayer;
    int _defaultLayer;


    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _enemyLayer = LayerMask.NameToLayer("Enemy");
        _defaultLayer = LayerMask.NameToLayer("Default");
    }

    void FixedUpdate()
    {
        if (_waiting)
        {

            _waitCounter += Time.deltaTime;

            if (_waitCounter > _waitTime)
            {
                _animator.SetTrigger("Start");
                _waiting = false;
                return;
            }
        }

        if (_waiting == false)
        {
            _rb.velocity = Vector2.right * _speed * Time.deltaTime;
            _isGrounded = Physics2D.OverlapCircle(_groundCheck.transform.position, _circleRadius, _groundLayer);
            //_animator.SetTrigger("Start");
            if (!_isGrounded && _facingRight)
            {
                
                _animator.SetTrigger("Stop");
                _waitCounter = 0f;
                _waiting = true;

                _facingRight = !_facingRight;
                Flip();


            }
            else if (!_isGrounded && !_facingRight)
            {
                
                _animator.SetTrigger("Stop");
                _waitCounter = 0f;
                _waiting = true;

                _facingRight = !_facingRight;
                Flip();
            }
        }

    }

    void Flip()
    {
        //_facingRight = !_facingRight;
        transform.Rotate(new Vector3(0, 180, 0));
        _speed = -_speed;
    }

    public void NoHit()
    {
        Debug.Log("No se le pega");
        gameObject.tag = "Untagged";
    }

    void SiHit()
    {
        Debug.Log("Se le pega");
        gameObject.tag = "Enemy";
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_groundCheck.transform.position,_circleRadius);
    }
}
