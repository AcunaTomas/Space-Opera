using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : State
{
    // Start is called before the first frame update
    bool coolingHitRef;
    float distance;
    EnemyBehaviour2 enemyBehaviour;
    private bool attackMode;
    private float timerAttack, attackDelay = 1f, intTimer;

    void Start()
    {
        enemyBehaviour = enemy.GetComponent<EnemyBehaviour2>();
        intTimer = enemyBehaviour.timer;
    }

    public override void Do()
    {
        coolingHitRef = enemy.GetComponent<Enemy>()._coolingHit;
        enemyBehaviour.Flip();
        if(!enemyBehaviour.DONTMOVE || !coolingHitRef)
        {
            distance = Vector2.Distance(enemy.transform.position, enemyBehaviour.target.position);
        }
        if (distance > enemyBehaviour.attackDistance && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
        {   
            Exit();
        }
        else if (enemyBehaviour.attackDistance >= distance && enemyBehaviour.cooling == false && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
        {
            animator.SetBool("Run", false);
            timerAttack += Time.deltaTime;
            if (timerAttack > attackDelay)
            {
                Attack();
            }
            //Attack();
        }

        if (enemyBehaviour.cooling)
        {
            Cooldown();
            animator.SetBool("Attack", false);
        }

        if (player.GetComponent<Player>().GetHP() <= 0)
        {
            //Exit();
            StopAttack();
        }
    }

    void Attack()
    {
        enemyBehaviour.timer = intTimer;
        attackMode = true;

        animator.SetBool("Run", false);
        animator.SetBool("Attack", true);
        timerAttack = 0;

        StartCoroutine(AtaquePosta());
    }
    void StopAttack() 
    {
        enemyBehaviour.cooling = false;
        attackMode = false;
        animator.SetBool("Attack", false);
        enemyBehaviour.idleState.PlayerKilled();
        enemyBehaviour.ChangeState(enemyBehaviour.idleState);
    }

    public override void Exit()
    {
        enemyBehaviour.ChangeState(enemyBehaviour.chaseState);
    }

    IEnumerator AtaquePosta()
    {
        yield return new WaitForSeconds(0.7f);
        AudioController.instance.PlayOneShot(FMODEvents.instance.EnemiesAttack, this.transform.position);
        if (transform.position.x > enemyBehaviour.target.position.x)
        {
            enemyBehaviour.attackPointFlip.gameObject.SetActive(true);
        }
        else
        {
            enemyBehaviour.attackPoint.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(0.001f);
        
        animator.SetBool("Attack", false);
    }
    void Cooldown()
    {
        enemyBehaviour.timer -= Time.deltaTime;
        if (enemyBehaviour.timer <= 0 && enemyBehaviour.cooling && attackMode)
        {
            enemyBehaviour.cooling = false;
            enemyBehaviour.timer = intTimer;
        }
    }
}
