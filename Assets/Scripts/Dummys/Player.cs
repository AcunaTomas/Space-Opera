using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private Object dustEffect;
    [SerializeField]
    float HorizontalInputValue;
    [SerializeField]
    public int HP = 5;
    [SerializeField]
    int MaxHP = 5;
    [SerializeField]
    float vertspid = -0.3f;
    public bool canIjump = true;
    bool _firstImpulse = true;
    bool wallijumpy = false;
    public Rigidbody2D body;
    [SerializeField]
    private float jumpLimit = 0f;
    [SerializeField]
    public float Xspeed = 0f;
    private Vector2 speedCaps = new Vector2(1.2f, 4f); //x: usado para el movimiento horizontal y valor temporal para el "arrastre" cuando se cae de una pared.
                                                     //y: usado para el movimiento vertical.
    [SerializeField]
    private float _maxVerticalSpeed = 5f; //El limite de velocidad en y actual
    [SerializeField]
    private int extrajumpcount = 1;
    [SerializeField]
    private float _lastJumpPress = 0f;
    
    public float _fallingTime = 0f;

    [SerializeField]    
    private float WallJumpXDirection = 0f;

    [SerializeField]
    private float _wallJumpFreezeTimer = 0.5f;

    private float _wallJumpXtimeFreeze = 0.3f;

    private float _coyoteValidTime;

    private float _xSpeedNullifier = 1;

    [SerializeField]
    private bool _wallJumpXHandicap = false;

    public SpriteRenderer _spriteRenderer;

    public Animator _animator;
    [SerializeField]
    private UpdateBars _healthBar;
    [SerializeField]
    private UnityEvent _callWhat;
    private bool _playerFall = false;
    [SerializeField]
    private float _airborneTime = 0f;

    private bool _skillPermitted = true;
    private float _skillHold;


    public float dashRate = 2f;
    float nextDashTime = 0f;

    public bool _coolingHit = false;
    public bool _coolingDashAnim = false;
    public bool _coolingShootDown = false;
    public bool _coolingRespawn = false;

    public Animator _effectAnimator;
    public Animator _landingAnimator1;
    public Animator _landingAnimator2;

    private float _timerBreak = 0f;

    private float dustSpawnCooldown = 0f;

    public struct parama
    {
        public bool a;
        public Vector2 b;
        
        public parama(bool ab, Vector2 bb)
        {
            a = ab;
            b = bb;
        }
    }


    public void ChangeSkillStatus(bool a)
    {
        _skillPermitted = a;
    }

    public float GetOrientation()
    {
        if (_spriteRenderer.flipX)
        {
            return -1;
        }
        if (_spriteRenderer.flipX == false)
        {
            return 1;
        }
        return 0;
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        dustEffect = Resources.Load("Prefabs/dustEffect");
    }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0)
        {
            _timerBreak += Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        HorizontalInputValue = Input.GetAxis("Horizontal"); 
        _plaseJump();
        WallJumpDelay();
        Movement();
        Flip();

        if (Input.GetButton("Jump"))
        {
            _lastJumpPress += 0.17f;
        }
        else
        {
            _lastJumpPress = 0f;
        }
        if (Input.GetButton("Whoosh"))
        {
            _skillHold += 0.17f;
        }
        else
        {
            _skillHold = 0f;
        }

        

        if (canIjump == false && wallijumpy == false && body.velocity.y < 0)
        {
            if (!_coolingDashAnim && !_coolingShootDown) 
            { 
                _fallingTime += 0.16f;
                _animator.SetFloat("fallingTime", _fallingTime);
            }
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
        
        if (Input.GetAxis("Whoosh") > 0 && _skillPermitted && _skillHold <= 0.17f && Time.time >= nextDashTime)
        {
            TheTheSkill();
            ChangeSkillStatus(false);
            nextDashTime = Time.time + 1f / dashRate;
        }

        if (_coyoteValidTime > 0)
        {
            _coyoteValidTime -= Time.deltaTime;
            if (Clamp(_coyoteValidTime, 0, 1) <= 0 && jumpLimit == 0)
            {
                _coyoteValidTime = 0;
                canIjump = false;
            }
        }

        if (dustSpawnCooldown > 0)
        {
            dustSpawnCooldown -=0.017f;
        }
        else
        {
            dustSpawnCooldown = 0f;
        }

    }

    private void _plaseJump()
    {
        
        if (canIjump && Input.GetButton("Jump"))
        {
            
           if (_lastJumpPress <= 0.30f)
           {
            vertspid = 1.3f + jumpLimit;
            //_firstImpulse = false;
            
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
        if (wallijumpy == false && canIjump == false && extrajumpcount >= 2)
        {
            if (Input.GetButton("Jump") && (_lastJumpPress <= 0.16f))
            {
                SpecialJump();
            }
        }
        //Debug.Log(canIjump);

    }

    public virtual void SpecialJump() //Defaults to double jump, Override it if you want to change its behaviour
    {
        body.velocity = new Vector2(body.velocity.x, 0);
        body.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
        extrajumpcount -= 1;
        _animator.SetBool("IsJumping", false);
        _animator.SetTrigger("DoubleJump");
        _effectAnimator.SetTrigger("Effect");
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

    public void setXStunVariables() //for wall jump, negates x input for a short time
    {
        _wallJumpFreezeTimer = 0f;
        _wallJumpXtimeFreeze = 0.1f;
        _wallJumpXHandicap = true;
        speedCaps = new Vector2(4, speedCaps.y);
    }

    private void unsetXStunVariables() 
    {

        speedCaps = new Vector2(1.2f, speedCaps.y);
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

    public int GetHP()
    {
        return HP;
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
                ChangeSkillStatus(true);
                canIjump = true;
                wallijumpy = false;
                _firstImpulse = true;
                extrajumpcount = 2;
                _animator.SetBool("IsJumping", false);
                _animator.SetBool("wall", false);
                
                if (_animator.GetBool("Dash"))
                {
                    StartCoroutine(StopDashAnim());
                }
                    
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
                    unsetXStunVariables();

                    if (extrajumpcount == 1)
                    {
                        extrajumpcount += 1;
                    }

                    WallJumpXDirection = Clamp(contacts[i].normal.x, -1, 1);
                    _maxVerticalSpeed = speedCaps.x;
                    _animator.SetBool("wall", true);
                    _animator.SetBool("IsJumping", false);  
                }
            }
        }
    }

    //Elevator quick fix
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

    void OnCollisionEnter2D(Collision2D collision) //for when you hit the ground
    {
        if (_fallingTime > 6)
        {
            _animator.SetTrigger("land");
            _landingAnimator1.SetTrigger("Effect");
            _landingAnimator2.SetTrigger("Effect");
        }
        
        if (collision.gameObject.CompareTag("Ascensor"))
        {
            transform.parent = collision.gameObject.transform;
        }
        extrajumpcount = 1;

        _animator.SetBool("Dash", false);

        // if (collision.gameObject.CompareTag("Enemy"))
        // {
        //     LoseHP();
        // }

    }

    void OnCollisionExit2D(Collision2D collision) //for when you leave the ground
    {
        if(jumpLimit == 0) //If the jump peaked or ended, set up the falling animation to play
        {
            //canIjump = false;
            _playerFall = true;
        }

        wallijumpy = false;
        _maxVerticalSpeed = speedCaps.y;
        _wallJumpFreezeTimer = 0.5f;
        _xSpeedNullifier = 1;
        _coyoteValidTime = 0.1f;

        if (collision.gameObject.CompareTag("Ascensor"))
        {
            transform.parent = null;
        }
        
        _animator.SetBool("wall", false);
        
    }

    void Movement()
    {
        //Horizontal
        if(_wallJumpXtimeFreeze > 0f)
        {
            _wallJumpXtimeFreeze += -0.016f;
        }
        else
        {
            _wallJumpXHandicap = false;
            speedCaps = new Vector2(1.2f, speedCaps.y);
        }

        if (_wallJumpXHandicap == false)
        {
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

        if (Input.GetAxis("Horizontal") > 0 && _spriteRenderer.flipX == true && canIjump) //Spawns dust
        {

            if (GameManager.INSTANCE.dustcap > 0)
            {
                return;
            }
                
            Debug.Log("Drift");     
            var a = Instantiate(dustEffect) as GameObject;
            a.gameObject.SendMessage("Initialize", new parama(true, transform.position));
            GameManager.INSTANCE.dustcap = 1;
            
        }
        if (Input.GetAxis("Horizontal") < 0 && _spriteRenderer.flipX == false && canIjump)
        {

            if (GameManager.INSTANCE.dustcap > 0)
            {
                return;
            }

            Debug.Log("Drift2");
            var a = Instantiate(dustEffect) as GameObject;
            a.gameObject.SendMessage("Initialize", new parama(true, transform.position));
            GameManager.INSTANCE.dustcap = 1;
        }

        if (Mathf.Abs(body.velocity.y) > _maxVerticalSpeed)
        {
            body.velocity = new Vector2(body.velocity.x, _maxVerticalSpeed * Clamp(body.velocity.y, -1, 1));  //Huge reach
        }

    }

    //Animaciones

    private void Flip() //I don't want to make Marian draw a new batch of redundant sprites, so mirroring it is!
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
    
    public void LoseHP(int damage)
    {
        if (!_coolingHit)
        {
            HP -= damage;
            _animator.SetBool("IsJumping", false);
            _animator.SetTrigger("Hurt");
            _healthBar.UpdateHP();

            _coolingHit = true;
            StartCoroutine(StartCooldown());

            if (HP <= 0)
            {
                Die();
            }
        }
    }

    void Die() //URGENT: this should lead to a game over screen
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

    public virtual void TheTheSkill()
    {
    }

    void Respawn() //also this shouldn't be here, this is main game logic stuff
    {
        if (!_coolingRespawn)
        {
            transform.parent = null;
            transform.localPosition = GameManager.INSTANCE.CHECKPOINT;
            _animator.SetTrigger("idle");
            _animator.SetBool("isDead", false);
            GetComponent<PlayerCombat>().enabled = true;
            GetComponent<Player>().enabled = true;
            HP = MaxHP;
            _healthBar.UpdateHP();
            if (GameManager.INSTANCE.ACTUAL_CHECKPOINT.name == "Checkpoint2" && GameManager.INSTANCE.ELEVATORS_LVL1[1].localPosition.y > 0)
            {
                _callWhat.Invoke();
            }

            _coolingRespawn = true;
            StartCoroutine(StartCooldownRespawn());
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

    public void SetHP(int hp)
    {
        HP = hp;
        _healthBar.UpdateHP();
    }

    public int GetMaxHP()
    {
        return MaxHP;
    }

    public void SetMaxHP(int hp)
    {
        MaxHP = hp;
        //HP = MaxHP;
        //_healthBar.UpdateHP();
    }

    private IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(0.8f);

        _coolingHit = false;
    }

    public IEnumerator StartCooldownIFrame()
    {
        yield return new WaitForSeconds(0.4f);

        _coolingHit = false;
    }

    public IEnumerator StartCooldownDashingAnimation()
    {
        yield return new WaitForSeconds(0.3f);

        _coolingDashAnim = false;
        _animator.SetBool("Dash", false);
    }

    public IEnumerator StopDashAnim()
    {
        yield return new WaitForSeconds(0.12f);

        _coolingDashAnim = false;
        _animator.SetBool("Dash", false);
    }

    public void StartCooldownShoot()
    {
        StartCoroutine(StartCooldownShootDown());
    }

    public IEnumerator StartCooldownShootDown()
    {
        yield return new WaitForSeconds(1f);

        _coolingShootDown = false;
    }

    public IEnumerator StartCooldownRespawn()
    {
        yield return new WaitForSeconds(2f);

        _coolingRespawn = false;
    }
}
