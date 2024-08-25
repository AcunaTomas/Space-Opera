using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasuraParent : MonoBehaviour
{
cintaMove[] childs;

Rigidbody2D[] rb;

public void Activate()
{
    childs = GetComponentsInChildren<cintaMove>();
    foreach (cintaMove child in childs)
        {
            child.enabled = true;
    }
}

public void Deactivate()
{
    rb = GetComponentsInChildren<Rigidbody2D>();
    foreach (Rigidbody2D childRB in rb)
        {
            childRB.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}

}

