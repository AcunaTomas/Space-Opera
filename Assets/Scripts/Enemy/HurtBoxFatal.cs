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
    void Start()
    {
        player = GameManager.INSTANCE.GetActivePlayer().gameObject;
        Physics2D.IgnoreCollision(player.GetComponent<CircleCollider2D>(), GetComponent<Collider2D>());
    }
}
