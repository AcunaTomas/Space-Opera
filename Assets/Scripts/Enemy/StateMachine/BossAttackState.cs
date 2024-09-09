using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : State
{
    // Start is called before the first frame update
    bool coolingHitRef;
    float distance;
    BossBehaviourNew enemyBehaviour;
    private bool attackMode;
    private float timerAttack, attackDelay = 1f, intTimer;

    void Start()
    {
        //enemyBehaviour = enemy.GetComponent<BossBehaviourNew>();
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
    int rand = Random.Range(1,1);
    if (rand == 1)
    {
        First();
    }
    else if (rand == 2)
    {
        Second();
    }
    else if (rand == 3)
    {
        Third();
    }
    
}


void First()
{
    enemyBehaviour.timer = intTimer;
    attackMode = true;

    animator.SetBool("Run", false);
    animator.SetBool("Attack", true);
    animator.SetTrigger("Shoot");
    //Instantiate(enemyBehaviour._rumba, enemyBehaviour._bulletSource.position, transform.rotation);
    //enemyBehaviour._rumba.SetActive(true);
    //enemyBehaviour._rumba.GetComponent<EnemyPatrol>().enabled = true;
    Instantiate(enemyBehaviour._rumbaRota, enemyBehaviour._bulletSource.position, transform.rotation);
    enemyBehaviour._rumbaRota.SetActive(true);
    if (enemyBehaviour.GetComponent<SpriteRenderer>().flipX)
    {
        enemyBehaviour._rumbaRota.GetComponent<EnemyPatrol>().flip = true;
    }
    else
    {
        enemyBehaviour._rumbaRota.GetComponent<EnemyPatrol>().flip = false;
    }

    //AudioManager.INSTANCE.PlayEnemyAttack();  SONIDO DE ATAQUE

    //attackPoint.gameObject.SetActive(true);
    enemyBehaviour.timerAttack = 0;
    Exit();
}

void Second()
{
    //Ataque con misiles
}

void Third()
{
    //Ataque de electricidad
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
    //enemyBehaviour.ChangeState(enemyBehaviour.chaseState);
}

}
