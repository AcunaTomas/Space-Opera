using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private bool sube = true;

    public void Moverse()
    {
        if (sube == true)
        {
            animator.SetBool("sube", false);
            animator.SetBool("baja", true);
            sube = false;
        }
        else if (sube == false)
        {
            animator.SetBool("sube", true);
            animator.SetBool("baja", false);
            sube = true;
        }
        
    }

}
