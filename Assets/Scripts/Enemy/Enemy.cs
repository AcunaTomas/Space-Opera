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

    [SerializeField]
    private bool _Inmortal = false;
    private bool isDead;

    [SerializeField]
    private bool _MakeFunctionCallOnHit;
    
    [SerializeField]
    private UnityEvent _callWhat;
    private UpdateBars _ub;

    void Start()
    {
        currentHealth = maxHealth;
        GameObject player = GameObject.FindWithTag("Player");
        
        _ub = GameObject.FindWithTag("LifeBar").GetComponent<UpdateBars>();
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(player.transform.GetChild(5).GetComponent<CircleCollider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(player.transform.GetChild(6).GetComponent<CircleCollider2D>(), GetComponent<Collider2D>());
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Daño");
        animator.SetTrigger("Hurt");
        GetComponent<EnemyBehaviour2>().enabled = false;

        if (currentHealth <= 0 && !_Inmortal) 
        {   
            Die();
        }
        if (_MakeFunctionCallOnHit)
        {
            _callWhat.Invoke();
        }

    }

    public void changeCallback(UnityEvent a)
    {
        _callWhat = a;
    }

    void Die()
    {
        isDead = true;
        Debug.Log("Enemy Died");
        animator.SetTrigger("Die");
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
