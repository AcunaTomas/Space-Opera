using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCheck : MonoBehaviour
{
    [SerializeField]
    private GameObject _elevator;

    public void CallUp()
    {
        if (_elevator.GetComponent<ElevatorController>().iselevatorup == true)
        {
            _elevator.GetComponent<ElevatorController>().Interact_Action();
        }
    }

    public void CallDown()
    {
        if (_elevator.GetComponent<ElevatorController>().iselevatorup == false)
        {
            _elevator.GetComponent<ElevatorController>().Interact_Action();
        }
    }
}
