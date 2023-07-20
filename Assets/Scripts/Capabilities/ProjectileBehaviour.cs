using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public Transform bombPoint;
    public LayerMask enemyLayers;
    public float bombRange = 1f;
    public int bombDamage = 80;
    
    public float speed = 4; 
    public Vector3 launchOffset;
    public bool thrown;
    private SpriteRenderer spriteRenderer;
    private Collider2D rango;
    [SerializeField]
    private Rigidbody2D rb;

    public Animator animator;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");

        spriteRenderer = player.GetComponent<SpriteRenderer>();
        rango = GetComponent<BoxCollider2D>();
        Throw();
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    public void Update()
    {
        if(!thrown)
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
    }

    void Throw()
    {
        if(thrown && spriteRenderer.flipX)
        {
            var direction = -transform.right + Vector3.up;
            GetComponent<Rigidbody2D>().AddForce(direction * speed ,ForceMode2D.Impulse);
        }
        else
        {
            var direction = transform.right + Vector3.up;
            GetComponent<Rigidbody2D>().AddForce(direction * speed ,ForceMode2D.Impulse);
        }
        transform.Translate(launchOffset);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(bombPoint.position, bombRange, enemyLayers);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        
        foreach (Collider2D enemyCollider in hitEnemies)
        {
            enemyCollider.GetComponent<Enemy>().TakeDamage(bombDamage);
        }

        animator.SetTrigger("Explode");
        Destroy(gameObject, 0.7f);
    }

    void OnDrawGizmosSelected() 
    {
        if (bombPoint == null){
            return;
        }
        Gizmos.DrawWireSphere(bombPoint.position, bombRange);
    }
}
