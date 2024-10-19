using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class SquireBehaviour : MonoBehaviour
{
    public float attackDistance;
    public float moveSpeed;
    public bool DONTMOVE = false;
    bool GOLPEADO = false;
    public float timer;
    [HideInInspector] public Transform target;
    public bool attackMode;
    public Transform attackPoint;
    public Transform attackPointFlip;
    [HideInInspector] public float timerAttack;
    public float attackDelay = 1.5f;
    public Animator animator;
    [HideInInspector] public bool cooling;

    private GameObject player;
    private Enemy enemyStats;
    private StudioEventEmitter walkingAudio;

    //EnemyState
    [Header("Estados")]
    public SquireIdleState idleState;
    public SquireChaseState chaseState;
    public SquireAttackState attackState;
    public SquireFlipState flipState;
    public SquirePhasingState phasingState;
    [SerializeField] State state;
    public bool grounded;

    bool SECONDPHASE = false;
    public RuntimeAnimatorController SecondPhaseAnimatorRight;
    public RuntimeAnimatorController SecondPhaseAnimatorLeft;
    [SerializeField] GameObject Shield;

void Awake()
{
    animator = GetComponent<Animator>();
    player = GameObject.FindWithTag("Player");
    walkingAudio = GetComponent<StudioEventEmitter>();
    target = player.transform;
    animator.SetBool("Dead", false);
    enemyStats = GetComponent<Enemy>();
    idleState.Setup(player, gameObject, animator);
    chaseState.Setup(player, gameObject, animator);
    attackState.Setup(player, gameObject, animator);
    flipState.Setup(player, gameObject, animator);
    if(!state) state = idleState;
}

void Update() 
{
    if(animator.GetCurrentAnimatorStateInfo(0).IsName("squire_secondPhaseTransition")) return;
    state.Do();
    timerAttack += Time.deltaTime;
    if(enemyStats.currentHealth <= 200 && !SECONDPHASE)
    {
        ChangeState(phasingState);
        SECONDPHASE = true;
        animator.SetTrigger("SecondPhase");
        animator.SetBool("Phasing", true);
    }
    if (attackMode == false)
    {
        attackPoint.gameObject.SetActive(false);
        attackPointFlip.gameObject.SetActive(false);
    }

    if(enemyStats.currentHealth <= 0)
    {
        walkingAudio.Stop();
    }


    /* Saltado
    Debug.DrawRay(transform.position, Vector2.down, Color.red);
    if(Physics.Raycast(transform.position, Vector2.down, 1f))
    {
        grounded = true;
    }
    else
    {
        grounded = false;
    }
    */
}

public void TriggerCooling()
{   
    cooling = true;
    attackPoint.gameObject.SetActive(false);
    attackPointFlip.gameObject.SetActive(false);
}

public void StopAlertAnim()
{
    animator.SetBool("Alert", false);
}

public void ChangeState(State newState)
{
    state = newState;
}

public State GetState()
{
    return state;
}

public void EnterSecondState()
{
    StartCoroutine(SecondState());
}

public bool GetPhase()
{
    return SECONDPHASE;
}

public GameObject GetShield()
{
    return Shield;
}
private void OnEnable() {
    idleState.gameObject.SetActive(true);
    chaseState.gameObject.SetActive(true);
    attackState.gameObject.SetActive(true);
    flipState.gameObject.SetActive(true);
}

private void OnDisable() {
    idleState.gameObject.SetActive(false);
    chaseState.gameObject.SetActive(false);
    attackState.gameObject.SetActive(false);
    flipState.gameObject.SetActive(false);
    if(enemyStats.currentHealth <= 0) animator.SetTrigger("Die");
    if(enemyStats.currentHealth <= 0) animator.SetBool("Dead", true);

}

private IEnumerator SecondState()
    {
        yield return new WaitForSeconds(0.0001f);
        animator.runtimeAnimatorController = SecondPhaseAnimatorRight;
        ChangeState(chaseState);
        Shield.SetActive(false);
        gameObject.layer = LayerMask.NameToLayer("Enemy");
        moveSpeed*=2;
        flipState.flipping = false;
        if(!flipState.lookingLeft && transform.position.x > target.position.x || flipState.lookingLeft && transform.position.x < target.position.x) StartCoroutine(flipState.Flip());
    }


public void DropShild()
    {
        Shield.GetComponent<Animator>().SetTrigger("SecondPhase");
    }

public void SetMoveSpeed(float num)
    {
        moveSpeed = num;
    }

}
