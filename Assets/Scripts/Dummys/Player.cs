using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private Object dustEffect;
    [SerializeField]
    float HorizontalInputValue;
    [SerializeField]
    int HP = 5;
    [SerializeField]
    int MaxHP = 5;
    [SerializeField]
    float vertspid = -0.3f;
    public bool canIjump = true;
    bool wallijumpy = false;
    public Rigidbody2D body;
    [SerializeField]
    private float jumpLimit = 0f;
    [SerializeField]
    public float Xspeed = 0f;
    private Vector2 speedCaps = new Vector2(1.2f, 4f); //x: usado para el movimiento horizontal y valor temporal para el "arrastre" cuando se cae de una pared.
                                                     //y: usado para el movimiento vertical.
    private Vector2 speedCapsStatic = new Vector2(1.2f, 4f);
    [SerializeField]
    private float _maxVerticalSpeed = 5f; //El limite de velocidad en y actual
    [SerializeField]
    private int extrajumpcount = 1;
    [SerializeField]
    private float _lastJumpPress = 0f;
    [SerializeField]
    private bool canIMove = true;
    private float _autoPilotOffset = 0.2f;
    public float _fallingTime = 0f;

    [SerializeField]
    private float WallJumpXDirection = 0f;

    [SerializeField]
    private float _wallJumpFreezeTimer = 0.5f;

    private float _wallJumpXtimeFreeze = 0.3f;

    private float _coyoteValidTime;

    private float _xSpeedNullifier = 1;

    private bool doIhaveToGoSomewhere = false;

    [SerializeField]
    private float destinationX, destinationY;

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

    [SerializeField]
    private bool _skillPermitted = true;
    private float _skillHold;


    public float dashRate = 2f;
    float nextDashTime = 0f;

    public bool _coolingHit = false;
    public bool _coolingDashAnim = false;
    public bool _coolingShootDown = false;
    public bool _coolingRespawn = false;

    public Animator _effectAnimator;

    [SerializeField]
    private GameObject _doubleJumpEffect;
    [SerializeField]
    private GameObject _landingEffect1;
    [SerializeField]
    private GameObject _landingEffect2;


    private float dustSpawnCooldown = 0f;
    [SerializeField]
    private RuntimeAnimatorController _brodyRight;
    [SerializeField]
    private RuntimeAnimatorController _brodyLeft;

    private float _cameraTimer = 0f;

    [SerializeField]
    private GameObject _cameraUp;
    [SerializeField]
    private GameObject _cameraDown;

    //Esta c�mara hay que sacarla de ac�, es solo para que ande, imagino que despu�s podemos designar la c�mara activa en el game manager y llamar esa
    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera _activeCamera;

    private float _flickerDuration = 0.4f;
    private int _flickerCount = 3;

    public PlayerType _playerType;
    public enum PlayerType
    {
        Bito,
        Brody
    }

    public bool _isBrodyJumping = false;

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
        return 1;
    }

    void Start()
    {
        switch (_playerType)
        {
            default:

                break;
        }

        body = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        dustEffect = Resources.Load("Prefabs/dustEffect");

    }

    void FixedUpdate()
    {
        
        if (canIMove == true)
        {
            HorizontalInputValue = Input.GetAxis("Horizontal");
            _plaseJump();
            Movement();
            Effects();
            Flip();
            CameraUpDown();
        }
        if (doIhaveToGoSomewhere == true)
        {
            MoveToPoint();
        }
        WallJumpDelay();
        speedCapping();
        UpdateTimers();
    }

    private void _plaseJump()
    {
        
        if (canIjump && Input.GetButton("Jump"))
        {
            
           if (_lastJumpPress <= 0.30f)
           {
                vertspid = 1.3f + jumpLimit;
                _animator.SetBool("IsJumping", true);
                _animator.SetFloat("Speed", 1f);
           }
           else
           {   
               vertspid = jumpLimit;
           }
           
           jumpLimit += 0.04f;
           
           if (_lastJumpPress > 4f)
           {
                jumpLimit = 0f;
           }
           
        }
        if (jumpLimit >= 0.4f || (Input.GetButton("Jump") == false && _lastJumpPress > 0.12f)) // TO-DO: Encontrar una forma de reformular este or
        {
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

    }

    public virtual void SpecialJump() //Defaults to double jump, Override it if you want to change its behaviour
    {
        body.velocity = new Vector2(body.velocity.x, 0);
        body.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
        extrajumpcount -= 1;
        _animator.SetBool("IsJumping", false);
        _animator.SetTrigger("DoubleJump");

        //acá hay que hacer el instantiate de Brody double jump
        //_effectAnimator.SetTrigger("Effect");

        var _jumpEffect = Instantiate(_doubleJumpEffect, this.transform);
        _jumpEffect.GetComponent<Animator>().SetTrigger("Effect");

        Destroy(_jumpEffect, 0.6f);

    }

    private void WallJump() //Wall jumping handler
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

    private void unsetXStunVariables() //like the last one, but for unfreezing
    {
        speedCaps = new Vector2(speedCapsStatic.x, speedCaps.y);
    }

    void WallJumpDelay() // Sticks The player to the wall
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

    private float Clamp(float x, float y, float z) // Should be replaced with Mathf.Clamp
    {
        if (x.CompareTo(y) < 0) return y;
        else if (x.CompareTo(z) > 0) return z;
        else return x;
    }

    public int GetHP() //Possibly the cleanest piece of code written in here
    {
        return HP;
    }

    void OnCollisionStay2D(Collision2D collision) //Handles collisions, both vertical and horizontal, and changes the "state" (Read: I have not explicitly defined them because I'm an idiot and it would have been easier that way but didn't do that due to a fear of mixed state bugs that happened anyway so why did I not define states in the first place fuck) accordingly
    {
        Debug.DrawRay(new Vector2(0,0), collision.GetContact(0).normal * -1, Color.red);
        Debug.DrawRay(transform.position, transform.position.normalized, Color.green);
        //Debug.Log(collision.GetContact(0).normal);
        ContactPoint2D[] contacts= new ContactPoint2D[8]; //Realistically, the maximum should be around 6, but I want to be careful
        collision.GetContacts(contacts);

        //Debug.Log(normalcoll.y);
        //Debug.Log(normalcoll.x);

        for (var i = 0; i < collision.contactCount; i++) // does this for every single contact point registered
        {
            if (Mathf.Abs(contacts[i].normal.y) > Mathf.Abs(contacts[i].normal.x) && contacts[i].normal.y > 0) //Contacto Vertical
            {
                ChangeSkillStatus(true);
                if (contacts[i].point.y < transform.position.y)
                {
                    canIjump = true;
                    wallijumpy = false;
                    extrajumpcount = 2;
                    _animator.SetBool("IsJumping", false);
                    _animator.SetBool("wall", false);
                }

                
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
            if (Mathf.Abs(contacts[i].normal.y) < Mathf.Abs(contacts[i].normal.x) && extrajumpcount > 0 && collision.gameObject.CompareTag("NotClimbable") == false && !collision.gameObject.CompareTag("Movible")) //Contacto Horizontal/Pared
            {
                if (canIjump == false) 
                {
                    wallijumpy = true;

                    if ((contacts[i].normal.x) > 0) 
                    {
                        _spriteRenderer.flipX = true;
                        if (_playerType == PlayerType.Brody)
                        {
                            _animator.runtimeAnimatorController = _brodyLeft;
                        }
                    } 
                    else
                    {
                        _spriteRenderer.flipX = false;
                        if (_playerType == PlayerType.Brody)
                        {
                            _animator.runtimeAnimatorController = _brodyRight;
                        }
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
        if (_fallingTime > 6.1)
        {
            _animator.SetTrigger("land");

            var _landing1 = Instantiate(_landingEffect1, this.transform);
            var _landing2 = Instantiate(_landingEffect2, this.transform);
            _landing1.GetComponent<Animator>().SetTrigger("Effect");
            _landing2.GetComponent<Animator>().SetTrigger("Effect");

            Destroy(_landing1, 0.7f);
            Destroy(_landing2, 0.7f);
        }
        
        if (collision.gameObject.CompareTag("Ascensor"))
        {
            transform.parent = collision.gameObject.transform;
        }
        if (collision.gameObject.transform.position.y <= transform.position.y)
        {
            extrajumpcount = 1;
        }


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
            speedCaps = new Vector2(speedCaps.x, speedCaps.y);
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

       

        if (Mathf.Abs(body.velocity.y) > _maxVerticalSpeed)
        {
            body.velocity = new Vector2(body.velocity.x, _maxVerticalSpeed * Clamp(body.velocity.y, -1, 1));  //Huge reach
        }

    }

    private void CameraUpDown()
    {
        if (Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") == 0)
        {
            _cameraTimer += Time.deltaTime;
        }
        else if (Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") == 0)
        {
            _cameraTimer += Time.deltaTime;
        }
        else if (Input.GetAxis("Horizontal") == 0)
        {
            _cameraTimer = 0;
        }
        else
        {
            _cameraTimer = 0;
        }

        //Camara
        if (Input.GetAxis("Vertical") > 0 && _cameraTimer >= 1.5f)
        {
            _activeCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = _cameraUp.transform;
            _animator.SetBool("LookUp", true);
            _animator.SetBool("LookDown", false);
        }
        else if (Input.GetAxis("Vertical") < 0 && _cameraTimer >= 1.5f)
        {
            _activeCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = _cameraDown.transform;
            _animator.SetBool("LookUp", false);
            _animator.SetBool("LookDown", true);
        }
        else
        {
            _activeCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = this.gameObject.transform;
            _animator.SetBool("LookUp", false);
            _animator.SetBool("LookDown", false);
        }

    }

    //Animaciones

    private void Flip() //I don't want to make Marian draw a new batch of redundant sprites, so mirroring it is! || In fact, Marian DID draw a batch of redundant sprites, so no mirroring for Brody, kinda...
        {
            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                _spriteRenderer.flipX = true;
                if (_playerType == PlayerType.Brody)
                {
                    _animator.runtimeAnimatorController = _brodyLeft;
                }
            }
            else if (Input.GetAxisRaw("Horizontal") > 0)
            {
                _spriteRenderer.flipX = false;
                if (_playerType == PlayerType.Brody)
                {
                    _animator.runtimeAnimatorController = _brodyRight;
                }
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
            CameraController.Instance.ScreenShake(.5f, .1f);

            _coolingHit = true;
            StartCoroutine(StartCooldown());

            _animator.SetBool("HurtBool", true);
            StartCoroutine(StartCooldownHurtAnim());


            if (HP <= 0)
            {
                Die();
            }
            else
            {
                StartCoroutine(FlickerSprite());
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

    private IEnumerator FlickerSprite()
    {
        Debug.Log("FLIKER");
        for (int i = 0; i < _flickerCount; i++)
        {
            for (float t = 1f; t >= 0.5; t -= Time.deltaTime / _flickerDuration)
            {
                Color newColor = _spriteRenderer.color;
                newColor.a = t;
                _spriteRenderer.color = newColor;
                yield return null;
            }
            
            for (float t = 0.5f; t <= 1f; t += Time.deltaTime / _flickerDuration)
            {
                Color newColor = _spriteRenderer.color;
                newColor.a = t;
                _spriteRenderer.color = newColor;
                yield return null;
            }
            
            yield return new WaitForSeconds(_flickerDuration);
        }
    }

    private IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(2f);

        _coolingHit = false;
        Debug.Log("PUEDE RECIBIR DAÑO");
    }

    private IEnumerator StartCooldownHurtAnim()
    {
        yield return new WaitForSeconds(0.2f);

        _animator.SetBool("HurtBool", false);
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

    private void Effects()
    {
         if (Input.GetAxis("Horizontal") > 0 && _spriteRenderer.flipX == true && canIjump) //Spawns dust
        {

            if (GameManager.INSTANCE.dustcap > 0 || body.velocity.x == 0)
            {
                return;
            }
                
            //Debug.Log("Drift");     
            var a = Instantiate(dustEffect) as GameObject;
            a.gameObject.SendMessage("Initialize", new parama(true, transform.position));
            GameManager.INSTANCE.dustcap = 1;
            
        }
        if (Input.GetAxis("Horizontal") < 0 && _spriteRenderer.flipX == false && canIjump)
        {

            if (GameManager.INSTANCE.dustcap > 0 || body.velocity.x == 0)
            {
                return;
            }

            //Debug.Log("Drift2");
            var a = Instantiate(dustEffect) as GameObject;
            a.gameObject.SendMessage("Initialize", new parama(true, transform.position));
            GameManager.INSTANCE.dustcap = 1;
        }
    }

    public void MovementEnableToggle(bool a)
    {
        canIMove = a;
        if (a == false)
        {
           _animator.SetFloat("Speed", 0);
            Xspeed = 0;
            _xSpeedNullifier = 0;
            body.velocity = new Vector2(0,0);
            return;
        }

    }

    public void MoveToPoint() //Please push them where they need to go please please please, It does :D
    {
        if (body.position.x < (destinationX - _autoPilotOffset))
        {
            body.AddForce(new Vector2(speedCaps.x, 0), ForceMode2D.Force);
            _animator.SetFloat("Speed", speedCaps.x);
            _spriteRenderer.flipX = false;
            return;
        }
        if (body.position.x > (destinationX + _autoPilotOffset))
        {
            body.AddForce(new Vector2(speedCaps.x * -1, 0), ForceMode2D.Force);
            _animator.SetFloat("Speed", speedCaps.x);
            _spriteRenderer.flipX = true;
            return;
        }
        body.velocity = new Vector2(0,0);
        _animator.SetFloat("Speed", 0);
        doIhaveToGoSomewhere = false;


    }
    public void setDestination(float x, float y)
    {
        destinationX = x;
        destinationY = y;
        doIhaveToGoSomewhere = true;
        
    }
    private void speedCapping()
    {
        if (Mathf.Abs(body.velocity.x) > speedCaps.x)
        {
            body.velocity = new Vector2(speedCaps.x * Clamp(body.velocity.x,-1,1), body.velocity.y);
        }
        if(doIhaveToGoSomewhere == true)
        {
            return;
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
    
    }

    public void SetSpeed(float _speedLimit, int axis = 0)
    {
        if(axis == 0)
        {
            speedCaps.x = _speedLimit;
            return;
        }
        speedCaps.y = _speedLimit;
        return;
    }

    public void ResetSpeed()
    {
        speedCaps = speedCapsStatic;
    }

    private void UpdateTimers()
    {
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
    }
}
