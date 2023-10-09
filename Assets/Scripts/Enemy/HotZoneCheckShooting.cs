using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotZoneCheckShooting : MonoBehaviour
{
    private EnemyShooting2 enemyParent;
    private bool inRange;
    private Animator animator;

    private void Awake()
    {
        enemyParent = GetComponentInParent<EnemyShooting2>();
        animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (inRange && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
        {
            enemyParent.Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            inRange = true;
            enemyParent.inRange = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            Debug.Log("OUT OF RANGE");
            inRange = false;
            enemyParent.target = null;
            enemyParent.triggerArea.SetActive(true);
            enemyParent.inRange = false;
            animator.SetBool("Run", false);
        }
    }
}
