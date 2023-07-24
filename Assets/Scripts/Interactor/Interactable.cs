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

    bool moved = false;
    bool timedDisable = false;
    float disableIn = 0;
    float moveAmount = 0;
    float _tempX;
    float _tempY;
    bool _keyHeld = false;
    
    void Start()
    {
        playerInstance = GameObject.FindWithTag("Player");
    }

    public void TemporaryMove(float x)
    {
        /* _tempX = _XDistance;
         _tempY = _YDistance;

         _XDistance = x;
         _YDistance = x; */
        moveAmount = x;
        moved = true;
    }

    public void disableAfter(float x)
    {
        if (timedDisable == true)
        {
            return;
        }
        timedDisable = true;
        disableIn = x;
    }

    public void restoreMove()
    {
        if (moveAmount > 0)
        {
            moveAmount -= 1;
            return;
        }
        _XDistance = _tempX;
        _YDistance = _tempY;
        moved = false;
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

        if ((Input.GetAxis("Submit") > 0) && _keyHeld == true)
        {
            //Debug.Log("hold");

        }
        else
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
                disableCheck();
            }
        }
        else
        {
            if (moved)
            {
                if ((Input.GetKeyDown(interactKey) || Input.GetAxis("Submit") > 0) && _timePressed <= 0)
                {
                    interactAction.Invoke();
                    Debug.Log("Interact");
                    _keyHeld = true;

                    restoreMove();
                    disableCheck();
                }
            }
        }

    }

    void disableCheck()
    {
        if (timedDisable == false)
        {
            return;
        }
        if (disableIn > 0)
        {
            disableIn -= 1;
            return;
        }
        gameObject.GetComponent<Interactable>().enabled = false;
    }
}

