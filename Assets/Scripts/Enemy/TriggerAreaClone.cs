using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaClone : MonoBehaviour
{
    private EnemyCloneBehaviour enemyParent;

    private bool _firstAlert = false;
    //public GameObject _alert;

    private void Awake()
    {
        enemyParent = GetComponentInParent<EnemyCloneBehaviour>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player")) 
        {
            if (_firstAlert == false)
            {
                enemyParent.animator.SetBool("Alert", true);
                _firstAlert = true;
            }
            
            //var _exclamationMark = Instantiate(_alert, enemyParent.gameObject.transform);
            //Destroy(_exclamationMark, 0.5f);

            gameObject.SetActive(false);
            enemyParent.target = collider.transform;
            enemyParent.inRange = true;
            enemyParent.hotZone.SetActive(true);
        }
    }
}
