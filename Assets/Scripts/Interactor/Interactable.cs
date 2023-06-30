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
    [SerializeField]
    float _XDistance;
    [SerializeField]
    float _YDistance;
    void Start()
    {
        playerInstance = GameObject.FindWithTag("Player");
    }


    void Update()
    {
        _XDistance = Mathf.Abs(playerInstance.gameObject.transform.position.x - transform.position.x);
        _YDistance = Mathf.Abs(playerInstance.gameObject.transform.position.y - transform.position.y);
        if ( Mathf.Abs(playerInstance.gameObject.transform.position.x -transform.position.x)  <= 0.5 &&  Mathf.Abs(playerInstance.gameObject.transform.position.y - transform.position.y)  <= 0.5f)
        {
            //Debug.Log(Mathf.Abs(playerInstance.gameObject.transform.position.x -transform.position.x));
            //Debug.Log(Mathf.Abs(playerInstance.gameObject.transform.position.x - transform.position.x));
            if (Input.GetKeyDown(interactKey) || Input.GetAxis("Submit") > 0)
            {
                interactAction.Invoke();
                Debug.Log("Interact");
            }
        }
    }

    //  void OnTriggerEnter2D(Collider2D collision) 
    // {
    //     if (collision.gameObject.CompareTag("Player"))
    //     {
    //         isInRange = true;
    //     }
    // }

    // void OnTriggerExit2D(Collider2D collision) 
    // {
    //     if (collision.gameObject.CompareTag("Player"))
    //     {
    //         isInRange = false;
    //     }
    // } 
}
