using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShieldBehaviour : MonoBehaviour
{
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange;
    public GameObject hotZone;
    public GameObject hotzone_flipped;
    public GameObject triggerArea;
    public Transform attackPoint;
    public Transform attackPointFlip;
    
    private Animator animator;
    private float distance;
    private bool attackMode;
    private bool cooling;
    private float intTimer;
    private GameObject player;
    private float timerAttack;
    private float attackDelay = 0.5f;
    


void Awake()
{
    intTimer = timer;
    animator = GetComponent<Animator>();
    player = GameObject.FindWithTag("Player");
}

void Update() 
{
    if (inRange)
    {
       EnemyLogic();
    }

    if (attackMode == false)
    {
        attackPoint.gameObject.SetActive(false);
        attackPointFlip.gameObject.SetActive(false);
    }

}

void EnemyLogic()
{
    distance = Vector2.Distance(transform.position, target.position);

    if (distance > attackDistance && !animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyShield_attack") && animator.GetBool("OnGuard") == false)
    {   
        Move();
        StopAttack();       
    }
    else if (attackDistance >= distance && cooling == false && !animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyShield_attack"))
    {
        timerAttack += Time.deltaTime;
        if (timerAttack > attackDelay)
        {
            Attack();
        }
    }

    if (cooling)
    {
        Cooldown();
        animator.SetBool("Attack", false);
    }

    if (player.GetComponent<Player>().GetHP() <= 0)
    {
        Move();
        StopAttack();
    }
}

void Move()
{
    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("EnemyShield_attack") && animator.GetBool("OnGuard") == false)
    {
        animator.SetBool("Run", true);
        Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}

void Attack()
{
    timer = intTimer;
    attackMode = true;

    animator.SetBool("Run", false);
    animator.SetBool("OnGuard", true);
    Debug.Log("callAttack");
}

public void RealAttack()
    {
        AudioManager.INSTANCE.PlayEnemyAttack();

        if (transform.position.x > target.position.x)
        {
            attackPointFlip.gameObject.SetActive(true);
        }
        else
        {
            attackPoint.gameObject.SetActive(true);
        }

        StartCoroutine(NoMoreAttack());

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
}

void StopAttack() 
{
    cooling = false;
    attackMode = false;
    animator.SetBool("OnGuard", false);
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

    transform.eulerAngles = rotation;
    }


public void StartCooldownTrigger()
{
        StartCoroutine(CooldownTrigger());
}

public IEnumerator CooldownTrigger()
{
    yield return new WaitForSeconds(1.2f);

   triggerArea.SetActive(true);
}

public IEnumerator NoMoreAttack()
{
    yield return new WaitForSeconds(0.3f);

    attackPoint.gameObject.SetActive(false);
    attackPointFlip.gameObject.SetActive(false);
}

}
