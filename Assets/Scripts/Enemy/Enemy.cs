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

    [SerializeField]
    private bool _MakeFunctionCallOnHit;
    
    [SerializeField]
    private UnityEvent _callWhat;
    private UpdateBars _ub;

    public bool _coolingHit = false;
    public bool _coolingHit2 = false;

    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        currentHealth = maxHealth;
        GameObject player = GameObject.FindWithTag("Player");
        
        _ub = GameObject.FindWithTag("LifeBar").GetComponent<UpdateBars>();
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(player.GetComponent<CircleCollider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(player.transform.GetChild(5).GetComponent<CircleCollider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(player.transform.GetChild(6).GetComponent<CircleCollider2D>(), GetComponent<Collider2D>());

        _spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void TakeDamage(int damage)
    {
        if (!_coolingHit)
        {
            currentHealth -= damage;
            attackDamage = damage;
            animator.SetTrigger("Hurt");
            GetComponent<EnemyBehaviour2>().enabled = false;

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
            GetComponent<EnemyBehaviour2>().enabled = false;

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
        isDead = true;
        if (attackDamage < 50)
        {
            animator.SetTrigger("Die");
        }
        else
        {
            animator.SetTrigger("DieBomb");
        }
        
        _ub.EnergyPlusOne();

        Destroy(GetComponent<Rigidbody2D>());
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<EnemyBehaviour2>().enabled = false;
        GetComponentInChildren<HotZoneCheck>().enabled = false;

        this.enabled = false;
    }

    public void ActivateBehaviour()
    {
        if(isDead == false)
        {
            GetComponent<EnemyBehaviour2>().enabled = true;
        }   
    }


    
    
}
