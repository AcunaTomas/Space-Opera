using System.Collections;
using System.Collections.Generic;
using Shinjingi;
using UnityEngine;

public class CombatBro : PlayerCombat
{
    public Animator _animator;

    public BrodyOval _radar;

    public GameObject _player;

    public BalasPool Balalist;

    
    [SerializeField]
    private GameObject balarda;
    [SerializeField]
    private LayerMask discoverable;

    private float attkdirX;
    private float attkdirY;

    public override void Bomb()
    {

    }
    public override void Attack()
    {
        if (spriteRenderer.flipX)
        {
            attackPoint.localPosition = new Vector2(-0.25f, 0);
            attkdirX = -1;
        }
        else
        {
            attackPoint.localPosition = new Vector2(0.25f, 0);
            attkdirX = 1;
        }   

        _animator.SetTrigger("Attack");
        SpawnBala(attkdirX,0);
    }

    private void SpawnBala(float X, float Y)
    {
        
        var a = Balalist.GetObj();
        a.transform.position = attackPoint.position;
        a.transform.rotation = Quaternion.identity;
        a.GetComponent<GenericBala>().SetDirection(X,Y);
        a.SetActive(true);
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
                attkdirX = -1;
                attkdirY = 1;
            }
            else
            {
                attackPoint.localPosition = new Vector2(0.13f, 0.1f);
                attkdirX = 1;
                attkdirY = 1;
            }
            SpawnBala(attkdirX,attkdirY);
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
            SpawnBala(0,1);
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
                attkdirX = -1;
                attkdirY = -1;
            }
            else
            {
                attackPoint.localPosition = new Vector2(0.1f, -0.1f);
                attkdirX = 1;
                attkdirY = -1;
            }
            SpawnBala(attkdirX,attkdirY);
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
            SpawnBala(0,-1);
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



    public void hackBool(bool a)
    {
        _canHack = a;
    }
}
