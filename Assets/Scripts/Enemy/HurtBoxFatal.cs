using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HurtBoxFatal : HurtBox
{

    public override void Awake()
    {
        ChangeAttackDMG(100);
    }
}
