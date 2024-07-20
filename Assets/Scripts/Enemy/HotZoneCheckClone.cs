using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotZoneCheckClone : MonoBehaviour
{
    private EnemyCloneBehaviour enemyParent;
    private bool inRange;
    private Animator animator;

    private void Awake()
    {
        enemyParent = GetComponentInParent<EnemyCloneBehaviour>();
        animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (enemyParent.inRange && !animator.GetCurrentAnimatorStateInfo(0).IsName("Clone_attack"))
        {
            enemyParent.Flip();
        }
    }

     private void OnTriggerEnter2D(Collider2D collider)
     {
         if (collider.gameObject.CompareTag("Player"))
         {
            enemyParent.inRange = true;
         }
    }


    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            enemyParent.inRange = false;
            gameObject.SetActive(false);
            enemyParent.triggerArea.SetActive(true);
            enemyParent.inRange = false;
            enemyParent.animator.SetTrigger("Confused");
            animator.SetBool("Run", false);
        }
    }
}
