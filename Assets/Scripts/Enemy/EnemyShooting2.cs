using System.Collections;
using System.Collections.Generic;
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
    private bool cooling;
    private float intTimer;
    private GameObject player;
    private float timerAttack;
    public float attackDelay = 1.5f;
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
    _playerPos = player.transform;
}

void FixedUpdate() 
{
    if (inRange)
    {
        EnemyLogic();
    }

    if (attackMode == false)
    {
        //attackPoint.gameObject.SetActive(false);
    }

    timerAttack += Time.deltaTime;
}

void EnemyLogic() 
{
    if (Vector2.Distance(transform.position, _playerPos.position) > _distanciaFrenado)
    {
        animator.SetBool("Run", true);
        animator.SetBool("Alert", true);
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Scientist_turn") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
        {
            transform.position = Vector2.MoveTowards(transform.position, _playerPos.position, _speed * Time.deltaTime);
        }
    }

    if (Vector2.Distance(transform.position, _playerPos.position) < _distanciaRetroceso)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Scientist_turn") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
        {
            transform.position = Vector2.MoveTowards(transform.position, _playerPos.position, -_speed * Time.deltaTime);
        }
        
        animator.SetBool("Run", true);
        //animator.SetBool("Alert", true);
            
    }

    if ((Vector2.Distance(transform.position, _playerPos.position) < _distanciaFrenado) && (Vector2.Distance(transform.position, _playerPos.position) > _distanciaRetroceso))
    {
        //transform.position = transform.position;
        animator.SetBool("Run", false);
    }

    if (cooling == false && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Scientist_turn") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_idle"))
    {
        if (timerAttack > attackDelay)
        {
            Attack();
        }
        //Attack();
    }

    if (player.GetComponent<Player>().GetHP() <= 0)
    {
        StopAttack();
    }

    if (cooling)
    {
        Cooldown();
        animator.SetBool("Attack", false);
    }
}


void Attack()
{
    timer = intTimer;
    attackMode = true;

    animator.SetBool("Run", false);
    animator.SetBool("Attack", true);
    animator.SetTrigger("Shoot");
    Instantiate(_bullet, _bulletSource.position, Quaternion.identity);
    _bullet.SetActive(true);
    //AudioManager.INSTANCE.PlayEnemyAttack();  SONIDO DE ATAQUE

    //attackPoint.gameObject.SetActive(true);
    timerAttack = 0;
}

void Cooldown()
{
    timer -= Time.deltaTime;

    if (timer <= 0 && cooling && attackMode)
    {
        cooling = false;
        timer = intTimer;
    }

    if (timer < 0 && !attackMode)
    {
        cooling = false;
        timer = intTimer;
    }
}


void StopAttack() 
{
    attackMode = false;
    animator.SetBool("Attack", false);
    animator.SetTrigger("Idle");
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
    }
    else
    {
        GetComponent<SpriteRenderer>().flipX = false;
        _bulletSource.localPosition = new Vector2(0.2f,0);
    }

    
    //Ternary Operator
    //rotation.y = (currentTarget.position.x < transform.position.x) ? rotation.y = 180f : rotation.y = 0f;

    transform.eulerAngles = rotation;
}


}
