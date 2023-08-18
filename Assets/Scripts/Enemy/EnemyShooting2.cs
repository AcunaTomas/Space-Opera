using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting2 : MonoBehaviour
{
    public Transform _playerPos;
    public float _speed;
    public float _distanciaFrenado;
    public float _distanciaRetroceso;

    public Transform _bulletSource;
    public GameObject _bullet;
    private float _tiempo;

    private SpriteRenderer _spriteRenderer;

    private bool inRange = true;


    void Start()
    {
        _playerPos = GameObject.FindWithTag("Player").transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        _tiempo += Time.deltaTime;
        if (_tiempo >= 2)
        {
            Instantiate(_bullet, _bulletSource.position, Quaternion.identity);
            _bullet.SetActive(true);
            
            _tiempo = 0;
        }

        if (inRange)
        {
            EnemyShootingLogic();
        }

    }

    private void EnemyShootingLogic()
    {
        //Movement
        if (Vector2.Distance(transform.position, _playerPos.position) > _distanciaFrenado)
        {
            transform.position = Vector2.MoveTowards(transform.position, _playerPos.position, _speed * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, _playerPos.position) < _distanciaRetroceso)
        {
            transform.position = Vector2.MoveTowards(transform.position, _playerPos.position, -_speed * Time.deltaTime);
        }

        if ((Vector2.Distance(transform.position, _playerPos.position) < _distanciaFrenado) && (Vector2.Distance(transform.position, _playerPos.position) > _distanciaRetroceso))
        {
            transform.position = transform.position;
        }

        //Flip
        if (_playerPos.position.x > this.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            _bulletSource.localPosition = new Vector2(0.2f,0);
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
            _bulletSource.localPosition = new Vector2(-0.2f,0);
        }
    }

}
