using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyBehaviour : MonoBehaviour
{

    public float _speed;
    public float _check;
    private Transform _player;

    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        float _distanceFromPlayer = Vector2.Distance(_player.position, transform.position);
        if (_distanceFromPlayer < _check)
        transform.position = Vector2.MoveTowards(this.transform.position, _player.position, _speed*Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,_check);
    }
}
