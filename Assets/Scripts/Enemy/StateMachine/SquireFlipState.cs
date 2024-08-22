using System;
using System.Collections;
using UnityEngine;

public class SquireFlipState : State
{
    // Start is called before the first frame update
    bool coolingHitRef, playerKilled, exiting;
    float distance;
    SquireBehaviour enemyBehaviour;
    Enemy enemyRef;

    void Start()
    {
        enemyBehaviour = enemy.GetComponent<SquireBehaviour>();
        enemyRef = enemy.GetComponent<Enemy>();
        exiting = false;
    }
    public override void Do()
    {
        
    }
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(enemyBehaviour.idleState.firstEncounter) enemyBehaviour.idleState.firstEncounter = false;
            enemyBehaviour.ChangeState(enemyBehaviour.flipState);
            StartCoroutine(Flip());
        }
    }


    public override void Exit()
    {
        if(enemyBehaviour.GetState() == enemyBehaviour.chaseState) return;
        enemyBehaviour.ChangeState(enemyBehaviour.chaseState);
    }

    public void PlayerKilled()
    {
        playerKilled = true;
    }

    public IEnumerator Flip()
    {
        if (transform.position.x > enemyBehaviour.target.position.x) 
        {
            animator.SetTrigger("Flip");
            enemyBehaviour.GetComponent<SpriteRenderer>().flipX = false;
            if(enemyBehaviour.GetPhase())
            {
                yield return new WaitForSeconds(1.3f);
                animator.runtimeAnimatorController = enemyBehaviour.SecondPhaseAnimatorLeft;
            }
            if(!enemyBehaviour.GetPhase())
            {
                yield return new WaitForSeconds(2f);
                enemyBehaviour.GetComponent<SpriteRenderer>().flipX = true;
            }
            else enemyBehaviour.GetComponent<SpriteRenderer>().flipX = false;
            enemyBehaviour.attackPoint.gameObject.GetComponent<HurtBox>().setOrientation(-1);
            enemyBehaviour.idleState.gameObject.GetComponent<BoxCollider2D>().offset = -enemyBehaviour.idleState.gameObject.GetComponent<BoxCollider2D>().offset;
            if(enemyBehaviour.GetShield())
            {
                enemyBehaviour.GetShield().GetComponent<BoxCollider2D>().offset = new Vector2(-enemyBehaviour.GetShield().GetComponent<BoxCollider2D>().offset.x,
                                                                    enemyBehaviour.GetShield().GetComponent<BoxCollider2D>().offset.y);
                enemyBehaviour.GetShield().GetComponent<SpriteRenderer>().flipX = true;
            }
            GetComponent<BoxCollider2D>().offset = new Vector2(-GetComponent<BoxCollider2D>().offset.x,
                                                                GetComponent<BoxCollider2D>().offset.y);
            Exit();
        }
        else
        {
            animator.SetTrigger("Flip");
            if(enemyBehaviour.GetPhase())
            {
                enemyBehaviour.GetComponent<SpriteRenderer>().flipX = true;
                yield return new WaitForSeconds(1.3f);
                animator.runtimeAnimatorController = enemyBehaviour.SecondPhaseAnimatorRight;
            }
            else yield return new WaitForSeconds(2f);
            enemyBehaviour.GetComponent<SpriteRenderer>().flipX = false;
            enemyBehaviour.attackPoint.gameObject.GetComponent<HurtBox>().setOrientation(1);
            enemyBehaviour.idleState.gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(Math.Abs(enemyBehaviour.idleState.gameObject.GetComponent<BoxCollider2D>().offset.x),
                                                                                            enemyBehaviour.idleState.gameObject.GetComponent<BoxCollider2D>().offset.y);
            if(enemyBehaviour.GetShield())
            {
                enemyBehaviour.GetShield().GetComponent<BoxCollider2D>().offset = new Vector2(Math.Abs(enemyBehaviour.GetShield().GetComponent<BoxCollider2D>().offset.x),
                                                                        enemyBehaviour.GetShield().GetComponent<BoxCollider2D>().offset.y);
                enemyBehaviour.GetShield().GetComponent<SpriteRenderer>().flipX = false;
            }
            GetComponent<BoxCollider2D>().offset = new Vector2(-GetComponent<BoxCollider2D>().offset.x,
                                                    GetComponent<BoxCollider2D>().offset.y);
            Exit();
        }
        
    }
}
