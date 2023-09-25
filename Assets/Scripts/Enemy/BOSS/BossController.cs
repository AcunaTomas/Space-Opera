using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject _missile;
    public GameObject _warning;
    public Animator _animator;
    public GameObject _player;

    int _rand = 0;
    bool _coolingAttack;
    int _lastRand = 0;



    private void Start()
    {
        switch (_rand)
        {
            case 0:
                First();
                break;
            case 1:
                Second();
                break;
            case 2:
                Third();
                break;
            default: break;
        }
    }
    void FixedUpdate()
    {
        Debug.Log(_coolingAttack);
        if (_player.GetComponent<Playerererer>().GetHP() <= 0)
        {
            PlayerRespawneando();
            Debug.Log("Se murió el player");
        }
        else if (_player.GetComponent<Playerererer>().GetHP() > 0)
        {
            _rand = Random.Range(0, 4);
        }

        
        //_rand = Random.Range(0, 4);

        if (_rand == 0 && _lastRand != 1)
        {
            First();
        }
        else if (_rand == 1 && _lastRand != 2)
        {
            Second();
        }
        else if (_rand == 2 && _lastRand != 3)
        {
            Third();
        }
        else if (_rand == 3 && _lastRand != 4)
        {
            Fourth();
        }
        
        
    }
    void First()
    {
        if (!_coolingAttack)
        {
            _coolingAttack = true;
            StartCoroutine(StartCooldown());
            _lastRand = 1;
            _animator.SetTrigger("Attack");

            Instantiate(_missile, new Vector2(16.787f, 17.389f), Quaternion.identity);
            Instantiate(_warning, new Vector2(16.787f, 14.624f), Quaternion.identity);

            Instantiate(_missile, new Vector2(17.579f, 17.389f), Quaternion.identity);
            Instantiate(_warning, new Vector2(17.579f, 14.624f), Quaternion.identity);

            Instantiate(_missile, new Vector2(18.371f, 17.389f), Quaternion.identity);
            Instantiate(_warning, new Vector2(18.371f, 14.624f), Quaternion.identity);
        }
        
    }

    void Second()
    {
        if (!_coolingAttack)
        {
            _coolingAttack = true;
            StartCoroutine(StartCooldown());
            _lastRand = 2;
            _animator.SetTrigger("Attack");

            Instantiate(_missile, new Vector2(16.787f, 17.389f), Quaternion.identity);
            Instantiate(_warning, new Vector2(16.787f, 14.624f), Quaternion.identity);

            Instantiate(_missile, new Vector2(18.1935f, 17.389f), Quaternion.identity);
            Instantiate(_warning, new Vector2(18.1935f, 14.624f), Quaternion.identity);

            Instantiate(_missile, new Vector2(19.600f, 17.389f), Quaternion.identity);
            Instantiate(_warning, new Vector2(19.600f, 14.624f), Quaternion.identity);

        }
    }

    void Third()
    {
        if (!_coolingAttack)
        {
            _coolingAttack = true;
            StartCoroutine(StartCooldown());
            _lastRand = 3;
            _animator.SetTrigger("Attack");

            Instantiate(_missile, new Vector2(19.000f, 17.389f), Quaternion.identity);
            Instantiate(_warning, new Vector2(19.000f, 14.624f), Quaternion.identity);

            Instantiate(_missile, new Vector2(19.300f, 17.389f), Quaternion.identity);
            Instantiate(_warning, new Vector2(19.300f, 14.624f), Quaternion.identity);

            Instantiate(_missile, new Vector2(19.600f, 17.389f), Quaternion.identity);
            Instantiate(_warning, new Vector2(19.600f, 14.624f), Quaternion.identity);

        }
    }   

    void Fourth()
    {
        if (!_coolingAttack)
        {
            _coolingAttack = true;
            StartCoroutine(StartCooldown());
            _lastRand = 4;
            _animator.SetTrigger("Attack");

            Instantiate(_missile, new Vector2(17.54f, 17.389f), Quaternion.identity);
            Instantiate(_warning, new Vector2(17.54f, 14.624f), Quaternion.identity);

            Instantiate(_missile, new Vector2(18.92f, 17.389f), Quaternion.identity);
            Instantiate(_warning, new Vector2(18.92f, 14.624f), Quaternion.identity);

        }
    }

    private IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(2f);

        _coolingAttack = false;
    }

    private void PlayerRespawneando()
    {
        if (!_coolingAttack)
        {
            _coolingAttack = true;
            StartCoroutine(StartSecondCooldown());
        }   
    }

    private IEnumerator StartSecondCooldown()
    {
        yield return new WaitForSeconds(3f);

        _coolingAttack = false;
    }
}
