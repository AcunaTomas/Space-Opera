using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    [SerializeField]
    private Animator animator;
    public GameObject attackPointVisual;
    public Transform attackPoint;
    [SerializeField]
    public GameObject attackPointUpVisual;
    public Transform attackPointUp;
    [SerializeField]
    public GameObject attackPointDownVisual;
    public Transform attackPointDown;
    public LayerMask enemyLayers;
    public LayerMask enemyGunLayers;
    public LayerMask doorLayers;
    public LayerMask enemyBalaLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 40;

    private int attackDamageCombo = 100;

    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public SpriteRenderer spriteRenderer;

    public float bombRate = 2f;
    float nextBombTime = 0f;
    public Transform launchOffset;
    public ProjectileBehaviour projectilePrefab;
    //TESTING
    public JabulaniBehaviour _jabulani;

    public bool _jabulaniOn;

    //TESTING
    //Animator attackAnimator;
    Animator attackAnimatorUp;
    Animator attackAnimatorDown;

    public float attackDuration = 0.3f;

    private bool isAttackActive = false;
    private bool isAttackUpActive = false;
    private bool isAttackDownActive = false;

    private float queryStartTime;

    public float _attackCount = 1;
    [SerializeField]
    private float _attackAnimTimer = 0f;
    [SerializeField]
    private float _attackAnimDelay = 0f;

    [SerializeField]
    private bool hitsomething = false;

    private float hitCooldown = 0.1f;
    private float lastHitTime;


    public bool _canHack;


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
    }
    void FixedUpdate()
    {
        Debug.Log($"Update: isAttackActive={isAttackActive}, _attackCount={_attackCount}, hitSomething={hitsomething}");
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
                if (_jabulaniOn)
                {
                    Invoke(nameof(Bomb2), 0.15f);
                    nextBombTime = Time.time + 2f / bombRate;
                    animator.SetTrigger("Bomb2");
                }
            }
            else if (Input.GetAxis("Fire2") > 0)
            {
                Bomb();
                nextBombTime = Time.time + 2f / bombRate;
                //animator.SetTrigger("Bomb");
            }
        }
        
        if (spriteRenderer.flipX)
        {
            attackPointUp.localPosition = new Vector2(0, 0.15f);
            attackPointDown.localPosition = new Vector2(0, -0.15f);
        }
        else
        {
            attackPointUp.localPosition = new Vector2(0, 0.15f);
            attackPointDown.localPosition = new Vector2(0, -0.15f);
        }

        if (isAttackActive)
        {
            if (Time.time - queryStartTime >= attackDuration)
            {
                isAttackActive = false;
                if (hitsomething)
                {
                    _attackAnimTimer = 0f;
                    _attackCount++;
                    if (_attackCount > 3f)
                    {
                        _attackCount = 1;
                    }
                    hitsomething = false;
                }
                else
                {
                    _attackCount = 1;
                    hitsomething = false;
                }
            }
            else
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
                foreach (Collider2D enemyCollider in hitEnemies)
                {
                    if (Time.time - lastHitTime >= hitCooldown)
                    {
                        if (_attackCount >= 3)
                        {
                            enemyCollider.GetComponent<Enemy>().TakeDamage(attackDamageCombo, transform.position);
                            KnockbackHitting(enemyCollider.transform, enemyCollider.GetComponent<Enemy>().GetKnockback());
                            CameraController.Instance.ScreenShake(.3f, .1f);
                        }
                        else
                        {
                            enemyCollider.GetComponent<Enemy>().TakeDamage(attackDamage, transform.position);
                            KnockbackHitting(enemyCollider.transform, enemyCollider.GetComponent<Enemy>().GetKnockback());
                            hitsomething = true;
                        }
                        hitsomething = true;
                        lastHitTime = Time.time;
                    }
                    
                }

                Collider2D[] hitEnemiesGun = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyGunLayers);
                foreach (Collider2D enemyGunCollider in hitEnemiesGun)
                {
                    if (_attackCount >= 3)
                    {
                        enemyGunCollider.GetComponent<Enemy>().TakeDamage2(attackDamageCombo, transform.position);
                        KnockbackHitting(enemyGunCollider.transform, enemyGunCollider.GetComponent<Enemy>().GetKnockback());
                        CameraController.Instance.ScreenShake(.3f, .1f);
                    }
                    else
                    {
                        enemyGunCollider.GetComponent<Enemy>().TakeDamage2(attackDamage, transform.position);
                    KnockbackHitting(enemyGunCollider.transform, enemyGunCollider.GetComponent<Enemy>().GetKnockback());

                        
                    }
                    hitsomething = true;
                }

                Collider2D[] hitBala = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyBalaLayers);
                foreach (Collider2D balaCollider in hitBala)
                {
                    balaCollider.gameObject.SetActive(false);
                }

                Collider2D[] hitDoor = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, doorLayers);
                foreach (Collider2D door in hitDoor)
                {
                    if(gameObject.GetComponent<Player>()._playerType == Player.PlayerType.Bito)
                    {
                        door.GetComponent<DoorController2>().DoSomeShit();
                    }
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
                    enemyCollider.GetComponent<Enemy>().TakeDamage(attackDamage, transform.position);
                    KnockbackHitting(enemyCollider.transform, enemyCollider.GetComponent<Enemy>().GetKnockback());
                }

                Collider2D[] hitEnemiesGun = Physics2D.OverlapCircleAll(attackPointUp.position, attackRange, enemyGunLayers);
                foreach (Collider2D enemyGunCollider in hitEnemiesGun)
                {
                    enemyGunCollider.GetComponent<Enemy>().TakeDamage2(attackDamage, transform.position);
                    KnockbackHitting(enemyGunCollider.transform, enemyGunCollider.GetComponent<Enemy>().GetKnockback());
                }

                Collider2D[] hitBala = Physics2D.OverlapCircleAll(attackPointUp.position, attackRange, enemyBalaLayers);
                foreach (Collider2D balaCollider in hitBala)
                {
                    balaCollider.gameObject.SetActive(false);
                }
            }
        }

        if (isAttackDownActive) //No recomiendo knockback porque podes abusarlo para ganar momentum xd
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
                    enemyCollider.GetComponent<Enemy>().TakeDamage2(attackDamage, transform.position);
                }

                Collider2D[] hitEnemiesGun = Physics2D.OverlapCircleAll(attackPointDown.position, attackRange, enemyGunLayers);
                foreach (Collider2D enemyGunCollider in hitEnemiesGun)
                {
                    enemyGunCollider.GetComponent<Enemy>().TakeDamage2(attackDamage, transform.position);
                }

                Collider2D[] hitBala = Physics2D.OverlapCircleAll(attackPointDown.position, attackRange, enemyBalaLayers);
                foreach (Collider2D balaCollider in hitBala)
                {
                    balaCollider.gameObject.SetActive(false);
                }
            }
        }
        
         _attackAnimTimer += Time.deltaTime; 

        if (_attackAnimTimer >= _attackAnimDelay)
        {
            _attackCount = 1;
            hitsomething = false;
            _attackAnimTimer = 0f;
        }  

    }

    
    public virtual void Attack()
    {
        if (spriteRenderer.flipX)
        {
            attackPoint.localPosition = new Vector2(-0.15f, 0);
            //attackPointVisual.localPosition = new Vector2(0.15f, 0);
        }
        else
        {
            attackPoint.localPosition = new Vector2(0.15f, 0);
            //attackPointVisual.localPosition = new Vector2(-0.15f, 0);
        }
        
        _attackAnimTimer = 0f;
        hitsomething = false;
        

        if (_attackCount == 1)
        {
            animator.SetTrigger("Attack1");
            Instantiate(attackPointVisual, attackPoint);
            return;
        }
        if (_attackCount == 2)
        {
            animator.SetTrigger("Attack2");
            Instantiate(attackPointVisual, attackPoint);
            return;
        }
        if (_attackCount == 3)
        {
            animator.SetTrigger("Attack3");
            Instantiate(attackPointVisual, attackPoint);
            return;
        }

    }

    public virtual void AttackUp()
    {

        animator.SetTrigger("AttackUp");
        animator.SetBool("IsJumping", false);
        Instantiate(attackPointUpVisual, attackPointUp);

        Collider2D[] hitDoor = Physics2D.OverlapCircleAll(attackPointUp.position, attackRange, doorLayers);

        foreach (Collider2D door in hitDoor)
        {
            if (gameObject.GetComponent<Player>()._playerType == Player.PlayerType.Bito)
            {
                door.GetComponent<DoorController2>().DoSomeShit();
            }
            
        }

    }

    public virtual void AttackDown()
    {

        animator.SetTrigger("AttackDown");
        animator.SetBool("IsJumping", false);
        Instantiate(attackPointDownVisual, attackPointDown);


        Collider2D[] hitDoor = Physics2D.OverlapCircleAll(attackPointDown.position, attackRange, doorLayers);

        foreach (Collider2D door in hitDoor)
        {
            if (gameObject.GetComponent<Player>()._playerType == Player.PlayerType.Bito)
            {
                door.GetComponent<DoorController2>().DoSomeShit();
            }
        }

    }

    public virtual void Bomb()
    {
        animator.SetTrigger("Bomb");
        Invoke(nameof(InstantiateBomb), 0.15f);
    }

    private void InstantiateBomb()
    {
        if (spriteRenderer.flipX)
        {
            launchOffset.localPosition = new Vector2(-0.15f, 0);
            Instantiate(projectilePrefab, launchOffset.position, transform.rotation); //projectilePrefab
        }
        else
        {
            launchOffset.localPosition = new Vector2(0.15f, 0);
            Instantiate(projectilePrefab, launchOffset.position, transform.rotation); //projectilePrefab
        }
    }
    public virtual void Bomb2()
    {
        if (spriteRenderer.flipX)
        {
            launchOffset.localPosition = new Vector2(-0.15f, -0.12f);
            Instantiate(_jabulani, launchOffset.position, transform.rotation); //projectilePrefab
        }
        else
        {
            launchOffset.localPosition = new Vector2(0.15f, -0.12f);
            Instantiate(_jabulani, launchOffset.position, transform.rotation); //projectilePrefab
        }

    }

    void KnockbackHitting(Transform _enemyRef, float _knockback)
    {
        gameObject.GetComponent<Player>()._coolingHit = true;
        StartCoroutine(gameObject.GetComponent<Player>().StartCooldownCooling());
        Vector2 dir = transform.position - _enemyRef.position;
        dir = dir.normalized;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(dir.x * _knockback, 0),ForceMode2D.Impulse);
        //GetComponent<Rigidbody2D>().AddForce(new Vector2(dir.x * _knockback, dir.y),ForceMode2D.Impulse);
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
