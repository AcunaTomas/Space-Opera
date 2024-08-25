using System;
using System.Collections;
using UnityEngine;

public class SquireFlipState : State
{
    // Start is called before the first frame update
    bool coolingHitRef, playerKilled;
    public bool flipping, lookingLeft;
    float distance;
    SquireBehaviour enemyBehaviour;
    Enemy enemyRef;

    void Start()
    {
        enemyBehaviour = enemy.GetComponent<SquireBehaviour>();
        enemyRef = enemy.GetComponent<Enemy>();
        flipping = false;
        lookingLeft = false;
    }
    public override void Do()
    {
        
    }
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            enemyBehaviour.ChangeState(enemyBehaviour.flipState);
            StartCoroutine(Flip());
        }
    }


    public override void Exit()
    {
        flipping = false;
        enemyBehaviour.attackPoint.gameObject.SetActive(false);
        enemyBehaviour.attackPointFlip.gameObject.SetActive(false);
        if(enemyBehaviour.GetState() == enemyBehaviour.chaseState) return;
        enemyBehaviour.ChangeState(enemyBehaviour.chaseState);
    }

    public void PlayerKilled()
    {
        playerKilled = true;
    }

    public IEnumerator Flip()
    {
        
        if (transform.position.x > enemyBehaviour.target.position.x && !flipping) 
        {
            flipping = true;
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
                enemyBehaviour.GetShield().transform.localScale = new Vector2(-1,
                                                                enemyBehaviour.GetShield().transform.localScale.y);
            }
            GetComponent<BoxCollider2D>().offset = new Vector2(-GetComponent<BoxCollider2D>().offset.x,
                                                                GetComponent<BoxCollider2D>().offset.y);
            lookingLeft = true;
            flipping = false;
            Exit();
        }
        else if(!flipping)
        {
            flipping = true;
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
                enemyBehaviour.GetShield().transform.localScale = new Vector2(1,
                                                                enemyBehaviour.GetShield().transform.localScale.y);
            }
            GetComponent<BoxCollider2D>().offset = new Vector2(-GetComponent<BoxCollider2D>().offset.x,
                                                    GetComponent<BoxCollider2D>().offset.y);
            lookingLeft = false;
            flipping = false;
            Exit();
        }
        
    }
}
