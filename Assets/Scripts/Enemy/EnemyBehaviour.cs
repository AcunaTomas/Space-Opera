using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    GameObject player;
    [SerializeField]
    bool flip;
    //public float speed;

    [SerializeField]
    float agroRange;
    [SerializeField]
    float moveSpeed;
    
    Rigidbody2D rb2d;

    void Start() 
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        float distToPlayer = Vector2.Distance (transform.position, player.transform.position);
        
        if (distToPlayer < agroRange)
        {
            Chase();
            Debug.Log("Chase");
        }
        else
        {
            Debug.Log("Stop");
        }

        Vector3 scale = transform.localScale;

        if (player.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
            //transform.Translate(speed * Time.deltaTime, 0, 0);
            //Debug.Log("un lado");
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
            //transform.Translate(speed * Time.deltaTime * -1, 0, 0);
            //Debug.Log("el otro");
        }

        transform.localScale = scale;

    }


    void Chase() 
    {
        if (player.transform.position.x > transform.position.x)
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        else
        transform.Translate(moveSpeed * Time.deltaTime * -1, 0, 0);
    }

    void Patrol()
    {
        
    }
}
