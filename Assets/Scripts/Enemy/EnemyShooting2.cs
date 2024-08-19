using System.Collections;
using System;
using UnityEngine;

public class EnemyShooting2 : MonoBehaviour
{
    public Transform _playerPos;
    public float _speed;
    public float _distanciaFrenado;
    public float _distanciaRetroceso;

    public GameObject _bullet;
    private float _tiempo;

    private SpriteRenderer _spriteRenderer;


    public float attackDistance;
    public float moveSpeed;
    public float timer;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange;
    public GameObject hotZone;
    public GameObject triggerArea;
    public Transform _bulletSource;

    private Animator animator;
    private float distance;
    private bool attackMode;
    [HideInInspector] public bool cooling;
    private float intTimer;
    private GameObject player;
    [HideInInspector] public float timerAttack;
    public float attackDelay = 1.5f;
        //EnemyState
    [Header("Estados")]
    public RangeIdleState idleState;
    public RangeChaseState chaseState;
    public RangeAttackState attackState;
    [SerializeField] State state;


void Awake()
{
    intTimer = timer;
    animator = GetComponent<Animator>();
    player = GameObject.FindWithTag("Player");
    _playerPos = player.transform;
    target = _playerPos;
    idleState.Setup(player, gameObject, animator);
    chaseState.Setup(player, gameObject, animator);
    attackState.Setup(player, gameObject, animator);
    if(!state) state = idleState;
}

void FixedUpdate() 
{
    state.Do();
    timerAttack += Time.deltaTime;
}

public void TriggerCooling()
{   
    cooling = true;
}

public void Flip()
{
    Vector3 rotation = transform.eulerAngles;
    if (transform.position.x > target.position.x) 
    {
        GetComponent<SpriteRenderer>().flipX = true;
        _bulletSource.localPosition = new Vector2(-0.2f,0);
        idleState.gameObject.GetComponent<BoxCollider2D>().offset = -idleState.gameObject.GetComponent<BoxCollider2D>().offset;
        chaseState.gameObject.GetComponent<BoxCollider2D>().offset = -chaseState.gameObject.GetComponent<BoxCollider2D>().offset;
    }
    else
    {
        GetComponent<SpriteRenderer>().flipX = false;
        _bulletSource.localPosition = new Vector2(0.2f,0);
        idleState.gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(Math.Abs(idleState.gameObject.GetComponent<BoxCollider2D>().offset.x),
                                                                                        idleState.gameObject.GetComponent<BoxCollider2D>().offset.y);
        chaseState.gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(Math.Abs(chaseState.gameObject.GetComponent<BoxCollider2D>().offset.x),
                                                                                        chaseState.gameObject.GetComponent<BoxCollider2D>().offset.y);
    }
    
    //Ternary Operator
    //rotation.y = (currentTarget.position.x < transform.position.x) ? rotation.y = 180f : rotation.y = 0f;

    transform.eulerAngles = rotation;
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
