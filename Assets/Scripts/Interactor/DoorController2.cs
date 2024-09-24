using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorController2 : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _callSomething;
    [SerializeField]
    private bool _hasCooldown;
    private bool _coolingHit = false;

    [SerializeField]
    private GameObject _luzVentilacion;

    public GameObject _blackScreen1;
    public GameObject _blackScreen2;
    public GameObject _blackScreen3;

    public GameObject _luzExtra;


    public void changeCallback(UnityEvent a)
    {
        _callSomething = a;
    }

    public void DoSomeShit()
    {
        if (_hasCooldown)
        {
            if (!_coolingHit)
            {
                _callSomething.Invoke();
                _coolingHit = true;
                StartCoroutine(Cooldown());
            }
        }
        else
        {
            _callSomething.Invoke();
        }
        
    }

    public void IncreaseHits()
    {
        float _hitNumber = GetComponent<Animator>().GetFloat("HowManyHits");
        GetComponent<Animator>().SetFloat("HowManyHits", _hitNumber + 1);
        if (GetComponent<Animator>().GetFloat("HowManyHits") == 2)
        {
            _luzVentilacion.SetActive(true);
            _blackScreen1.GetComponent<Animator>().SetTrigger("Fade");
            _blackScreen2.GetComponent<Animator>().SetTrigger("Fade");
            _blackScreen3.GetComponent<Animator>().SetTrigger("Fade");
            _luzExtra.SetActive(false);
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.3f);
        _coolingHit = false;
    }

    public void EnemiesCounter()
    {
        float _enemies = GetComponent<Animator>().GetFloat("DeadEnemies");
        GetComponent<Animator>().SetFloat("DeadEnemies", _enemies + 1);
    }

}
