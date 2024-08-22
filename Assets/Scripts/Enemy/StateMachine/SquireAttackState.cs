using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquireAttackState : State
{
    // Start is called before the first frame update
    bool coolingHitRef;
    float distance;
    SquireBehaviour enemyBehaviour;
    private bool attackMode;
    private float timerAttack, attackDelay = 1f, intTimer;
    [SerializeField] Transform _bulletSource;
    [SerializeField] GameObject _bullet;

    void Start()
    {
        enemyBehaviour = enemy.GetComponent<SquireBehaviour>();
        intTimer = enemyBehaviour.timer;
    }

    public override void Do()
    {
        coolingHitRef = enemy.GetComponent<Enemy>()._coolingHit;
        //enemyBehaviour.Flip();
        if(!enemyBehaviour.DONTMOVE || !coolingHitRef)
        {
            distance = Vector2.Distance(enemy.transform.position, enemyBehaviour.target.position);
        }
        if(!enemyBehaviour.GetPhase())
        {
            if (enemyBehaviour.cooling == false && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_idle"))
            {
                if (enemyBehaviour.timerAttack > enemyBehaviour.attackDelay)
                {
                    enemyBehaviour.timer = intTimer;
                    attackMode = true;
                    animator.SetBool("Run", false);
                    animator.SetBool("Attack", true);
                    enemyBehaviour.timerAttack = 0;
                    Invoke("RangeAttack", 0.5f);
                }
            }


            if (player.GetComponent<Player>().GetHP() <= 0)
            {
                StopRangeAttack();
            }
        }
        else
        {
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
            }

            if (player.GetComponent<Player>().GetHP() <= 0)
            {
                StopAttack();
            }
        }
        if (enemyBehaviour.cooling)
        {
            enemyBehaviour.attackState.Cooldown();
            enemyBehaviour.attackState.RangeCooldown();
            animator.SetBool("Attack", false);
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
    public void Cooldown()
    {
        enemyBehaviour.timer -= Time.deltaTime;
        if (enemyBehaviour.timer <= 0 && enemyBehaviour.cooling && attackMode)
        {
            enemyBehaviour.cooling = false;
            enemyBehaviour.timer = intTimer;
        }
    }

    void RangeAttack()
    {

        Instantiate(_bullet, _bulletSource.position, Quaternion.identity);
        _bullet.SetActive(true);
        //AudioManager.INSTANCE.PlayEnemyAttack();  SONIDO DE ATAQUE

        //attackPoint.gameObject.SetActive(true);

        Exit();
    }

    public void RangeCooldown()
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

    void StopRangeAttack() 
    {
        attackMode = false;
        animator.SetBool("Attack", false);
        animator.SetTrigger("Idle");
    }

    public override void Exit()
    {
        enemyBehaviour.ChangeState(enemyBehaviour.chaseState);
        animator.SetBool("Attack", false);
    }
}
