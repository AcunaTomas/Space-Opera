using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BreakableWall : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _callSomething;
    public void changeCallback(UnityEvent a)
    {
        _callSomething = a;
    }

    public void DoSomeShit()
    {
        _callSomething.Invoke();
    }

}
