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

    public LayerMask _changeSideLayer;
    public bool _facingRight;
    public bool _isGrounded;
    public bool _changeSide;
    private Animator _animator;

    private float _waitTime = 0.3f;
    private float _waitCounter = 0f;
    public   bool _waiting = false;

    int _enemyLayer;
    int _defaultLayer;

    private Enemy _mainScript;
    private EnemySoundsController _sounds;



    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _mainScript = GetComponent<Enemy>();
        _sounds = GetComponent<EnemySoundsController>();


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
                //_animator.SetTrigger("Start");
                _waiting = false;
                return;
            }
        }

        if (_waiting == false)
        {
            _rb.velocity = Vector2.right * _speed * Time.deltaTime;
            _isGrounded = Physics2D.OverlapCircle(_groundCheck.transform.position, _circleRadius, _groundLayer);
            _changeSide = Physics2D.OverlapCircle(_groundCheck.transform.position, _circleRadius, _changeSideLayer);
    
            //_animator.SetTrigger("Start");
            if (!_isGrounded && _facingRight || _changeSide && _facingRight)
            {
                
                //_animator.SetTrigger("Stop");
                _waitCounter = 0f;
                _waiting = true;

                _facingRight = !_facingRight;
                Flip();
            }
            else if (!_isGrounded && !_facingRight || _changeSide && !_facingRight)
            {
                //_animator.SetTrigger("Stop");
                _waitCounter = 0f;
                _waiting = true;

                _facingRight = !_facingRight;
                Flip();
            }
        }

        if(_mainScript.currentHealth > 0)
        {
            if(_speed == 40 || _speed == -40)
            {
                _sounds.UpdateParameter("EnemyMode", 0);
            }
            else if(_speed == 60 || _speed == -60)
            {
                _sounds.UpdateParameter("EnemyMode", 1);
            }
        }
        
    }

    void Flip()
    {
        //_facingRight = !_facingRight;
        transform.Rotate(new Vector3(0, 180, 0));
        _speed = -_speed;
    }

    public void ChangeSpeed()
    {
        if (_speed > 0)
        {
            _speed = 60;
        }
        else if (_speed < 0)
        {
            _speed = -60;
        }


    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_groundCheck.transform.position,_circleRadius);
    }
}
