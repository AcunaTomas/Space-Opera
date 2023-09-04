using System.Collections;
using System.Collections.Generic;
using Shinjingi;
using UnityEngine;

public class CombatBro : PlayerCombat
{
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
            enemyCollider.GetComponent<DetectableController>().Detectado();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
