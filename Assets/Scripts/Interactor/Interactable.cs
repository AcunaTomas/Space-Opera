using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    private bool isInRange;

    [SerializeField]
    private KeyCode interactKey;

    [SerializeField]
    private UnityEvent interactAction;

    [SerializeField]
    GameObject playerInstance;
    void Start()
    {
        playerInstance = GameObject.FindWithTag("Player");
    }

    
    void Update()
    {
        if ( Mathf.Abs(playerInstance.gameObject.transform.position.x -transform.position.x)  <= 1f &&  Mathf.Abs(playerInstance.gameObject.transform.position.x - transform.position.x)  <=1f)
        {
            //Debug.Log(Mathf.Abs(playerInstance.gameObject.transform.position.x -transform.position.x));
            //Debug.Log(Mathf.Abs(playerInstance.gameObject.transform.position.x - transform.position.x));
            if (Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();
            }
        }
    }

/*     void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
        }
    } */
}
