using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    public void Open()
    {
        animator.SetTrigger("OpenDoor");
    }

    public void Close()
    {
        animator.SetTrigger("CloseDoor");
    }

    public void BoxCollider2DNoMore()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
