using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingDown : MonoBehaviour
{
    private float _fireRate = 1f;
    private float _nextFireTime;
    public GameObject _bullet;
    public GameObject _bulletParent;
    public Animator _animator;
    [SerializeField] private LayerMask _layerRef;
    private RaycastHit2D _hitRef;
    private bool _hitSomething = false;
    private bool _hitMiddle;
    private bool _hitDowns;
    private Vector2 _salidaRef;
    [SerializeField] private float _animDelay;

    void Start()
    {
        _nextFireTime = Time.time + _fireRate;
    }

    void FixedUpdate()
    {
        //Debug.DrawRay(_refAbajoIZQ, Vector2.left + Vector2.down, Color.red);
        //Debug.DrawRay(_refAbajoDER, Vector2.right + Vector2.down, Color.yellow);
        DoRaycasts();
        if(_hitSomething)
        {
            Flip(_hitRef.transform);
            _bullet.GetComponent<BulletController2>()._targetPos = _hitRef.point;
            if(_hitMiddle && !_hitDowns)
            {
                _animator.SetBool("AttackSides", false);
                _animator.SetBool("AttackDownSides", false);
                _animator.SetBool("AttackDown", true);
                //_bullet.GetComponent<BulletController2>().ChangeTargetPos(_hitRef.point);
            }
            else if(!_hitMiddle && _hitDowns)
            {
                _animator.SetBool("AttackSides", false);
                _animator.SetBool("AttackDown", false);
                _animator.SetBool("AttackDownSides", true);

            }
            else if(!_hitMiddle && !_hitDowns )
            {
                _animator.SetBool("AttackDown", false);
                _animator.SetBool("AttackDownSides", false);
                _animator.SetBool("AttackSides", true);
            }
        }
        if(_nextFireTime - _animDelay/2 < Time.time)
            {  
                Invoke("Attack", _animDelay);
                _nextFireTime = Time.time + _fireRate;
            }
        /*
        if (_distanceFromPlayer <= _shootingRange && _nextFireTime < Time.time && _player.position.y <= transform.position.y)
        {
            Instantiate(_bullet, _bulletParent.transform.position, Quaternion.identity);
            _nextFireTime = Time.time + _fireRate;
            _animator.SetBool("Attack", true);
        }
        else
        {
            _animator.SetBool("Attack", false);
        }
        */
        
        
        
    }
    void Flip(Transform target)
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x + .5f) 
        {
            GetComponent<SpriteRenderer>().flipX = true;
            //_bulletParent.transform.localPosition = new Vector2(-0.2f,0);
        }
        else 
        {
            GetComponent<SpriteRenderer>().flipX = false;
            //_bulletParent.transform.localPosition = new Vector2(0.2f,0);
        }

    
    //Ternary Operator
    //rotation.y = (currentTarget.position.x < transform.position.x) ? rotation.y = 180f : rotation.y = 0f;

        transform.eulerAngles = rotation;
    }
    void DoRaycasts()
    {
        /*Medio, abajoizq, abajo der :( dejar lógica
        Vector2 _refAbajo = new Vector2 (transform.position.x, transform.GetComponent<Collider2D>().bounds.min.y);
        Vector2 _refAbajoIZQ = transform.GetComponent<Collider2D>().bounds.min;
        Vector2 _refAbajoDER = new Vector2 (transform.GetComponent<Collider2D>().bounds.max.x, transform.GetComponent<Collider2D>().bounds.min.y);
        */
        Vector2 _refAbajo = _bulletParent.gameObject.transform.GetChild(0).transform.position;
        Vector2 _refAbajoIZQ = _bulletParent.gameObject.transform.GetChild(1).transform.position;
        Vector2 _refAbajoDER = _bulletParent.gameObject.transform.GetChild(2).transform.position;
        Vector2 _refIZQ = _bulletParent.gameObject.transform.GetChild(3).transform.position;
        Vector2 _refDER = _bulletParent.gameObject.transform.GetChild(4).transform.position;
        RaycastHit2D hitAbajo = Physics2D.Raycast(_refAbajo, Vector2.down, _layerRef);
        RaycastHit2D hitAbajoIZQ = Physics2D.Raycast(_refAbajoIZQ, Vector2.left + Vector2.down, _layerRef);
        RaycastHit2D hitAbajoDER = Physics2D.Raycast(_refAbajoDER, Vector2.right + Vector2.down, _layerRef);
        RaycastHit2D hitIZQ = Physics2D.Raycast(_refIZQ, Vector2.left, _layerRef);
        RaycastHit2D hitDER = Physics2D.Raycast(_refDER, Vector2.right, _layerRef);
        HitPlayer(hitAbajo, _refAbajo, hitAbajoIZQ, _refAbajoIZQ, hitAbajoDER, _refAbajoDER, hitIZQ, _refIZQ, hitDER, _refDER);
        /*
        HitPlayer(hitAbajoIZQ);
        HitPlayer(hitAbajoDER);
        */
    }
    void HitPlayer(RaycastHit2D hit1, Vector2 refsal1, RaycastHit2D hit2, Vector2 refsal2, RaycastHit2D hit3, Vector2 refsal3, RaycastHit2D hit4, Vector2 refsal4, RaycastHit2D hit5, Vector2 refsal5)
    {
        if(hit1.collider.gameObject.tag != "Player" && hit2.collider.gameObject.tag != "Player" && hit3.collider.gameObject.tag != "Player" && hit4.collider.gameObject.tag != "Player" && hit5.collider.gameObject.tag != "Player")
        {
            _hitSomething = false;
            return;
        }
        else
        {
            
        }
        if (hit1.collider.gameObject.tag == "Player")
        {
            _hitSomething = true;
            _hitMiddle = true;
            _hitDowns = false;
            _hitRef = hit1;
            _salidaRef = refsal1;
        }
        else if (hit2.collider.gameObject.tag == "Player")
        {
            _hitSomething = true;
            _hitMiddle = false;
            _hitDowns = true;
            _hitRef = hit2;
            _salidaRef = refsal2;
        }
        else if (hit3.collider.gameObject.tag == "Player")
        {
            _hitSomething = true;
            _hitMiddle = false;
            _hitDowns = true;
            _hitRef = hit3;
            _salidaRef = refsal3;
        }
        else if (hit4.collider.gameObject.tag == "Player")
        {
            _hitSomething = true;
            _hitMiddle = false;
            _hitDowns = false;
            _hitRef = hit4;
            _salidaRef = refsal4;
        }
        else if (hit5.collider.gameObject.tag == "Player")
        {
            _hitSomething = true;
            _hitMiddle = false;
            _hitDowns = false;
            _hitRef = hit5;
            _salidaRef = refsal5;
        }
    }
    public void Attack()
    {
        Instantiate(_bullet, _salidaRef, Quaternion.identity);
    }
}