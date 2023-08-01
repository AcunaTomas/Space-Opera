using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    [SerializeField]
    private Animator animator;

    public Transform attackPoint;
    public Transform attackPointUp;
    public Transform attackPointDown;
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
    Animator attackAnimatorUp;
    Animator attackAnimatorDown;
    SpriteRenderer attackSprite;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GameObject attackVisual = GameObject.FindWithTag("AttackVisual");
        GameObject attackVisualUp = GameObject.FindWithTag("AttackVisualUp");
        GameObject attackVisualDown = GameObject.FindWithTag("AttackVisualDown");
        attackAnimator = attackVisual.GetComponent<Animator>();
        attackAnimatorUp = attackVisualUp.GetComponent<Animator>();
        attackAnimatorDown = attackVisualDown.GetComponent<Animator>();
        attackSprite = attackVisual.GetComponent<SpriteRenderer>();

    }
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetAxis("Fire1") > 0 && (Input.GetAxis("Vertical") == 0) && (Input.GetAxis("Vertical") == 0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else if (Input.GetAxis("Fire1") > 0 && (Input.GetAxis("Vertical") > 0))
            {
                AttackUp();
                nextAttackTime = Time.time + 1f / attackRate;
            }
            else if (Input.GetAxis("Fire1") > 0 && (Input.GetAxis("Vertical") < 0) && gameObject.GetComponent<Player>().canIjump == false)
            {
                Debug.Log("Ataca abajo");
                AttackDown();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        if (Time.time >= nextBombTime)
        {
            if(Input.GetAxis("Fire2") > 0)
            {
                Invoke(nameof(Bomb), 0.15f);
                nextBombTime = Time.time + 2f / bombRate;
                animator.SetTrigger("Bomb");
            }
        }
        
        if (spriteRenderer.flipX)
        {
            attackPointUp.localPosition = new Vector2(0, 0.2f);
            attackPointDown.localPosition = new Vector2(0, -0.2f);
        }
        else
        {
            attackPointUp.localPosition = new Vector2(0, 0.2f);
            attackPointDown.localPosition = new Vector2(0, -0.2f);
        }   

    }

    void Attack()
    {

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

        animator.SetTrigger("Attack");
        attackAnimator.SetTrigger("Golpe");
        Debug.Log("Ataca");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        Collider2D[] hitDoor = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, doorLayers);

        foreach (Collider2D enemyCollider in hitEnemies)
        {
            enemyCollider.GetComponent<Enemy>().TakeDamage(attackDamage);
        }

        foreach (Collider2D door in hitDoor)
        {
            door.GetComponent<DoorController2>().TakeDamageDoor(attackDamage);
        }

    }

        void AttackUp()
    {

        animator.SetTrigger("AttackUp");
        animator.SetBool("IsJumping", false);
        attackAnimatorUp.SetTrigger("GolpeUp");
        Debug.Log("Ataca arriba");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointUp.position, attackRange, enemyLayers);
        Collider2D[] hitDoor = Physics2D.OverlapCircleAll(attackPointUp.position, attackRange, doorLayers);

        foreach (Collider2D enemyCollider in hitEnemies)
        {
            enemyCollider.GetComponent<Enemy>().TakeDamage(attackDamage);
        }

        foreach (Collider2D door in hitDoor)
        {
            door.GetComponent<DoorController2>().TakeDamageDoor(attackDamage);
        }

    }

        void AttackDown()
    {

        animator.SetTrigger("AttackDown");
        animator.SetBool("IsJumping", false);
        attackAnimatorDown.SetTrigger("GolpeDown");
        Debug.Log("Ataca abajo");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointDown.position, attackRange, enemyLayers);
        Collider2D[] hitDoor = Physics2D.OverlapCircleAll(attackPointDown.position, attackRange, doorLayers);

        foreach (Collider2D enemyCollider in hitEnemies)
        {
            enemyCollider.GetComponent<Enemy>().TakeDamage(attackDamage);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1), ForceMode2D.Impulse);
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

        if (attackPointUp == null){
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(attackPointUp.position, attackRange);
        Gizmos.DrawWireSphere(attackPointDown.position, attackRange);
    }
}
