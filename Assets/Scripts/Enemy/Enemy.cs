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

    // public string layer1;
    // public string layer2;

    void Start()
    {
        currentHealth = maxHealth;
        GameObject player = GameObject.FindWithTag("Player");
        
        // int layer1Index = LayerMask.NameToLayer(layer1);
        // int layer2Index = LayerMask.NameToLayer(layer2);

        // if (layer1Index >= 0 && layer2Index >= 0)
        // {
        //     Physics2D.IgnoreLayerCollision(layer1Index, layer2Index, true);
        // }
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        //Physics2D.IgnoreCollision(player.GetComponent<CircleCollider2D>(), GetComponent<Collider2D>());
        //Physics2D.IgnoreCollision(player.GetComponentInChildren<Collider2D>(), GetComponent<Collider2D>());
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
