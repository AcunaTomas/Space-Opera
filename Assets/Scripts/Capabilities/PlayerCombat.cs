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
    public LayerMask enemyGunLayers;
    public LayerMask doorLayers;
    public LayerMask enemyBalaLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 40;

    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public SpriteRenderer spriteRenderer;

    public float bombRate = 2f;
    float nextBombTime = 0f;
    public Transform launchOffset;
    public ProjectileBehaviour projectilePrefab;
    Animator attackAnimator;
    Animator attackAnimatorUp;
    Animator attackAnimatorDown;
    SpriteRenderer attackSprite;

    public float attackDuration = 0.3f;

    private bool isAttackActive = false;
    private bool isAttackUpActive = false;
    private bool isAttackDownActive = false;

    private float queryStartTime;

    public bool GetSpriteRend()
    {
        return spriteRenderer.flipX;
    }
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
    void FixedUpdate()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetAxis("Fire1") > 0 && (Input.GetAxis("Vertical") == 0) && (Input.GetAxis("Vertical") == 0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
                isAttackActive = true;
                queryStartTime = Time.time;
            }
            else if (Input.GetAxis("Fire1") > 0 && (Input.GetAxis("Vertical") > 0))
            {
                AttackUp();
                nextAttackTime = Time.time + 1f / attackRate;
                isAttackUpActive = true;
                queryStartTime = Time.time;
            }
            else if (Input.GetAxis("Fire1") > 0 && (Input.GetAxis("Vertical") < 0) && gameObject.GetComponent<Player>().canIjump == false)
            {
                AttackDown();
                nextAttackTime = Time.time + 1f / attackRate;
                isAttackDownActive = true;
                queryStartTime = Time.time;
            }
        }


        if (Time.time >= nextBombTime)
        {
            if (Input.GetAxis("Fire2") > 0 && (Input.GetAxis("Vertical") < 0))
            {
                Invoke(nameof(Bomb2), -0.15f);
                nextBombTime = Time.time + 2f / bombRate;
                animator.SetTrigger("Bomb2");
            }
            else if (Input.GetAxis("Fire2") > 0)
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

        if (isAttackActive)
        {
            if (Time.time - queryStartTime >= attackDuration)
            {
                isAttackActive = false;
            }
            else
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
                foreach (Collider2D enemyCollider in hitEnemies)
                {
                    enemyCollider.GetComponent<Enemy>().TakeDamage(attackDamage);
                }

                Collider2D[] hitEnemiesGun = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyGunLayers);
                foreach (Collider2D enemyGunCollider in hitEnemiesGun)
                {
                    enemyGunCollider.GetComponent<Enemy>().TakeDamage2(attackDamage);
                }

                Collider2D[] hitBala = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyBalaLayers);
                foreach (Collider2D balaCollider in hitBala)
                {
                    balaCollider.gameObject.SetActive(false);
                }
            }
        }

        if (isAttackUpActive)
        {
            if (Time.time - queryStartTime >= attackDuration)
            {
                isAttackUpActive = false;
            }
            else
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointUp.position, attackRange, enemyLayers);
                foreach (Collider2D enemyCollider in hitEnemies)
                {
                    enemyCollider.GetComponent<Enemy>().TakeDamage(attackDamage);
                }

                Collider2D[] hitEnemiesGun = Physics2D.OverlapCircleAll(attackPointUp.position, attackRange, enemyGunLayers);
                foreach (Collider2D enemyGunCollider in hitEnemiesGun)
                {
                    enemyGunCollider.GetComponent<Enemy>().TakeDamage2(attackDamage);
                }

                Collider2D[] hitBala = Physics2D.OverlapCircleAll(attackPointUp.position, attackRange, enemyBalaLayers);
                foreach (Collider2D balaCollider in hitBala)
                {
                    balaCollider.gameObject.SetActive(false);
                }
            }
        }

        if (isAttackDownActive)
        {
            if (Time.time - queryStartTime >= attackDuration)
            {
                isAttackDownActive = false;
            }
            else
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointDown.position, attackRange, enemyLayers);
                foreach (Collider2D enemyCollider in hitEnemies)
                {
                    enemyCollider.GetComponent<Enemy>().TakeDamage2(attackDamage);
                }

                Collider2D[] hitEnemiesGun = Physics2D.OverlapCircleAll(attackPointDown.position, attackRange, enemyGunLayers);
                foreach (Collider2D enemyGunCollider in hitEnemiesGun)
                {
                    enemyGunCollider.GetComponent<Enemy>().TakeDamage2(attackDamage);
                }

                Collider2D[] hitBala = Physics2D.OverlapCircleAll(attackPointDown.position, attackRange, enemyBalaLayers);
                foreach (Collider2D balaCollider in hitBala)
                {
                    balaCollider.gameObject.SetActive(false);
                }
            }
        }

    }

    
    public virtual void Attack()
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

        //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        Collider2D[] hitDoor = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, doorLayers);

        // foreach (Collider2D enemyCollider in hitEnemies)
        // {
        //     enemyCollider.GetComponent<Enemy>().TakeDamage(attackDamage);
        //     AudioManager.INSTANCE.PlayEnemyHit();
        // }

        foreach (Collider2D door in hitDoor)
        {
            door.GetComponent<DoorController2>().TakeDamageDoor(attackDamage);
        }

    }

    public virtual void AttackUp()
    {

        animator.SetTrigger("AttackUp");
        animator.SetBool("IsJumping", false);
        attackAnimatorUp.SetTrigger("GolpeUp");
        Debug.Log("Ataca arriba");

        //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointUp.position, attackRange, enemyLayers);
        Collider2D[] hitDoor = Physics2D.OverlapCircleAll(attackPointUp.position, attackRange, doorLayers);

        // foreach (Collider2D enemyCollider in hitEnemies)
        // {
        //     enemyCollider.GetComponent<Enemy>().TakeDamage(attackDamage);
        //     AudioManager.INSTANCE.PlayEnemyHit();
        // }

        foreach (Collider2D door in hitDoor)
        {
            door.GetComponent<DoorController2>().TakeDamageDoor(attackDamage);
        }

    }

    public virtual void AttackDown()
    {

        animator.SetTrigger("AttackDown");
        animator.SetBool("IsJumping", false);
        attackAnimatorDown.SetTrigger("GolpeDown");
        Debug.Log("Ataca abajo");

        //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPointDown.position, attackRange, enemyLayers);
        Collider2D[] hitDoor = Physics2D.OverlapCircleAll(attackPointDown.position, attackRange, doorLayers);

        // foreach (Collider2D enemyCollider in hitEnemies)
        // {
        //     enemyCollider.GetComponent<Enemy>().TakeDamage(attackDamage);
        //     GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1), ForceMode2D.Impulse);
        //     AudioManager.INSTANCE.PlayEnemyHit();
        // }

        foreach (Collider2D door in hitDoor)
        {
            door.GetComponent<DoorController2>().TakeDamageDoor(attackDamage);
        }

    }

    public virtual void Bomb()
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
    public virtual void Bomb2()
    {
        if (spriteRenderer.flipX)
        {
            launchOffset.localPosition = new Vector2(-0.15f, -0.12f);
            Instantiate(projectilePrefab, launchOffset.position, transform.rotation);
        }
        else
        {
            launchOffset.localPosition = new Vector2(0.15f, -0.12f);
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
