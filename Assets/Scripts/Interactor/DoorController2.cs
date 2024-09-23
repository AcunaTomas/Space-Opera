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
        try
        {
            if (GetComponent<Animator>().GetFloat("HowManyHits") == 2)
            {
                _luzVentilacion.SetActive(true);
            }
        }
        catch 
        {
            Debug.Log("Null Reference Exception en DoorController2.cs, Ignorar");
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
