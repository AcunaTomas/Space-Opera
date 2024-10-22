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
    
    void OnEnable()
    {
        if(!enemyBehaviour) return;
        if(((lookingLeft && transform.position.x < enemyBehaviour.target.position.x) || (!lookingLeft && transform.position.x > enemyBehaviour.target.position.x)) && !enemyBehaviour.idleState.firstEncounter) 
        {
            enemyBehaviour.ChangeState(enemyBehaviour.flipState);
            StartCoroutine(Flip());
        }
    }

    void Start()
    {
        enemyBehaviour = enemy.GetComponent<SquireBehaviour>();
        enemyRef = enemy.GetComponent<Enemy>();
        flipping = false;
        lookingLeft = false;
    }
    public override void Do()
    {
        if(lookingLeft) enemyBehaviour.idleState.gameObject.GetComponent<BoxCollider2D>().offset = new Vector2 (-Math.Abs(enemyBehaviour.idleState.gameObject.GetComponent<BoxCollider2D>().offset.x),
                                                                            Math.Abs(enemyBehaviour.idleState.gameObject.GetComponent<BoxCollider2D>().offset.y));
        else    enemyBehaviour.idleState.gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(Math.Abs(enemyBehaviour.idleState.gameObject.GetComponent<BoxCollider2D>().offset.x),
                                                                                        enemyBehaviour.idleState.gameObject.GetComponent<BoxCollider2D>().offset.y);
    }
   
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            StopCoroutine(Flip());
            enemyBehaviour.ChangeState(enemyBehaviour.flipState);
            StartCoroutine(Flip());
        }
    }


    public override void Exit()
    {
        flipping = false;
        enemyBehaviour.idleState.firstEncounter = false;
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
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("squire_flipR") && !animator.GetCurrentAnimatorStateInfo(0).IsName("squireF2_flipR")) animator.SetTrigger("Flip");
        if (transform.position.x > enemyBehaviour.target.position.x) //To the left
        {
            enemyBehaviour.GetComponent<SpriteRenderer>().flipX = false;
            GetComponent<BoxCollider2D>().offset = new Vector2(-GetComponent<BoxCollider2D>().offset.x,
                                                    GetComponent<BoxCollider2D>().offset.y);
            if(enemyBehaviour.GetPhase())
            {
                yield return new WaitForSeconds(1.2f);
                
            }
            if(!enemyBehaviour.GetPhase())
            {
                yield return new WaitForSeconds(1.7f);
            }
            //FlippingToTheLeftFinished();
        }
        else
        {
            GetComponent<BoxCollider2D>().offset = new Vector2(-GetComponent<BoxCollider2D>().offset.x,
                                        GetComponent<BoxCollider2D>().offset.y);
            if(enemyBehaviour.GetPhase())
            {
                enemyBehaviour.GetComponent<SpriteRenderer>().flipX = true;
                yield return new WaitForSeconds(1.2f);
            }
            else yield return new WaitForSeconds(1.7f);
            //FlippingToTheRightFinished();
        }
        
    }
    public void FlippingToTheLeftFinished()
    {
        enemyBehaviour.attackPoint.gameObject.GetComponent<HurtBox>().setOrientation(-1);
        if(enemyBehaviour.GetShield())
        {
            enemyBehaviour.GetShield().transform.localScale = new Vector2(-1,
                                                            enemyBehaviour.GetShield().transform.localScale.y);
        }

        lookingLeft = true;
        flipping = false;
        Exit();
    }
    public void FlippingToTheRightFinished()
    {
        enemyBehaviour.GetComponent<SpriteRenderer>().flipX = false;
        enemyBehaviour.attackPoint.gameObject.GetComponent<HurtBox>().setOrientation(1);
        if(enemyBehaviour.GetShield())
        {
            enemyBehaviour.GetShield().transform.localScale = new Vector2(1,
                                                            enemyBehaviour.GetShield().transform.localScale.y);
        }
        lookingLeft = false;
        flipping = false;
        Exit();
    }
}
