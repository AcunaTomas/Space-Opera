using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotZoneCheckShield : MonoBehaviour
{
    private EnemyShieldBehaviour enemyParent;
    private bool inRange;
    private Animator animator;

    private void Awake()
    {
        enemyParent = GetComponentInParent<EnemyShieldBehaviour>();
        animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (inRange && !animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyShield_attack"))
        {
            enemyParent.Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            inRange = false;
            gameObject.SetActive(false);
            enemyParent.StartCooldownTrigger();
            //enemyParent.triggerArea.SetActive(true);
            enemyParent.gameObject.layer = LayerMask.NameToLayer("Enemy");
            enemyParent.inRange = false;
            animator.SetBool("Run", false);
        }
    }
}