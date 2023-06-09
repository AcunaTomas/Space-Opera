using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject orientation;
    [SerializeField]
    float HP = 193f;
    [SerializeField]
    float vertspid = -0.3f;
    [SerializeField]
    bool canIjump = true;
    bool wallijumpy = false;
    private Rigidbody2D body;
    [SerializeField]
    private float jumpLimit = 0f;
    [SerializeField]
    private float Xspeed = 0f;
    private Vector2 speedCaps = new Vector2(5f, 12f);
    [SerializeField]
    private float _maxVerticalSpeed = 15f;
    [SerializeField]
    private int extrajumpcount = 1;
    [SerializeField]
    private float _lastJumpPress = 0f;
    [SerializeField]
    private float _fallingTime = 0f;

    [SerializeField]    
    private float WallJumpXDirection = 0f;

    private SpriteRenderer _spriteRenderer;

    private Animator _animator;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        _plaseJump();
        Movement();
        if (Input.GetButton("Jump"))
        {
            _lastJumpPress += 0.17f;
        }
        else
        {
            _lastJumpPress = 0f;
        }

            Flip();

        if (canIjump == false && wallijumpy == false && body.velocity.y < 0)
        {
            _fallingTime += 0.16f;
            _animator.SetFloat("fallingTime", _fallingTime);
        }
        else
        {
            _fallingTime = 0;
            _animator.SetFloat("fallingTime", _fallingTime);
        }

        if (body.velocity.y < 0 && canIjump == false) 
        {
            _animator.SetFloat("speedY", body.velocity.y);
        }
        else
        {
            _animator.SetFloat("speedY", 1);
        }
    }

    private void _plaseJump()
    {
        
        if (canIjump && Input.GetButton("Jump"))
        {
           
            jumpLimit += 0.3f;
            vertspid = 1.2f + (jumpLimit * 0.12f);
            _animator.SetBool("IsJumping", true);
            _animator.SetFloat("Speed", 1f);
        }
        if (jumpLimit >= 2.0f || (Input.GetButton("Jump") == false && _lastJumpPress > 0.12f)) // TO-DO: Encontrar una forma de reformular este or
        {
            jumpLimit = 0f;
            canIjump = false;
        }
        if (wallijumpy && canIjump == false)
        {
            WallJump();
        }
        //Debug.Log(canIjump);

    }
    private void WallJump()
    {

        if (Input.GetButton("Jump") && _lastJumpPress <= 0.15f)
        {

            body.AddForce(new Vector2(WallJumpXDirection * 4f, 2f), ForceMode2D.Impulse);
            vertspid = 2.5f;
            
            _animator.SetBool("wall", false);
            Debug.Log("WallE");
        }
        
    }

    private float Clamp(float x, float y, float z)
    {
        if (x.CompareTo(y) < 0) return y;
        else if (x.CompareTo(z) > 0) return z;
        else return x;
    }

    public string GetHP()
    {
        return HP.ToString();
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        Debug.DrawRay(new Vector2(0,0), collision.GetContact(0).normal * -1, Color.red);
        Debug.DrawRay(transform.position, transform.position.normalized, Color.green);
        //Debug.Log(collision.GetContact(0).normal);
        ContactPoint2D[] contacts= new ContactPoint2D[8];
        collision.GetContacts(contacts);

        //Debug.Log(normalcoll.y);
        //Debug.Log(normalcoll.x);

        for (var i = 0; i < collision.contactCount; i++)
        {
            if (Mathf.Abs(contacts[i].normal.y) > Mathf.Abs(contacts[i].normal.x) && contacts[i].normal.y > 0)
            {
                canIjump = true;
                wallijumpy = false;
                extrajumpcount = 1;
                _animator.SetBool("IsJumping", false);
                _animator.SetBool("wall", false);
                _maxVerticalSpeed = 15f;

            }
            if (Mathf.Abs(contacts[i].normal.y) < Mathf.Abs(contacts[i].normal.x) && extrajumpcount > 0)
            {
                if (canIjump == false) 
                {
                    wallijumpy = true;

                    if ((contacts[i].normal.x) > 0) 
                    {
                        _spriteRenderer.flipX = true;
                    } 
                    else
                    {
                        _spriteRenderer.flipX = false;
                    }

                    //extrajumpcount += -1;
                    WallJumpXDirection = Clamp(contacts[i].normal.x, -1, 1);
                    _maxVerticalSpeed = 5f;
                    _animator.SetBool("wall", true);
                    _animator.SetBool("IsJumping", false);
                }
            }
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (_fallingTime > 6)
        {
            _animator.SetTrigger("land");
        }
        
        if (collision.gameObject.CompareTag("Ascensor"))
        {
            transform.parent = collision.gameObject.transform;
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(jumpLimit == 0)
        {
            canIjump = false;
        }

        wallijumpy = false;
        _maxVerticalSpeed = 15f;

        if (collision.gameObject.CompareTag("Ascensor"))
        {
            transform.parent = null;
        }
        
        _animator.SetBool("wall", false);
        
    }

    void Movement()
    {
        //Horizontal
        orientation.transform.localPosition = new Vector2(Clamp(body.velocity.x, -1, 1), 0);
        Xspeed = Input.GetAxis("Horizontal") * 15f;
        if (canIjump == false)
        {
            vertspid += -0.3f;

        }
        _animator.SetFloat("Speed", Mathf.Abs(Xspeed));




        //body.velocity = new Vector2(Clamp(Xspeed, -speedCaps.x, speedCaps.x), vertspid);
        vertspid = Clamp(vertspid, -0.5f, speedCaps.y);
        body.AddForce(new Vector2(Xspeed, 0), ForceMode2D.Force);
        body.AddForce(new Vector2(0, vertspid), ForceMode2D.Impulse);
        if (Mathf.Abs(body.velocity.x) > speedCaps.x)
        {
            body.velocity = new Vector2(speedCaps.x * orientation.transform.localPosition.x, body.velocity.y);
        }
        if (Mathf.Abs(Input.GetAxis("Horizontal")) == 0 && canIjump == true)
        {
            body.velocity = new Vector2(0f, body.velocity.y);
        }
        if (Mathf.Abs(body.velocity.y) > _maxVerticalSpeed)
        {
            body.velocity = new Vector2(body.velocity.x, _maxVerticalSpeed * Clamp(body.velocity.y, -1, 1));  //Huge reach
        }

            //just checking if I understood how to normalized a vector
           /* float magnitude = Mathf.Sqrt(transform.position.x * transform.position.x + transform.position.y * transform.position.y);
            Vector2 normal = new Vector2(transform.position. x / magnitude, transform.position.y / magnitude);
            Debug.Log("Formula: " + normal.ToString());
            Debug.Log("Built-In: " + transform.position.normalized.ToString());
           */
            //Debug.Log(transform.localPosition);
    }

    //Animaciones

    private void Flip()
        {
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                _spriteRenderer.flipX = true;
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                _spriteRenderer.flipX = false;
            }

        }
   /*  void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(orientation.transform.position, 1);
    } */

    // void OnCollisionEnter2D(Collision2D coll)
    // {
    //     if (coll.gameObject.CompareTag("Ascensor"))
    //     {
    //         transform.parent = coll.gameObject.transform;
    //     }
    // }

    //     void OnCollisionExit2D(Collision2D coll)
    // {
    //     if (coll.gameObject.CompareTag("Ascensor"))
    //     {
    //         transform.parent = null;
    //     }
    // }
}
