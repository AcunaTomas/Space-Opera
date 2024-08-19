using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeChaseState : State
{
    bool coolingHitRef, canAttack;
    float distance;
    EnemyShooting2 enemyBehaviour;

    void Start()
    {
        enemyBehaviour = enemy.GetComponent<EnemyShooting2>();
    }

    public override void Do()
    {
        coolingHitRef = enemy.GetComponent<Enemy>()._coolingHit;
        enemyBehaviour.Flip();
        if (Vector2.Distance(enemy.transform.position, enemyBehaviour.target.position) > enemyBehaviour._distanciaFrenado)
        {
            animator.SetBool("Run", true);
            animator.SetBool("Alert", true);
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Scientist_turn") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
            {
                enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, enemyBehaviour.target.position, enemyBehaviour._speed * Time.deltaTime);
            }
        }

        if (Vector2.Distance(enemy.transform.position, enemyBehaviour.target.position) < enemyBehaviour._distanciaRetroceso)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Scientist_turn") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
            {
                enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, enemyBehaviour.target.position, -enemyBehaviour._speed * Time.deltaTime);
            }
            
            animator.SetBool("Run", true);
            //animator.SetBool("Alert", true);
                
        }

        if ((Vector2.Distance(enemy.transform.position, enemyBehaviour.target.position) < enemyBehaviour._distanciaFrenado) && (Vector2.Distance(enemy.transform.position, enemyBehaviour.target.position) > enemyBehaviour._distanciaRetroceso))
        {
            //transform.position = transform.position;
            animator.SetBool("Run", false);
        }

        if (enemyBehaviour.cooling == false && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Scientist_turn") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_idle"))
        {
            if (enemyBehaviour.timerAttack > enemyBehaviour.attackDelay && canAttack)
            {
                Exit();
            }
        }

        if (enemyBehaviour.cooling)
        {
            enemyBehaviour.attackState.Cooldown();
            animator.SetBool("Attack", false);
        }
        
        if (player.GetComponent<Player>().GetHP() <= 0)
        {
            //Exit();
            StopRun();
        }
    }
    void Move()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack") && !coolingHitRef && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_confused") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_alert"))
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
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }
    }
}
