using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackState : State
{
    // Start is called before the first frame update
    bool coolingHitRef;
    float distance;
    EnemyShooting2 enemyBehaviour;
    private bool attackMode;
    private float timerAttack, attackDelay = 1f, intTimer;

    void Start()
    {
        enemyBehaviour = enemy.GetComponent<EnemyShooting2>();
        intTimer = enemyBehaviour.timer;
    }

    public override void Do()
    {
        coolingHitRef = enemy.GetComponent<Enemy>()._coolingHit;
        enemyBehaviour.Flip();
        if (enemyBehaviour.cooling == false && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Scientist_turn") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_idle"))
        {
            if (enemyBehaviour.timerAttack > enemyBehaviour.attackDelay)
            {
                Attack();
            }
        }

        if (enemyBehaviour.cooling)
        {
            Cooldown();
            animator.SetBool("Attack", false);
        }

        if (player.GetComponent<Player>().GetHP() <= 0)
        {
            StopAttack();
        }
        
    }

void Attack()
{
    enemyBehaviour.timer = intTimer;
    attackMode = true;

    animator.SetBool("Run", false);
    animator.SetBool("Attack", true);
    animator.SetTrigger("Shoot");
    Instantiate(enemyBehaviour._bullet, enemyBehaviour._bulletSource.position, Quaternion.identity);
    enemyBehaviour._bullet.SetActive(true);
    //AudioManager.INSTANCE.PlayEnemyAttack();  SONIDO DE ATAQUE

    //attackPoint.gameObject.SetActive(true);
    enemyBehaviour.timerAttack = 0;
    Exit();
}

public void Cooldown()
{
    enemyBehaviour.timer -= Time.deltaTime;

    if (enemyBehaviour.timer <= 0 && enemyBehaviour.cooling && attackMode)
    {
        enemyBehaviour.cooling = false;
        enemyBehaviour.timer = intTimer;
    }

    if (enemyBehaviour.timer < 0 && !attackMode)
    {
        enemyBehaviour.cooling = false;
        enemyBehaviour.timer = intTimer;
    }
}


void StopAttack() 
{
    attackMode = false;
    animator.SetBool("Attack", false);
    animator.SetTrigger("Idle");
}

public override void Exit()
{
    enemyBehaviour.ChangeState(enemyBehaviour.chaseState);
}

}
