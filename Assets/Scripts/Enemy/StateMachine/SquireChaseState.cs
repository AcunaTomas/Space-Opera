using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquireChaseState : State
{
    bool coolingHitRef;
    float distance;
    SquireBehaviour enemyBehaviour;

    void Start()
    {
        enemyBehaviour = enemy.GetComponent<SquireBehaviour>();
        enemyBehaviour.attackPoint.gameObject.SetActive(false);
        enemyBehaviour.attackPointFlip.gameObject.SetActive(false);
    }

    public override void Do()
    {
        coolingHitRef = enemy.GetComponent<Enemy>()._coolingHit;
        //enemyBehaviour.Flip();
        if(!enemyBehaviour.DONTMOVE || !coolingHitRef)
        {
            distance = Vector2.Distance(enemy.transform.position, enemyBehaviour.target.position);
            if(enemy.transform.position.y + .5f < enemyBehaviour.target.position.y && enemyBehaviour.grounded)
            {
                //enemy.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5f);
            }
        }
        if (distance > enemyBehaviour.attackDistance && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
        {   
            Move();
        }
        if(!enemyBehaviour.GetPhase())
        {
            if (!enemyBehaviour.cooling && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_idle"))
            {
                if (enemyBehaviour.timerAttack > enemyBehaviour.attackDelay)
                {
                    Exit();
                }
            }
        }
        else
        {
            if(enemyBehaviour.attackDistance >= distance && !enemyBehaviour.cooling && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
            {
                Exit();
            }
        }
        
        
        if (player.GetComponent<Player>().GetHP() <= 0)
        {
            //Exit();
            StopRun();
        }
        if (enemyBehaviour.cooling)
        {
            enemyBehaviour.attackState.Cooldown();
            enemyBehaviour.attackState.RangeCooldown();
            animator.SetBool("Attack", false);
        }
    }
    void Move()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack") && !enemyBehaviour.DONTMOVE && !coolingHitRef && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_confused") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_alert"))
        {
            animator.SetBool("Run", true);
            Vector2 targetPosition = new Vector2(enemyBehaviour.target.position.x, enemy.transform.position.y);
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, targetPosition, enemyBehaviour.moveSpeed * Time.deltaTime);
        }
    }

    public override void Exit()
    {
        animator.SetBool("Run", false);
        enemyBehaviour.ChangeState(enemyBehaviour.attackState);
    }

    void StopRun()
    {
        animator.SetBool("Run", false);
        enemyBehaviour.idleState.PlayerKilled();
        enemyBehaviour.ChangeState(enemyBehaviour.idleState);
    }
}
