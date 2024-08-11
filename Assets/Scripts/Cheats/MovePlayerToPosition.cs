using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerToPosition : MonoBehaviour
{
    public GameObject playerToMove;
    public bool justX, justY;

    
    void Update()
    {
        if(Input.GetButtonDown("MovePlayer"))
        {
            //Debug.Log("move player");
            if(justX && !justY)
            {
                playerToMove.transform.position = new Vector2(transform.position.x, playerToMove.transform.position.y);
            }
            else if(!justX && justY)
            {
                playerToMove.transform.position = new Vector2(playerToMove.transform.position.x, transform.position.y);
            }
            else if(justX && justY)
            {
                playerToMove.transform.position = new Vector2(transform.position.x, transform.position.y);
            }
        }
    }
}
