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
        AchievementsManager.INSTANCE.Achievement03();
        _radar.enabled = true;

        _player.GetComponent<Player>().enabled = false;
        _player.GetComponent<CombatBro>().enabled = false;
        _player.GetComponent<Player>().body.velocity = new Vector2(0f, _player.GetComponent<Player>().body.velocity.y);
        StartCoroutine(StartCooldownMovement());
    }
    public override void Attack()
    {
        if (spriteRenderer.flipX)
        {
            attackPoint.localPosition = new Vector2(-0.25f, 0);
        }
        else
        {
            attackPoint.localPosition = new Vector2(0.25f, 0);
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
        
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
         return;   
        }
        

        if (Input.GetAxis("Horizontal") != 0)
        {
            _animator.SetTrigger("AttackDiagUp");
            if (spriteRenderer.flipX)
            {
                attackPoint.localPosition = new Vector2(-0.13f, 0.1f);
            }
            else
            {
                attackPoint.localPosition = new Vector2(0.13f, 0.1f); ;
            }
            var a = Instantiate(balarda, attackPoint.position, Quaternion.identity);
            a.GetComponent<GenericBala>().SetDirection(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        else
        {
            _animator.SetTrigger("AttackUp");

            if (spriteRenderer.flipX)
            {
                attackPoint.localPosition = new Vector2(0.04f, 0.17f);
            }
            else
            {
                attackPoint.localPosition = new Vector2(-0.04f, 0.17f);
            }
            var a = Instantiate(balarda, attackPoint.position, Quaternion.identity);
            a.GetComponent<GenericBala>().SetDirection(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
    }

    public override void AttackDown()
    {
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
         return;   
        }

        
        if (Input.GetAxis("Horizontal") != 0)
        {
            _animator.SetTrigger("AttackDiagDown");
            if (spriteRenderer.flipX)
            {
                attackPoint.localPosition = new Vector2(-0.1f, -0.1f);
            }
            else
            {
                attackPoint.localPosition = new Vector2(0.1f, -0.1f);
            }
            var a = Instantiate(balarda, attackPoint.position, Quaternion.identity);
            a.GetComponent<GenericBala>().SetDirection(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        else
        {
            _animator.SetTrigger("AttackDown");
            if (spriteRenderer.flipX)
            {
                attackPoint.localPosition = new Vector2(0f, -0.17f);
            }
            else
            {
                attackPoint.localPosition = new Vector2(0f, -0.17f);
            }
            var a = Instantiate(balarda, attackPoint.position, Quaternion.identity);
            a.GetComponent<GenericBala>().SetDirection(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        _animator.SetBool("IsJumping", false);
        _player.GetComponent<Player>()._fallingTime = 0f;
        _player.GetComponent<Player>()._coolingShootDown = true;
        _player.GetComponent<Player>().StartCooldownShoot();
    }

    public IEnumerator StartCooldownMovement()
    {
        yield return new WaitForSeconds(1f);

        _player.GetComponent<Player>().enabled = true;
        _player.GetComponent<CombatBro>().enabled = true;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
