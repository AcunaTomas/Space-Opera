using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Event : MonoBehaviour
{
    
    public enum eventType
    { 
        Spawn,
        EndLevel,
        Teleport,
        Custom
    
    
    }

    //Spawneo de una cosa
    public Vector2 spawnLocation;
    public GameObject _thingToSpawn;

    //teleport
    public GameObject who;
    public Vector2 where;
    
    //End Level
    public string sceneName;

    public string event_name;

    //Custom
    public UnityEvent interactAction;
    public bool external;

    //Si se borra cuando se activa, por defecto es true
    public bool single_use = true;

    
    public eventType options =  new eventType();

    private GameObject _player;
    
    

    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        //Physics2D.IgnoreCollision(_player.GetComponent<CircleCollider2D>(), GetComponent<Collider2D>());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("trig");
        if (other.gameObject.tag  == "Player")
        {
            doTheThing(options);
        }
    }

    void manualDo()
    {
        doTheThing(options);
    }
    
    void doTheThing(eventType thing)
    {
        switch (thing)
        {
            case eventType.Spawn:
                {
                    var a = Instantiate(_thingToSpawn);
                    a.transform.position = spawnLocation;
                    break;
                }
            case eventType.EndLevel:
                {
                    print("Level Ended");
                    ScenesManager.Instance.LoadNextScene(sceneName); //Replace with an actual scene loader/handler for transitions.
                    break;
                }
            case eventType.Teleport:
                {
                    print("Teleport");
                    print(who);
                    print(where);
                    who.gameObject.transform.position = where;
                    break;
                }
            case eventType.Custom:
                {
                    if (external)
                    {
                        Debug.LogWarning("You tried to trigger an external event with a BoxCollider2D");
                        break;
                    }
                    interactAction.Invoke();

                    break;
                }
        }

        if (single_use)
        {
            Destroy(gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        switch (options)
        {
            case eventType.Spawn:
            {
                Gizmos.color = new Color(1, 0, 0, 0.5F);
                Gizmos.DrawSphere(spawnLocation, 0.2f);
                break;
            }
            case eventType.Teleport:
            {
                    Gizmos.color = new Color(0, 1, 0, 0.5F);
                    Gizmos.DrawSphere(where, 0.2f);
                    break;

            }


        
        }


    }
}
