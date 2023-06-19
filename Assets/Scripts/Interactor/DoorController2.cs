using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController2 : MonoBehaviour
{
    public int maxHealth = 10;
    int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        
    }

    public void TakeDamageDoor(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0) 
        {   
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        Destroy(GameObject.FindWithTag("Puerta3"));
    }
}
