using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableOnlyBrody : Interactable
{

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

        if ((Input.GetAxis("Submit") > 0 || Input.GetAxis(GameManager.INSTANCE.ALTSKIPENABLED) > 0) && _keyHeld == true)
        {

        }
        else
        {
           
            _keyHeld = false;
        }


        if ((Mathf.Abs(playerInstance.gameObject.transform.position.x -transform.position.x)  <= _XDistance &&  Mathf.Abs(playerInstance.gameObject.transform.position.y - transform.position.y)  <= _YDistance) && !moved)
        {
            if ((Input.GetAxis("Submit") > 0  || Input.GetAxis(GameManager.INSTANCE.ALTSKIPENABLED) > 0) && _timePressed <= 0 && playerInstance.GetComponent<Player>()._playerType == Player.PlayerType.Brody && playerInstance.GetComponent<PlayerCombat>()._canHack == true)
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
                if ((Input.GetAxis("Submit") > 0 || Input.GetAxis(GameManager.INSTANCE.ALTSKIPENABLED) > 0) && _timePressed <= 0 && playerInstance.GetComponent<Player>()._playerType == Player.PlayerType.Brody && playerInstance.GetComponent<PlayerCombat>()._canHack == true)
                {
                    interactAction.Invoke();
                    _keyHeld = true;

                    restoreMove();
                    disableCheck();
                }
            }
        }

    }






    
}

