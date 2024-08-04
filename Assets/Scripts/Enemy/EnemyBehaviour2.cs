using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour2 : MonoBehaviour
{
    public float attackDistance;
    public float moveSpeed;
    public bool DONTMOVE = false;
    bool GOLPEADO = false;
    public float timer;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange;
    public GameObject hotZone;
    public GameObject triggerArea;
    public Transform attackPoint;
    public Transform attackPointFlip;

    public Animator animator;
    private float distance;
    private bool attackMode;
    [HideInInspector] public bool cooling;
    private float intTimer;
    private GameObject player;
    private float timerAttack;
    private float attackDelay = 1f;

    //EnemyState
    [Header("Estados")]
    public IdleState idleState;
    public ChaseState chaseState;
    public AttackState attackState;
    [SerializeField] State state;

void Awake()
{
    intTimer = timer;
    animator = GetComponent<Animator>();
    player = GameObject.FindWithTag("Player");
    target = player.gameObject.transform;
    animator.SetBool("Dead", false);

    idleState.Setup(player, gameObject, animator);
    chaseState.Setup(player, gameObject, animator);
    attackState.Setup(player, gameObject, animator);
    if(!state) state = idleState;
}

void Update() 
{
    state.Do();

    if (attackMode == false)
    {
        attackPoint.gameObject.SetActive(false);
        attackPointFlip.gameObject.SetActive(false);
    }
}

public void TriggerCooling()
{   
    Debug.Log("COOLING");
    cooling = true;
    attackPoint.gameObject.SetActive(false);
    attackPointFlip.gameObject.SetActive(false);
}

public void Flip()
{
    Vector3 rotation = transform.eulerAngles;
    if (transform.position.x > target.position.x) 
    {
        GetComponent<SpriteRenderer>().flipX = true;
        attackPoint.gameObject.GetComponent<HurtBox>().setOrientation(-1);
        }
    else
    {
        GetComponent<SpriteRenderer>().flipX = false;
        attackPoint.gameObject.GetComponent<HurtBox>().setOrientation(1);
    }

    
    //Ternary Operator
    //rotation.y = (currentTarget.position.x < transform.position.x) ? rotation.y = 180f : rotation.y = 0f;

    transform.eulerAngles = rotation;
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


}
