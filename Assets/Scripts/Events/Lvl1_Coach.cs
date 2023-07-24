using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lvl1_Coach : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    public void Lever()
    {
        animator.SetTrigger("Lever");
    }

}
