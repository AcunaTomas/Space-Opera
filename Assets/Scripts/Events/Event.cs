using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public bool single_use = true;

    
    public eventType options =  new eventType();
    

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trig");
        if (other.gameObject.tag  == "Player")
        {
            doTheThing(options);
        }
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
                    //Replace this with a custom scene loader
                    StartCoroutine(fade());
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
                    print("Custom");
                    break;
                }
        }

        if (single_use)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator fade()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }
}