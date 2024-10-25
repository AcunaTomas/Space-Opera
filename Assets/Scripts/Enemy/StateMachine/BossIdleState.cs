using UnityEngine;
using FMOD;
using FMODUnity;

public class BossIdleState : State
{
    // Start is called before the first frame update
    bool coolingHitRef, playerKilled, exiting;
    float distance;
    BossBehaviourNew enemyBehaviour;
    Enemy enemyRef;

    void Start()
    {
        //enemyBehaviour = enemy.GetComponent<BossBehaviourNew>();
        enemyRef = enemy.GetComponent<Enemy>();
        exiting = false;
    }

    void Update()
    {
        //if(enemyBehaviour.GetState() != enemyBehaviour.idleState) return;
        //if((enemyRef._coolingHit || enemyRef._coolingHit2) && !exiting) Exit();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerKilled = false;
            enemyBehaviour.target = other.transform;
            Exit();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(playerKilled) return;
            enemyBehaviour.target = other.transform;
            Exit();
        }
    }

    public override void Exit()
    {
        //if(enemyBehaviour.GetState() == enemyBehaviour.chaseState) return;
        exiting = true;
        Invoke(nameof(Alert), .2f);
    }

    public void PlayerKilled()
    {
        playerKilled = true;
    }

    void Alert()
    {
        //enemyBehaviour.ChangeState(enemyBehaviour.chaseState);
    }
}
