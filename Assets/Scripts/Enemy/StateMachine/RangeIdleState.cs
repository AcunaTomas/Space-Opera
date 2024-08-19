using UnityEngine;

public class RangeIdleState : State
{
    // Start is called before the first frame update
    bool coolingHitRef, playerKilled, exiting;
    float distance;
    EnemyShooting2 enemyBehaviour;
    Enemy enemyRef;
    [SerializeField] GameObject _alert;

    void Start()
    {
        enemyBehaviour = enemy.GetComponent<EnemyShooting2>();
        enemyRef = enemy.GetComponent<Enemy>();
        exiting = false;
    }

    void Update()
    {
        if(enemyBehaviour.GetState() != enemyBehaviour.idleState) return;
        if((enemyRef._coolingHit || enemyRef._coolingHit2) && !exiting) Exit();
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
        if(enemyBehaviour.GetState() == enemyBehaviour.chaseState) return;
        exiting = true;
        animator.SetBool("Alert", true);
        var _exclamationMark = Instantiate(_alert, enemyBehaviour.gameObject.transform);
        Destroy(_exclamationMark, 0.5f);
        Invoke(nameof(Alert), .5f);
    }

    public void PlayerKilled()
    {
        playerKilled = true;
    }

    void Alert()
    {
        enemyBehaviour.ChangeState(enemyBehaviour.chaseState);
    }
}
