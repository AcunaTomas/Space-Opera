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
    public bool iselevatorup;

    public GameObject pisomundo;
    public GameObject pisoascensor;
    private PolygonCollider2D pisoreal;

    private bool isActive = false;
    public AudioCheck _audioCheck;
    public enum AudioCheck
    {
        ascensor,
        pinchos,
        nave
    }

    void Start()
    {
        switch (_audioCheck)
        {
            case AudioCheck.ascensor:

                pisoreal = transform.GetChild(0).GetChild(0).GetComponent<PolygonCollider2D>();
                break;

            default:

                break;

        }
        
    }

    void Update() 
    {
        if (isActive)
        {
            if (_audioCheck == AudioCheck.ascensor)
            {
                pisoreal.enabled = true;
            }
            
            Moverse();
            if (transform.position.y <= downpos.position.y)
            {
                transform.position = downpos.position;
                isActive = !isActive;
                if (_audioCheck == AudioCheck.ascensor)
                {
                    pisoreal.enabled = false;
                }
                StopAudio();
            }
            else if (transform.position.y >= upperpos.position.y)
            {
                transform.position = upperpos.position;
                isActive = !isActive;
                if (_audioCheck == AudioCheck.ascensor)
                {
                    pisoreal.enabled = false;
                }
                StopAudio();
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
            SwitchAudio();
        }
        else if (transform.position.y >= upperpos.position.y)
        {
            iselevatorup = true;
            transform.position = upperpos.position;
            SwitchAudio();
        }
    }

    void SwitchAudio()
    {
        switch (_audioCheck)
        {
            case AudioCheck.ascensor:
                AudioManager.INSTANCE.PlayElevatorInteractor();
                AudioManager.INSTANCE.PlayElevator();
                pisomundo.SetActive(false);
                pisoascensor.SetActive(true);
                break;
            case AudioCheck.pinchos:
                AudioManager.INSTANCE.PlayPinchos();
                break;  
            case AudioCheck.nave:
                AudioManager.INSTANCE.PlayElevatorNave();
                break;            
            default:
                break;
        }

    }

    void StopAudio()
    {
        switch (_audioCheck)
        {
            case AudioCheck.ascensor:
                AudioManager.INSTANCE.StopElevator();
                pisomundo.SetActive(true);
                pisoascensor.SetActive(false);
                break;            
            default:
                break;
        }
    }

}
