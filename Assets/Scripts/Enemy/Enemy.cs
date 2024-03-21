using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//hola
//hola :D
//hi 0_0
public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    private GameObject player;
    private int attackDamage;

    [SerializeField]
    private bool _Inmortal = false;
    private bool isDead;

    public bool _MakeFunctionCallOnHit;
    
    [SerializeField]
    private UnityEvent _callWhat;
    private UpdateBars _ub;

    public bool _coolingHit = false;
    public bool _coolingHit2 = false;

    private SpriteRenderer _spriteRenderer;

    public EnemyType _enemyType;
    public enum EnemyType
    {
        melee,
        gun,
        simple,
        shoot,
        boss,
        shield
    }

    [SerializeField]
    private GameObject _objectToDisable;

    [Header("KnockBack")]
    [SerializeField] private float knockbackRecibido = 0f; //.0.2f va
    [SerializeField] private float knockbackDado = 0f; //3f va


    void Start()
    {

        switch (_enemyType)
        {
            default:

                break;
        }


        currentHealth = maxHealth;
        GameObject player = GameObject.FindWithTag("Player");
        
        _ub = GameObject.FindWithTag("LifeBar").GetComponent<UpdateBars>();
        Physics2D.IgnoreCollision(player.GetComponent<BoxCollider2D>(), GetComponent<Collider2D>());
        //Physics2D.IgnoreCollision(player.GetComponent<CircleCollider2D>(), GetComponent<Collider2D>());
        //Physics2D.IgnoreCollision(player.transform.GetChild(5).GetComponent<CircleCollider2D>(), GetComponent<Collider2D>());
        //Physics2D.IgnoreCollision(player.transform.GetChild(6).GetComponent<CircleCollider2D>(), GetComponent<Collider2D>());

        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("BalaPlayer"))
    //     {
    //         TakeDamage(40);
    //     }
    // }

    public void TakeDamage(int damage, Vector2 sourcePosition)
    {
        if(knockbackRecibido > 0)
        {
            Debug.Log("EmpujoEnem");
            Vector2 dir = (Vector2)transform.position - sourcePosition;
            GetComponent<Rigidbody2D>().AddForce(dir.normalized * knockbackRecibido, ForceMode2D.Impulse);
        }

        if (!_coolingHit && !isDead)
        {
            currentHealth -= damage;
            attackDamage = damage;
            animator.SetTrigger("Hurt");

            if (_enemyType == EnemyType.melee)
            {
                GetComponent<EnemyBehaviour2>().enabled = false;
            }

            if (_enemyType == EnemyType.gun)
            {
                GetComponent<EnemyShooting2>().enabled = false;
            }

            if (_enemyType == EnemyType.simple)
            {
                GetComponent<EnemyPatrol>()._waiting = false;
                GetComponent<EnemyPatrol>().NoHit();
            }

            AudioManager.INSTANCE.PlayEnemyHit();

            if (currentHealth <= 0 && !_Inmortal) 
            {   
                Die();
            }
            if (_MakeFunctionCallOnHit)
            {
                _callWhat.Invoke();
            }
            _coolingHit = true;
            StartCoroutine(StartCooldown());

            if (_spriteRenderer.flipX == true)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0), ForceMode2D.Impulse);
            }
            else
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0), ForceMode2D.Impulse);
            }
        }
    }

    public void TakeDamage2(int damage)
    {
        if (!_coolingHit2)
        {
            currentHealth -= damage;
            attackDamage = damage;
            animator.SetTrigger("Hurt");

            if (_enemyType == EnemyType.melee)
            {
                GetComponent<EnemyBehaviour2>().enabled = false;
            }

            if (_enemyType == EnemyType.gun)
            {
                GetComponent<EnemyShooting2>().enabled = false;
            }


            AudioManager.INSTANCE.PlayEnemyHit();

            if (currentHealth <= 0 && !_Inmortal) 
            {   
                Die();
            }
            if (_MakeFunctionCallOnHit)
            {
                _callWhat.Invoke();
            }
            _coolingHit2 = true;
            StartCoroutine(StartCooldown2());
            
        }
    }

    private IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(0.5f);

        _coolingHit = false;
    }
    private IEnumerator StartCooldown2()
    {
        yield return new WaitForSeconds(0.5f);

        _coolingHit2 = false;
    }
    public void changeCallback(UnityEvent a)
    {
        _callWhat = a;
    }

    void Die()
    {
        if (GameManager.INSTANCE.LEVEL == 1)
        {
            AchievementsManager.INSTANCE.Achievement01();
        }

        if (GameManager.INSTANCE.LEVEL == 3)
        {
            AchievementsManager.INSTANCE.YouFuckingKilledAnimals();
        }
        isDead = true;

        if (_enemyType == EnemyType.boss)
        {
            animator.SetTrigger("Die");
            animator.SetBool("Died", true);
            GetComponent<BossController>().enabled = false;
            GetComponentInChildren<HurtBoxFly>().enabled = false;
        }

        if (_enemyType == EnemyType.shoot)
        {
            animator.SetTrigger("Die");
            GetComponent<EnemyFireBehaviour>().enabled = false;

        }

        if (_enemyType == EnemyType.simple)
        {
            animator.SetTrigger("Die");
            GetComponent<EnemyPatrol>().enabled = false;
            GetComponentInChildren<HurtBoxFly>().enabled = false;

            _objectToDisable.SetActive(false);


        }

        if (attackDamage < 50)
        {
            animator.SetTrigger("Die");
        }
        else
        {
            if (_enemyType == EnemyType.melee || _enemyType == EnemyType.gun)
            {
                AchievementsManager.INSTANCE.Achievement04();
                animator.SetTrigger("DieBomb");
            }    
        }
        
        try
        {
            _ub.EnergyPlusOne();   
        }
        catch (System.Exception)
        {
            
        }

        Destroy(GetComponent<Rigidbody2D>());
        GetComponent<BoxCollider2D>().enabled = false;

        if (_enemyType == EnemyType.melee)
        {
            GetComponent<EnemyBehaviour2>().enabled = false;
        }

        if (_enemyType == EnemyType.gun)
        {
            GetComponent<EnemyShooting2>().enabled = false;
        }

        try
        {
            GetComponentInChildren<HotZoneCheckShooting>().enabled = false;
        }
        catch(System.Exception)
        {
        }

        this.enabled = false;
    }

    public void ActivateBehaviour()
    {
        if(isDead == false)
        {
            if (_enemyType == EnemyType.melee)
            {
                GetComponent<EnemyBehaviour2>().enabled = true;
            }

            if (_enemyType == EnemyType.gun)
            {
                GetComponent<EnemyShooting2>().enabled = true;
            }
        }   
    }

    public float GetKnockback()
    {
        return knockbackDado;
    }
    
    
}
