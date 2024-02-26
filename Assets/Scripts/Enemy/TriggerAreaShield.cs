using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaShield : MonoBehaviour
{
    private EnemyShieldBehaviour enemyParent;


    private void Awake()
    {
        enemyParent = GetComponentInParent<EnemyShieldBehaviour>();

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player")) 
        {
            gameObject.SetActive(false);
            enemyParent.target = collider.transform;
            enemyParent.inRange = true;
            enemyParent.gameObject.layer = LayerMask.NameToLayer("Default");

            if (enemyParent.transform.position.x > enemyParent.target.position.x)
            {
                enemyParent.hotzone_flipped.SetActive(true);
            }
            else if (enemyParent.transform.position.x < enemyParent.target.position.x)
            {
                enemyParent.hotZone.SetActive(true);
            }

        }
    }
}
