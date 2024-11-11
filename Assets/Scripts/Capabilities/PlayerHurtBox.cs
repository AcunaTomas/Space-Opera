using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtBox : HurtBox
{
    private float dura;


    void OnEnable()
    {
        dura = DURATION;
    }
    void FixedUpdate()
    {
        if (dura > 0)
        {
            dura -= Time.deltaTime;
            return;
        }
        gameObject.SetActive(false);
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (Coll.IsTouchingLayers(targetLayer))
        {
            var Ref = other.GetComponent<Enemy>();
            if (Ref)
            {
                Ref.TakeDamage(attackDamage,transform.position);
                return;
            }
        }
    }

}
