using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerScript : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _callWhat;

    [SerializeField]
    private float _time = 5;

    void Awake()
    {
       StartCoroutine(QuePaseEltiempo());
    }


    IEnumerator QuePaseEltiempo()
    {
        yield return new WaitForSeconds(_time);
        Debug.Log("I would like to die pls");
        _callWhat.Invoke();

    }



}
