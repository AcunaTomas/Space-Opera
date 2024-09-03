using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchoMataEnemy : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().Die();
        }
    }
}
