using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    private enum _eventType
    { 
        Spawn,
        Move,
        Teleport,
        Custom
    
    
    }


    [SerializeField]
    private string event_name;

    [SerializeField]
    private bool single_use = true;

    [SerializeField]
    private _eventType options =  new _eventType();
    

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trig");
        if (other.gameObject.tag  == "Player")
        {
            doTheThing(options);
        }
    }


    void doTheThing(_eventType thing)
    {
        switch (thing)
        {
            case _eventType.Spawn:
                {
                    print("Spawn");
                    break;
                }
            case _eventType.Move:
                {
                    print("Move");
                    break;
                }
            case _eventType.Teleport:
                {
                    print("Teleport");
                    break;
                }
            case _eventType.Custom:
                {
                    print("Custom");
                    break;
                }
        }
    }
}
