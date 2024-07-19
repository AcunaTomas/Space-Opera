using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JabulaniBehaviour : MonoBehaviour
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

    private SpriteRenderer _SP;

    private bool canMove = false;
    private bool _coolingCollision = false;
    //private bool isBombActive = false;
    private float queryStartTime;

    [SerializeField]
    private float _lifetime = 15f;

    [SerializeField]
    private float _explosionDuration = 0.5f;

    private Vector2 _direction;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = player.GetComponent<SpriteRenderer>();
        _SP =             GetComponent<SpriteRenderer>();
        rango = GetComponent<BoxCollider2D>();
        thrown = true;
        Throw();
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    public void FixedUpdate()
    {
        Debug.Log(rb.velocity);
        Debug.Log(_direction);
        if (canMove)
        {
            rb.AddForce(_direction * speed, ForceMode2D.Force);
            
        }
        rb.velocity = new Vector2(Mathf.Clamp( rb.velocity.x,-speed,speed), rb.velocity.y);
    }


    void Throw()
    {
        if(thrown && spriteRenderer.flipX)
        {
            _direction = -transform.right; // + Vector3.up;
            _SP.flipX = true;
            //GetComponent<Rigidbody2D>().AddForce(direction * speed ,ForceMode2D.Impulse);
        }
        else
        {
            _direction = transform.right; // + Vector3.up;
            //GetComponent<Rigidbody2D>().AddForce(direction * speed ,ForceMode2D.Impulse);
        }
        Invoke("EnableMovement", _explosionDuration);
        thrown = false;
        //transform.Translate(launchOffset);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D[] list = new ContactPoint2D[5] ; 
        collision.GetContacts(list);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            canMove = false;
            animator.SetTrigger("Explode");
            collision.gameObject.GetComponent<Enemy>().TakeDamage(bombDamage, transform.position);
            Destroy(gameObject, _explosionDuration);
        }

        if (!_coolingCollision)
        {
            Destroy(gameObject, _lifetime);

            _coolingCollision = true;
            StartCoroutine(StartCooldown());
        }

        for (int i = 0; i < collision.contactCount; i++)
        {
            if (Mathf.Abs(list[i].normal.y) < Mathf.Abs(list[i].normal.x)) //Horizontal contact
            {
                _direction = -_direction;
                _SP.flipX = !_SP.flipX;
            }

        }        
        
    }

    private IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(_lifetime - _explosionDuration);

        animator.SetTrigger("Explode");
        GetComponent<FMODUnity.StudioEventEmitter>().Play();

        _coolingCollision = false;
    }

    void OnDrawGizmosSelected() 
    {
        if (bombPoint == null){
            return;
        }
        Gizmos.DrawWireSphere(bombPoint.position, bombRange);
    }

    void EnableMovement()
    {
        canMove = true;
        animator.SetTrigger("Run");
    }
}


