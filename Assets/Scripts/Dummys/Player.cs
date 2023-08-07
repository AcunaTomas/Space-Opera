using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{

    [SerializeField]
    float HP = 193f;
    [SerializeField]
    float vertspid = -0.3f;
    public bool canIjump = true;
    bool _firstImpulse = true;
    bool wallijumpy = false;
    private Rigidbody2D body;
    [SerializeField]
    private float jumpLimit = 0f;
    [SerializeField]
    private float Xspeed = 0f;
    private Vector2 speedCaps = new Vector2(1.2f, 5f); //x: usado para el movimiento horizontal y valor temporal para el "arrastre" cuando se cae de una pared.
                                                     //y: usado para el movimiento vertical.
    [SerializeField]
    private float _maxVerticalSpeed = 5f; //El limite de velocidad en y actual
    [SerializeField]
    private int extrajumpcount = 1;
    [SerializeField]
    private float _lastJumpPress = 0f;
    [SerializeField]
    private float _fallingTime = 0f;

    [SerializeField]    
    private float WallJumpXDirection = 0f;

    [SerializeField]
    private float _wallJumpFreezeTimer = 0.5f;

    private float _wallJumpXtimeFreeze = 0.3f;

    private float _xSpeedNullifier = 1;

    [SerializeField]
    private bool _wallJumpXHandicap = false;

    private SpriteRenderer _spriteRenderer;

    private Animator _animator;
    [SerializeField]
    private UpdateBars _healthBar;
    [SerializeField]
    private UnityEvent _callWhat;
    private bool _playerFall = false;
    private float _airborneTime = 0f;


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        _plaseJump();
        
        WallJumpDelay();
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

        if (!canIjump && !wallijumpy)
        {
            _airborneTime += 0.1f;
        }
        else
        {
            _airborneTime = 0;
        }
        

        //Debug.Log(body.velocity.y);
    }

    private void _plaseJump()
    {
        
        if (canIjump && Input.GetButton("Jump"))
        {
            
           if (_lastJumpPress <= 0.30f && _firstImpulse == true)
           {
            vertspid = 1.3f + jumpLimit;
            _firstImpulse = false;
            
           }
           else
           {   
               vertspid = jumpLimit;
           }
            jumpLimit += 0.04f;
            
            _animator.SetBool("IsJumping", true);
            _animator.SetFloat("Speed", 1f);
        }
        if (jumpLimit >= 0.4f || (Input.GetButton("Jump") == false && _lastJumpPress > 0.12f)) // TO-DO: Encontrar una forma de reformular este or
        {
            //Debug.Log("Jump Peak");
            vertspid = 0f;
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
            _wallJumpFreezeTimer = 0f;
            _wallJumpXtimeFreeze = 0.3f;
            _wallJumpXHandicap = true;
            _xSpeedNullifier = 1;
            body.AddForce(new Vector2(WallJumpXDirection * 2f, 4f), ForceMode2D.Impulse);
            vertspid = 1f;
            
            _animator.SetBool("wall", false);
            Debug.Log("WallE");
            //Wall-e
        }
        
    }

    void WallJumpDelay()
    {
        if (_wallJumpFreezeTimer > 0 && wallijumpy)
        {
            body.velocity = new Vector2(body.velocity.x, 0);
            vertspid = 0;
            _wallJumpFreezeTimer += -0.016f;
            _xSpeedNullifier = 0;
            return;
        }
        _xSpeedNullifier = 1;
    }

    IEnumerator WallXHandicap()
    {
        _wallJumpXHandicap = true;
        yield return new WaitForSeconds(0.4f);
        _wallJumpXHandicap = false;
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
            if (Mathf.Abs(contacts[i].normal.y) > Mathf.Abs(contacts[i].normal.x) && contacts[i].normal.y > 0) //Contacto Vertical
            {
                canIjump = true;
                wallijumpy = false;
                _firstImpulse = true;
                extrajumpcount = 1;
                _animator.SetBool("IsJumping", false);
                _animator.SetBool("wall", false);
                _maxVerticalSpeed = speedCaps.y;
                _xSpeedNullifier = 1;
                if (_playerFall && _airborneTime > 0.8f)
                {
                    AudioManager.INSTANCE.PlayPlayerJump();
                    _playerFall = false;
                }

            }
            if (Mathf.Abs(contacts[i].normal.y) < Mathf.Abs(contacts[i].normal.x) && extrajumpcount > 0 && collision.gameObject.CompareTag("NotClimbable") == false) //Contacto Horizontal/Pared
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
                    _maxVerticalSpeed = speedCaps.x;
                    _animator.SetBool("wall", true);
                    _animator.SetBool("IsJumping", false);  
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ascensor"))
        {
            transform.parent = other.gameObject.transform;
        }
    }

        private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ascensor"))
        {
            transform.parent = null;
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

        // if (collision.gameObject.CompareTag("Enemy"))
        // {
        //     LoseHP();
        // }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(jumpLimit == 0)
        {
            canIjump = false;
            _playerFall = true;
        }

        wallijumpy = false;
        _maxVerticalSpeed = speedCaps.y;
        _wallJumpFreezeTimer = 0.5f;
        _xSpeedNullifier = 1;


        if (collision.gameObject.CompareTag("Ascensor"))
        {
            transform.parent = null;
        }
        
        _animator.SetBool("wall", false);
        
    }

    void Movement()
    {
        //Horizontal
        //orientation.transform.localPosition = new Vector2(Clamp(body.velocity.x, -1, 1), 0);
        if(_wallJumpXtimeFreeze > 0f)
        {
            _wallJumpXtimeFreeze += -0.016f;
        }
        else
        {
            _wallJumpXHandicap = false;
        }

        if (_wallJumpXHandicap == false)
        {
            //print("MOVETRUE");
            Xspeed = (Input.GetAxis("Horizontal") * 13.8f) * _xSpeedNullifier;
        }
        else
        {
            Xspeed = body.velocity.x;
        }
        if (canIjump == false)
        {
            vertspid += -0.1f;

        }
        _animator.SetFloat("Speed", Mathf.Abs(Xspeed));




        //body.velocity = new Vector2(Clamp(Xspeed, -speedCaps.x, speedCaps.x), vertspid);
        vertspid = Clamp(vertspid, 0f, speedCaps.y);
        body.AddForce(new Vector2(Xspeed, 0), ForceMode2D.Force);
        body.AddForce(new Vector2(0, vertspid), ForceMode2D.Impulse);
        if (Mathf.Abs(body.velocity.x) > speedCaps.x)
        {
            body.velocity = new Vector2(speedCaps.x * Clamp(body.velocity.x,-1,1), body.velocity.y);
        }
        if (Mathf.Abs(Input.GetAxis("Horizontal")) == 0)
        {
            if (canIjump) //airborne checko
            {
                body.velocity = new Vector2(0f, body.velocity.y);
            }
            else
            {
                body.velocity = new Vector2(Clamp(body.velocity.x,-0.7f,0.7f), body.velocity.y);
            }
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
    
    public void LoseHP(float damage)
    {
        HP -= damage;
        _animator.SetBool("IsJumping", false);
        _animator.SetTrigger("Hurt");
        _healthBar.UpdateHP();
        
        if (HP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        _animator.SetTrigger("Die");
        _animator.SetBool("isDead", true);
        GetComponent<PlayerCombat>().enabled = false;
        GetComponent<Player>().enabled = false;
        Invoke("Respawn", 1.7f);
        transform.parent = null;
    }

    public void AddHP(int hp)
    {
        HP = HP + hp;
        _healthBar.UpdateHP();
    }

    void Respawn() 
    {
        transform.parent = null;
        transform.localPosition = GameManager.INSTANCE.CHECKPOINT;
        _animator.SetTrigger("idle");
        _animator.SetBool("isDead", false);
        GetComponent<PlayerCombat>().enabled = true;
        GetComponent<Player>().enabled = true;
        HP = _healthBar.MaxHP();
        _healthBar.UpdateHP();
        if (GameManager.INSTANCE.ACTUAL_CHECKPOINT.name == "Checkpoint2")
        {
            _callWhat.Invoke();
        }
        
    }

    public void changeCallback(UnityEvent a)
    {
        _callWhat = a;
    }

    public void PlaySound(string a)
    {
        AudioManager.INSTANCE.gameObject.SendMessage(a);
    }
}
