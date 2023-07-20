using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour2 : MonoBehaviour
{
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange;
    public GameObject hotZone;
    public GameObject triggerArea;

    private Animator animator;
    private float distance;
    private bool attackMode;
    private bool cooling;
    private float intTimer;

void Awake()
{
    intTimer = timer;
    animator = GetComponent<Animator>();
}

void Update() 
{
    if (inRange)
    {
       //animator.SetBool("Run", false);
       EnemyLogic();
    }
}

void EnemyLogic() 
{
    distance = Vector2.Distance(transform.position, target.position);

    if (distance > attackDistance)
    {   
        Move();
        StopAttack();
    }
    else if (attackDistance >= distance && cooling == false)
    {
        Attack();
    }

    if (cooling)
    {
        Cooldown();
        animator.SetBool("Attack", false);
    }
}

void Move()
{
    Debug.Log("move");
    animator.SetBool("Run", true);

    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
    {
        Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}

void Attack()
{
    timer = intTimer;
    attackMode = true;

    animator.SetBool("Run", false);
    animator.SetBool("Attack", true);
}


void Cooldown()
{
    timer -= Time.deltaTime;

    if (timer <= 0 && cooling && attackMode)
    {
        cooling = false;
        timer = intTimer;
    }
}

void StopAttack() 
{
    cooling = false;
    attackMode = false;
    animator.SetBool("Attack", false);
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
        rotation.y = 180;
    }
    else
    {
        Debug.Log("Twist");
        rotation.y = 0;
    }

    //Ternary Operator
    //rotation.y = (currentTarget.position.x < transform.position.x) ? rotation.y = 180f : rotation.y = 0f;

    transform.eulerAngles = rotation;
}










    // [SerializeField]
    // animator animator;

    // [SerializeField]
    // GameObject player;
    // [SerializeField]
    // bool flip;
    // //public float speed;

    // [SerializeField]
    // float agroRange;
    // [SerializeField]
    // float moveSpeed;
    
    // Rigidbody2D rb2d;

    // void Start() 
    // {
    //     rb2d = GetComponent<Rigidbody2D>();
    // }

    // void Update()
    // {

    //     float distToPlayer = Vector2.Distance (transform.position, player.transform.position);
        
    //     if (distToPlayer < agroRange)
    //     {
    //         Chase();
    //         Debug.Log("Chase");
    //     }
    //     else
    //     {
    //         Debug.Log("Stop");
    //     }

    //     Vector3 scale = transform.localScale;

    //     if (player.transform.position.x > transform.position.x)
    //     {
    //         scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
    //         //transform.Translate(speed * Time.deltaTime, 0, 0);
    //         //Debug.Log("un lado");
    //     }
    //     else
    //     {
    //         scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
    //         //transform.Translate(speed * Time.deltaTime * -1, 0, 0);
    //         //Debug.Log("el otro");
    //     }

    //     transform.localScale = scale;

    // }


    // void Chase() 
    // {
    //     if (player.transform.position.x > transform.position.x)
    //         transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
    //     else
    //     transform.Translate(moveSpeed * Time.deltaTime * -1, 0, 0);
    // }

    // void Patrol()
    // {
        
    // }
}
