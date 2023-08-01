using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLayers : MonoBehaviour
{
    public void EnemyLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("Enemy");
    }

    public void DefaultLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
    }
    
}
