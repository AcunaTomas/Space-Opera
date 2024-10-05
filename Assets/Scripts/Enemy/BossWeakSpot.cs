using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeakSpot : MonoBehaviour
{
    private int damage = 120;
    public void Hit()
    {
        GetComponentInParent<Enemy>().TakeDamage2(damage, transform.position);
        GetComponentInParent<Enemy>().DebugTroll(damage);
        Destroy(gameObject, 0.2f);
    }

}
