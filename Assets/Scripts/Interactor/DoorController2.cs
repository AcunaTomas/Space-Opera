using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FMODUnity;

public class DoorController2 : BasicHittableEntity
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

    [field: Header("Sounds")]
    [field: SerializeField] public EventReference DoorGetHit { get; private set; }
    [field: SerializeField] public EventReference DoorIsBreaking { get; private set; }



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


    void OnCollisionEnter2D(Collision2D other)
    {
        if (LayersCheckAllowed(1 << other.gameObject.layer))
        {
            DoSomeShit();
        }
    }

    public void IncreaseHits()
    {
        float _hitNumber = GetComponent<Animator>().GetFloat("HowManyHits");
        AudioController.instance.PlayOneShot(DoorGetHit, this.transform.position);
        GetComponent<Animator>().SetFloat("HowManyHits", _hitNumber + 1);
        if (GetComponent<Animator>().GetFloat("HowManyHits") == 2)
        {
            AudioController.instance.PlayOneShot(DoorIsBreaking, this.transform.position);
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
