using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireBehaviour : MonoBehaviour
{
    private float _fireRate = 1f;
    private float _nextFireTime;
    public float _shootingRange;
    public GameObject _bullet;
    public GameObject _bulletParent;
    public Animator _animator;
    private Transform _player;

    void Start()
    {
        _player = GameManager.INSTANCE.PLAYER.transform;
    }

    public void GetPlayer()
    {
        _player = GameManager.INSTANCE.PLAYER.transform;
    }
    void FixedUpdate()
    {
        float _distanceFromPlayer = Vector2.Distance(_player.position, transform.position);
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
        
    }

    public void Test()
    {
        _animator.SetBool("Attack", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,_shootingRange);
    }
}
