using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerScript : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _callWhat;

    void Awake()
    {
       StartCoroutine(QuePaseEltiempo());
    }


    IEnumerator QuePaseEltiempo()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("I would like to die pls");
        _callWhat.Invoke();

    }



}
