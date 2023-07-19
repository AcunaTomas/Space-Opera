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
    float _XDistance = 0.3f;
    [SerializeField]
    float _YDistance = 0.3f;

    public float _timePressed = 0f; 

    bool _keyHeld = false;
    
    void Start()
    {
        playerInstance = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (_keyHeld == true)
        {
            _timePressed += Time.deltaTime;
        }
        else
        {
            _timePressed = 0;
        }

        if ((Input.GetAxis("Submit") > 0) && _keyHeld == true && _timePressed > 0.9f)
        {
            _keyHeld = false;
        }


        if (Mathf.Abs(playerInstance.gameObject.transform.position.x -transform.position.x)  <= _XDistance &&  Mathf.Abs(playerInstance.gameObject.transform.position.y - transform.position.y)  <= _YDistance)
        {
            if ((Input.GetKeyDown(interactKey) || Input.GetAxis("Submit") > 0) && _timePressed <= 0)
            {
                interactAction.Invoke();
                Debug.Log("Interact");
                _keyHeld = true;
            }
        }
    }
}
