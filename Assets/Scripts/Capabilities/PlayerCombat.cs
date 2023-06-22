using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    [SerializeField]
    private Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;
    public LayerMask doorLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 40;

    public float attackRate = 2f;
    float nextAttackTime = 0f;
    private SpriteRenderer spriteRenderer;

    public float bombRate = 2f;
    float nextBombTime = 0f;
    public Transform launchOffset;
    public ProjectileBehaviour projectilePrefab;
    Animator attackAnimator;
    SpriteRenderer attackSprite;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameObject attackVisual = GameObject.FindWithTag("AttackVisual");
        attackAnimator = attackVisual.GetComponent<Animator>();
        attackSprite = attackVisual.GetComponent<SpriteRenderer>();

    }
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetAxis("Fire1") > 0)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        if (Time.time >= nextBombTime)
        {
            if(Input.GetAxis("Fire2") > 0)
            {
                Bomb();
                nextBombTime = Time.time + 2f / bombRate;
                animator.SetTrigger("Bomb");
            }
        }

        if (spriteRenderer.flipX)
        {
            attackPoint.localPosition = new Vector2(-0.15f, 0);
            attackSprite.flipX = true;
        }
        else
        {
            attackPoint.localPosition = new Vector2(0.15f, 0);
            attackSprite.flipX = false;
        }

    }

    void Attack()
    {

        animator.SetTrigger("Attack");
        attackAnimator.SetTrigger("Golpe");
        Debug.Log("ataca");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        Collider2D[] hitDoor = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, doorLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }

        foreach (Collider2D door in hitDoor)
        {
            door.GetComponent<DoorController2>().TakeDamageDoor(attackDamage);
        }

    }

    void Bomb()
    {
        if (spriteRenderer.flipX)
        {
            launchOffset.localPosition = new Vector2(-0.15f, 0);
            Instantiate(projectilePrefab, launchOffset.position, transform.rotation);
        }
        else
        {
            launchOffset.localPosition = new Vector2(0.15f, 0);
            Instantiate(projectilePrefab, launchOffset.position, transform.rotation);
        }
        
        
    }

    void OnDrawGizmosSelected() 
    {
        if (attackPoint == null){
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
