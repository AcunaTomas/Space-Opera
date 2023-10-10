using System.Collections;
using System.Collections.Generic;
using Shinjingi;
using UnityEngine;

public class CombatBro : PlayerCombat
{
    public Animator _animator;

    public BrodyOval _radar;

    public GameObject _player;

    [SerializeField]
    private GameObject balarda;
    [SerializeField]
    private LayerMask discoverable;


    public override void Bomb()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 30, discoverable);
        foreach (Collider2D enemyCollider in hitEnemies)
        {
            try
            {
                enemyCollider.GetComponent<DetectableController>().Detectado();
            }
            catch (System.Exception)
            {
                enemyCollider.GetComponent<DetectableController2>().Detectado();
            }
        }

        _radar.enabled = true;
    }
    public override void Attack()
    {
        if (spriteRenderer.flipX)
        {
            attackPoint.localPosition = new Vector2(-0.15f, 0);
        }
        else
        {
            attackPoint.localPosition = new Vector2(0.15f, 0);
        }   

        _animator.SetTrigger("Attack");

        var a = Instantiate(balarda, attackPoint.position, Quaternion.identity);
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
         return;   
        }
        a.GetComponent<GenericBala>().SetDirection(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));

        
    }

    public override void AttackUp()
    {
        var a = Instantiate(balarda, transform.position, Quaternion.identity);
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
         return;   
        }
        a.GetComponent<GenericBala>().SetDirection(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));

        if (Input.GetAxis("Horizontal") != 0)
        {
            _animator.SetTrigger("AttackDiagUp");
        }
        else
        {
            _animator.SetTrigger("AttackUp");
        }
    }

    public override void AttackDown()
    {
        var a = Instantiate(balarda, transform.position, Quaternion.identity);
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
         return;   
        }
        a.GetComponent<GenericBala>().SetDirection(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));

        
        if (Input.GetAxis("Horizontal") != 0)
        {
            _animator.SetTrigger("AttackDiagDown");
        }
        else
        {
            _animator.SetTrigger("AttackDown");
        }

        _animator.SetBool("IsJumping", false);
        _player.GetComponent<Player>()._fallingTime = 0f;
        _player.GetComponent<Player>()._coolingShootDown = true;
        _player.GetComponent<Player>().StartCooldownShoot();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
