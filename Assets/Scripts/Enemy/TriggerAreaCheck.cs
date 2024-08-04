using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaCheck : MonoBehaviour
{
    private EnemyBehaviour2 enemyParent;
    public GameObject _alert;

    private void Awake()
    {
        enemyParent = GetComponentInParent<EnemyBehaviour2>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player")) 
        {

            enemyParent.animator.SetBool("Alert", true);
            var _exclamationMark = Instantiate(_alert, enemyParent.gameObject.transform);
            Destroy(_exclamationMark, 0.5f);

            gameObject.SetActive(false);
            enemyParent.target = collider.transform;
            enemyParent.inRange = true;
            //enemyParent.hotZone.SetActive(true);
        }
    }
}
