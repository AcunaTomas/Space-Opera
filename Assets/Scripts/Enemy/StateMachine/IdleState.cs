using UnityEngine;

public class IdleState : State
{
    // Start is called before the first frame update
    bool coolingHitRef, playerKilled;
    float distance;
    EnemyBehaviour2 enemyBehaviour;
    [SerializeField] GameObject _alert;

    void Start()
    {
        enemyBehaviour = enemy.GetComponent<EnemyBehaviour2>();
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
        animator.SetBool("Alert", true);
        var _exclamationMark = Instantiate(_alert, enemyBehaviour.gameObject.transform);
        Destroy(_exclamationMark, 0.5f);
        enemyBehaviour.ChangeState(enemyBehaviour.chaseState);
    }

    public void PlayerKilled()
    {
        playerKilled = true;
    }
}
