using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{

    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;
    public GameObject playerInstance;
    public  float _XDistance = 0.3f;
    public float _YDistance = 0.3f;
    public float _timePressed = 0f; 

    public bool moved = false;
    public bool timedDisable = false;
    public float disableIn = 0;
    public float moveAmount = 0;
    public float _tempX;
    public float _tempY;
    public bool _keyHeld = false;

    public bool waveMode;

    public UnityEvent afterAction;
    public float _waitTime;

    public UnityEvent afterafterAction;
    public float _afterwaitTime;


    void FixedUpdate()
    {
        playerInstance = GameManager.INSTANCE.GetActivePlayer().gameObject;
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
        print(moveAmount);
        if (moveAmount > 0)
        {
            moveAmount -= 1;
            return;
        }
/*        _XDistance = _tempX;
        _YDistance = _tempY;*/
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

        if (Input.GetAxis("Submit") > 0 && _keyHeld == true) //|| Input.GetAxis(GameManager.INSTANCE.ALTSKIPENABLED) > 0) 
        {

        }
        else
        {
           
            _keyHeld = false;
        }


        if ((Mathf.Abs(playerInstance.gameObject.transform.position.x -transform.position.x)  <= _XDistance &&  Mathf.Abs(playerInstance.gameObject.transform.position.y - transform.position.y)  <= _YDistance) && !moved)
        {
            if (Input.GetAxis("Submit") > 0 && _timePressed <= 0)  //|| Input.GetAxis(GameManager.INSTANCE.ALTSKIPENABLED) > 0) 
            {
                interactAction.Invoke();
                StartCoroutine(DoAfter());
                _keyHeld = true;
                disableCheck();
            }
        }
        else
        {
            if (moved)
            {
                if (Input.GetAxis("Submit") > 0 && _timePressed <= 0) //|| Input.GetAxis(GameManager.INSTANCE.ALTSKIPENABLED) > 0) 
                {
                    interactAction.Invoke();
                    _keyHeld = true;

                    restoreMove();
                    disableCheck();
                }
            }
        }



    }

    public void disableCheck()
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

    public void SetXRange(float x)
    {
        _XDistance = x;
    }
    public void SetYRange(float y)
    {
        _YDistance = y;
    }


    private void EndWaveMode()
    {
        waveMode = false;
    }


    public IEnumerator DoAfter()
    {

        yield return new WaitUntil(() => waveMode == false);

        yield return new WaitForSeconds(_waitTime);
        afterAction.Invoke();

        yield return new WaitForSeconds(_afterwaitTime);
        afterafterAction.Invoke();
    }





    
}

