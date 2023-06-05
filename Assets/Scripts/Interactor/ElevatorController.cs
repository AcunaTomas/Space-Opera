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
    
    void Start() 
    {
        
    }

    void Update() 
    {
        Moverse();
    }

    void Moverse()
    {
        if (Vector2.Distance(player.position, elevatorSwitch.position)<0.5f && Input.GetKeyDown ("e"))
        {
            if (transform.position.y <= downpos.position.y)
            {
                iselevatorup = false;
            }
            else if (transform.position.y >= upperpos.position.y)
            {
                iselevatorup = true;
            }

        }

        if (iselevatorup)
        {
            transform.position = Vector2.MoveTowards (transform. position, downpos.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards (transform. position, upperpos.position, speed * Time.deltaTime);
        }

    }

}
