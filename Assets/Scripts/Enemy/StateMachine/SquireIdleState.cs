using UnityEngine;

public class SquireIdleState : State
{
    // Start is called before the first frame update
    bool coolingHitRef, playerKilled, exiting;
    public bool firstEncounter;
    float distance;
    SquireBehaviour enemyBehaviour;
    Enemy enemyRef;
    [SerializeField] GameObject _alert;

    void Start()
    {
        enemyBehaviour = enemy.GetComponent<SquireBehaviour>();
        enemyRef = enemy.GetComponent<Enemy>();
        exiting = false;
        firstEncounter = true;
    }

    void Update()
    {
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerKilled = false;
            enemyBehaviour.target = other.transform;
            if(firstEncounter) return;
            if(enemyBehaviour.GetState() == enemyBehaviour.idleState) Exit();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(playerKilled || firstEncounter) return;
            enemyBehaviour.target = other.transform;
            if(enemyBehaviour.GetState() == enemyBehaviour.idleState) Exit();
        }
    }

    public override void Exit()
    {
        if(enemyBehaviour.GetState() == enemyBehaviour.chaseState) return;
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
