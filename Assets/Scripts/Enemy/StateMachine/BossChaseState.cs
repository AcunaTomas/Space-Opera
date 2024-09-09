using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BossChaseState : State
{
    bool coolingHitRef, canAttack;
    float distance;
    BossBehaviourNew enemyBehaviour;

    public GameObject bossPosition1;
    public GameObject bossPosition2;
    public GameObject bossPosition3;

    public int _randomNum;
    public int _lastRandom;

    void Start()
    {
        //enemyBehaviour = enemy.GetComponent<BossBehaviourNew>();
    }

    public override void Do()
    {
        coolingHitRef = enemy.GetComponent<Enemy>()._coolingHit;
        enemyBehaviour.Flip();

            _randomNum = Random.Range(1,3);
            animator.SetBool("Run", true);

            if (_randomNum == 1 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
            {
                enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, bossPosition1.transform.position, enemyBehaviour._speed * Time.deltaTime);
                _lastRandom = 1;
            }
            if (_randomNum == 2 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
            {
                enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, bossPosition2.transform.position, enemyBehaviour._speed * Time.deltaTime);
                _lastRandom = 2;
            }
            if (_randomNum == 3 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
            {
                enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, bossPosition3.transform.position, enemyBehaviour._speed * Time.deltaTime);
                _lastRandom = 3;
            }

        if (enemyBehaviour.cooling == false) //&& !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Scientist_turn") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_idle"))
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
        if (!coolingHitRef)
        {
            animator.SetBool("Run", true);
            Vector2 targetPosition = new Vector2(enemyBehaviour.target.position.x, enemy.transform.position.y);
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, targetPosition, enemyBehaviour.moveSpeed * Time.deltaTime);
        }
    }

    public override void Exit()
    {
        animator.SetBool("Run", false);
        //enemyBehaviour.ChangeState(enemyBehaviour.attackState);
    }

    void StopRun()
    {
        animator.SetBool("Run", false);
        enemyBehaviour.idleState.PlayerKilled();
        //enemyBehaviour.ChangeState(enemyBehaviour.idleState);
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
