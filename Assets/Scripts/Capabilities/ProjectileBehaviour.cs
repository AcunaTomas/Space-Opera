using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public Transform bombPoint;
    public LayerMask enemyLayers;
    public LayerMask enemyGunLayers;
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

    private bool _coolingCollision = false;

    public float bombDuration = 0.5f;
    private bool isBombActive = false;
    private float queryStartTime;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");

        spriteRenderer = player.GetComponent<SpriteRenderer>();
        rango = GetComponent<BoxCollider2D>();
        Throw();
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(player.GetComponent<CircleCollider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(player.transform.GetChild(5).GetComponent<CircleCollider2D>(), GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(player.transform.GetChild(6).GetComponent<CircleCollider2D>(), GetComponent<Collider2D>());
    }

    public void FixedUpdate()
    {
        if(!thrown)
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }

        if (isBombActive)
        {
            if (Time.time - queryStartTime >= bombDuration)
            {
                isBombActive = false;
            }
            else
            {
                Debug.Log("Explota");
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(bombPoint.position, bombRange, enemyLayers);
                //rb.constraints = RigidbodyConstraints2D.FreezeAll;
            
                foreach (Collider2D enemyCollider in hitEnemies)
                {
                    enemyCollider.GetComponent<Enemy>().TakeDamage2(bombDamage);
                }

                Collider2D[] hitEnemiesGun = Physics2D.OverlapCircleAll(bombPoint.position, bombRange, enemyGunLayers);
                foreach (Collider2D enemyGunCollider in hitEnemiesGun)
                {
                    enemyGunCollider.GetComponent<Enemy>().TakeDamage2(bombDamage);
                }
            }
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
        if (!_coolingCollision)
        {
            isBombActive = true;
            queryStartTime = Time.time;

            AudioManager.INSTANCE.PlayPlayerBomb();
            //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(bombPoint.position, bombRange, enemyLayers);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            
            //foreach (Collider2D enemyCollider in hitEnemies)
            //{
                //enemyCollider.GetComponent<Enemy>().TakeDamage2(bombDamage);
            //}

            animator.SetTrigger("Explode");
            Destroy(gameObject, 0.7f);

            _coolingCollision = true;
            StartCoroutine(StartCooldown());
        }
        
    }

    private IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(0.7f);

        _coolingCollision = false;
    }

    void OnDrawGizmosSelected() 
    {
        if (bombPoint == null){
            return;
        }
        Gizmos.DrawWireSphere(bombPoint.position, bombRange);
    }
}
