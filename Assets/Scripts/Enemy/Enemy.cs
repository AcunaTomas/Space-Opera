using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//hola
//hola :D
public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    private GameObject player;
    private bool isDead;

    void Start()
    {
        currentHealth = maxHealth;
        GameObject player = GameObject.FindWithTag("Player");
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Daño");
        animator.SetTrigger("Hurt");
        GetComponent<EnemyBehaviour2>().enabled = false;

        if (currentHealth <= 0) 
        {   
            Die();
        }

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
