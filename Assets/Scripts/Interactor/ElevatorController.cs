using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public Transform player;
    public Transform elevatorSwitch;
    public Transform downpos;
    public Transform upperpos;

    public float speed;
    private bool iselevatorup;

    private bool isActive = false;
    
    void Start() 
    {
        
    }

    void Update() 
    {
        if (isActive)
        {
            Moverse();
            if (transform.position.y <= downpos.position.y)
            {
                iselevatorup = false;
                transform.position = downpos.position;
                isActive = !isActive;
            }
            else if (transform.position.y >= upperpos.position.y)
            {
                iselevatorup = true;
                transform.position = upperpos.position;
                isActive = !isActive;
            }
            
        } 

    }

    void Moverse()
    {
        if (iselevatorup)
        {
            transform.position = Vector2.MoveTowards (transform. position, downpos.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards (transform. position, upperpos.position, speed * Time.deltaTime);
        }


    }

    public void Interact_Action()
    {

        if (transform.position.y < upperpos.position.y && transform.position.y > downpos.position.y)
        {

            return;
        }
        isActive = !isActive;
        
        if (transform.position.y <= downpos.position.y)
        {
            iselevatorup = false;
            transform.position = downpos.position;
        }
        else if (transform.position.y >= upperpos.position.y)
        {
            iselevatorup = true;
            transform.position = upperpos.position;
        }
    }

}
