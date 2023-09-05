using System.Collections;
using System.Collections.Generic;
using Shinjingi;
using UnityEngine;

public class CombatBro : PlayerCombat
{
    public Animator _animator;

    [SerializeField]
    private GameObject balarda;
    [SerializeField]
    private LayerMask discoverable;


    public override void Bomb()
    {
        var a = Instantiate(balarda, transform.position, Quaternion.identity);
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
         return;   
        }
        a.GetComponent<GenericBala>().SetDirection(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
    }
    public override void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, 10, discoverable);
        foreach (Collider2D enemyCollider in hitEnemies)
        {
            Debug.Log("asdasdasdas");
            try
            {
                enemyCollider.GetComponent<DetectableController>().Detectado();
            }
            catch (System.Exception)
            {
                enemyCollider.GetComponent<DetectableController2>().Detectado();
            }
            
        }

        _animator.SetTrigger("Attack");
        
    }

    public override void AttackUp()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, 10, discoverable);
        foreach (Collider2D enemyCollider in hitEnemies)
        {
            Debug.Log("asdasdasdas");
            try
            {
                enemyCollider.GetComponent<DetectableController>().Detectado();
            }
            catch (System.Exception)
            {
                enemyCollider.GetComponent<DetectableController2>().Detectado();
            }
        }

        _animator.SetTrigger("Attack");
    }

    public override void AttackDown()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, 10, discoverable);
        foreach (Collider2D enemyCollider in hitEnemies)
        {
            Debug.Log("asdasdasdas");
            try
            {
                enemyCollider.GetComponent<DetectableController>().Detectado();
            }
            catch (System.Exception)
            {
                enemyCollider.GetComponent<DetectableController2>().Detectado();
            }
        }

        _animator.SetTrigger("Attack");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
