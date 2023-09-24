using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject _missile;
    public GameObject _warning;

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
        _rand = Random.Range(0, 4);

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

            Instantiate(_missile, new Vector2(16.787f, 17.389f), Quaternion.identity);
            Instantiate(_warning, new Vector2(16.787f, 14.624f), Quaternion.identity);

            Instantiate(_missile, new Vector2(18.194f, 17.389f), Quaternion.identity);
            Instantiate(_warning, new Vector2(18.194f, 14.624f), Quaternion.identity);

            Instantiate(_missile, new Vector2(19.601f, 17.389f), Quaternion.identity);
            Instantiate(_warning, new Vector2(19.601f, 14.624f), Quaternion.identity);
            
        }
    }

    void Third()
    {
        if (!_coolingAttack)
        {
            _coolingAttack = true;
            StartCoroutine(StartCooldown());
            _lastRand = 3;

            Instantiate(_missile, new Vector2(19.357f, 17.389f), Quaternion.identity);
            Instantiate(_warning, new Vector2(19.357f, 14.624f), Quaternion.identity);

            Instantiate(_missile, new Vector2(19.601f, 17.389f), Quaternion.identity);
            Instantiate(_warning, new Vector2(19.601f, 14.624f), Quaternion.identity);

            Instantiate(_missile, new Vector2(19.845f, 17.389f), Quaternion.identity);
            Instantiate(_warning, new Vector2(19.845f, 14.624f), Quaternion.identity);
        }
    }

    void Fourth()
    {
        if (!_coolingAttack)
        {
            _coolingAttack = true;
            StartCoroutine(StartCooldown());
            _lastRand = 4;

            Instantiate(_missile, new Vector2(19.144f, 17.389f), Quaternion.identity);
            Instantiate(_warning, new Vector2(19.144f, 14.624f), Quaternion.identity);

            Instantiate(_missile, new Vector2(17.579f, 17.389f), Quaternion.identity);
            Instantiate(_warning, new Vector2(17.579f, 14.624f), Quaternion.identity);

            Instantiate(_missile, new Vector2(18.371f, 17.389f), Quaternion.identity);
            Instantiate(_warning, new Vector2(18.371f, 14.624f), Quaternion.identity);

            Instantiate(_missile, new Vector2(18.0f, 17.389f), Quaternion.identity);
            Instantiate(_warning, new Vector2(18.0f, 14.624f), Quaternion.identity);
        }
    }

    private IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(2f);

        _coolingAttack = false;
    }
}
