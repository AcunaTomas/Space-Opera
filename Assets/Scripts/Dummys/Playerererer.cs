using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerererer : Player
{
    public bool _isDashing;
    private float _dashForce = 4f;
    
    private float _dashCooldown = 0.5f;
    public bool _failDash = false;
    public GameObject _dashEffect;

    private float _dashTimer = 0f;
    private float _dashTime = 0.2f;


    float dashRate = 2f;
    float nextDashTime = 0f;

    bool _touchedFloor = false;

    private bool _dashSounded = false;


    public IEnumerator Dash1()
    {
        _canDash = false;
        canIMove = false;
        _isDashing = true;

        _animator.SetBool("Dash", true);
        
        _fallingTime = 0f;
        _animator.SetBool("IsJumping", false);
        //_coolingHit = true;
        //StartCoroutine(StartCooldownIFrame());

        yield return new WaitForSeconds(_dashTime);
        _isDashing = false;
        canIMove = true;
        //yield return new WaitForSeconds(_dashCooldown);
        //_canDash = true;
        
    }

    public override void Update()
    {
        if (canIMove)
        {
            _plaseJump();
        }


        if (!_canDash)
        {
            if (grounded)_touchedFloor = true;
            
            nextDashTime += Time.deltaTime;

            if (_touchedFloor && nextDashTime >= _dashCooldown + _dashTime)
            {
                _touchedFloor = false;
                _canDash = true;
                _dashTimer = 0f;
                canIMove = true;
                _isDashing = false;
                _animator.SetBool("Dash", false);
                _coolingDashAnim = false;
                nextDashTime = 0;
                body.gravityScale = 1; //hard code for the win
            }
        }

        Dash();
    }

    public override void SpecialJump()
   {
   }
    public override void TheTheSkill()
    {
        if (_canDash)
        {
            _isDashing = true;
            _dashTimer += _dashTime;
            _canDash = false;
        }
        
    }

    private void Dash()
    {
        if (_isDashing)
        {   
            if(!_dashSounded)
            {
                AudioController.instance.PlayOneShot(FMODEvents.instance.DashSound, this.transform.position);
                _dashSounded = true;
            }


            _animator.SetBool("Dash", true);
            _fallingTime = 0f;
            _animator.SetBool("IsJumping", false);
            _coolingDashAnim = true;
            //_effectAnimator.SetTrigger("Effect");
            StartCoroutine(StartCooldownDashingAnimation());

            if (_dashTimer > 0)
            {
                canIMove = false;
                body.gravityScale = 0f;
                if (_spriteRenderer.flipX)
                {
                    body.velocity = new Vector2(-1 * _dashForce, 0);
                }
                else
                {
                    body.velocity = new Vector2(1 * _dashForce, 0);
                }

                _dashTimer -= Time.deltaTime;
            }

            if (_dashTimer < 0)
            {
                _isDashing = false;
                canIMove = true;
                body.gravityScale = 1;
                _dashSounded = false;
            }
        }
    }

// ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⠋⢉⣩⣭⣭⣭⣭⣭⣍⠉⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
// ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠿⢛⣋⣁⡄⣠⣤⣶⣶⣾⣿⣿⣿⣷⣤⣌⣉⠙⠻⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
// ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠟⣩⣶⣾⣿⣿⢏⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣶⣦⢉⠻⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
// ⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⣡⣼⣿⣿⣿⣿⡇⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⡄⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
// ⣿⣿⣿⣿⣿⣿⣿⣿⠏⣴⣿⣿⣿⣿⣿⣿⣷⣆⡻⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡀⠘⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
// ⣿⣿⣿⣿⣿⣿⣿⡟⣰⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⣍⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⠀⠈⢿⠿⢿⣿⣿⣿⣿⣿⣿⣿
// ⣿⣿⣿⣿⣿⣿⣿⢀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⢁⠘⠃⢹⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠂⠀⢀⣤⣀⠙⢿⣿⣿⣿⣿⣿
// ⣿⣿⣿⣿⣿⣿⠇⣼⣿⣿⣿⣿⡿⠟⣋⣩⣥⠒⠚⠛⠛⠁⠀⠀⠀⠀⣉⣉⠀⠈⠙⠻⠿⣿⠿⠛⢙⡛⠂⢰⣿⣿⣿⣷⣤⡙⠻⢿⣿⣿
// ⣿⣿⣿⣿⠿⢃⣴⣿⣿⣿⠿⠛⢛⣉⣩⠤⠒⠀⠀⠀⠀⠀⠤⠤⠤⢤⡀⠙⢿⠿⢦⣄⣤⣠⡀⠈⠛⢿⡄⠀⢻⡿⣿⣿⣿⣿⣷⡄⠿⣿
// ⣿⣿⣿⣇⠐⠛⢛⣉⣥⣴⣶⠿⠛⠉⠀⠀⠀⠀⠀⠀⠀⢰⣿⡆⢆⠀⠉⢦⠀⠄⣾⣷⣿⠋⠀⠀⠀⠀⠀⠀⠀⠙⠎⢻⣿⡿⠟⣠⣶⣤
// ⣿⣿⣿⣿⣷⡄⢿⠿⠛⠉⢠⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢿⣿⡘⡆⣦⠈⡇⠀⠈⠉⠁⠀⠀⠀⠀⠐⠀⠀⣄⠀⠀⠀⠠⠴⣿⢿⣿⣿
// ⣿⣿⣿⣿⣿⣇⡀⠀⡎⣠⠈⡇⣶⣦⣤⣀⠀⠀⠀⠀⠠⣤⠘⣿⣷⣹⡜⠀⠀⠀⠀⠀⠀⠀⠀⠀⡄⡀⠀⠀⠀⢀⣤⣴⠆⢀⣠⣤⣶⣿       We Dashing
// ⣿⣿⣿⣿⣿⣿⣷⠀⢈⠻⡄⠛⢿⣿⣿⣿⡦⠀⠀⠀⠀⠉⠃⠛⠛⡋⠀⠂⢀⣾⣧⠀⠀⠀⠀⢸⣇⠃⠀⠀⣰⣿⣿⠏⣰⣿⣿⣿⣿⣿
// ⣿⣿⣿⣿⣿⣿⣿⣷⡀⠲⠡⠀⢸⣿⣿⠏⣴⣷⣤⣀⢠⣀⣀⣉⣀⠠⣐⣾⣿⣿⣿⠀⠀⠀⠀⢸⣿⠀⢀⣄⠙⠿⠟⠀⠿⠿⢟⣛⣿⣿
// ⣿⣿⣿⣿⣿⣿⡿⠛⠁⢠⣶⠀⣿⣿⣿⢸⣿⣿⣿⣿⣶⣶⡿⠿⢻⣀⠈⠛⢹⣿⣿⠆⠀⠀⠀⣈⠁⢀⣾⣿⣿⣿⣿⡇⣠⣾⣿⣿⣿⣿
// ⣿⣿⣿⣿⣿⠏⢠⡾⠀⣾⣿⡀⢹⣿⣿⢸⣿⣿⣿⣿⡟⠀⠐⢿⣿⣿⣿⣶⣦⣭⣥⣀⣿⣶⣶⣴⠀⣼⣿⣿⣿⣿⣿⣇⠹⣿⣿⣿⣿⣿
// ⣿⣿⣿⡿⠋⣰⣿⠁⠀⣿⣿⣷⠀⢻⣿⡆⣿⣿⣿⣟⣿⣧⠀⠀⣈⠛⠿⣿⣿⣿⣿⣿⣿⣿⡿⠃⣠⢟⣿⣿⣿⣿⣿⣿⡄⠈⣹⣿⣿⣿
// ⣁⣤⡄⢁⣸⣿⠃⠀⢰⣿⣿⣿⣇⠈⢿⣧⢸⣿⣿⣿⣿⣿⣧⠀⣌⡻⣶⡄⠉⠛⢛⣿⣿⠟⢀⣼⣷⣿⣿⣿⣿⣿⣿⣿⣿⠘⣿⣿⣿⣿
// ⣿⡏⢀⣾⠿⣿⠀⠀⢸⣿⣿⣿⣿⣷⡀⠛⠈⣿⣿⣿⣿⣿⣿⣷⣌⣙⣛⣁⣤⣾⣿⣿⠏⢠⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⡈⠛⠿⠿
// ⣿⠀⢸⣷⣾⣿⡆⠀⡀⣿⣿⣿⣿⢿⣿⣆⠀⠸⣿⣿⣿⣿⣿⣿⣿⠋⣉⣥⣿⣿⣿⠉⢠⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣶⣶
// ⡟⠀⣿⣿⡿⠿⠇⠀⣧⢸⣿⣿⣿⣦⡙⢿⣷⣦⣈⡛⠿⠿⠿⣿⣿⣿⣿⣿⡿⠟⠁⣀⣈⠛⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
// ⠀⣸⡟⠉⣀⣤⡄⠀⢿⣦⣟⢻⣿⣿⣿⣦⣙⠻⣿⣿⣷⣶⣶⣦⣤⡄⠀⠀⣠⣶⣿⣿⢋⡄⠀⣁⡀⠙⠿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿
// ⠀⠋⢠⣾⣿⣿⣿⡀⠸⣿⣿⡌⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠛⢁⣴⣿⣿⣿⡿⢡⣾⠁⣸⣿⣿⠆⠀⣠⣤⣈⠙⠿⣿⣿⣿⣿⣿⣿
// ⠀⣠⣿⣿⣿⣿⣿⣇⠀⠘⢿⣿⣸⣿⣿⣿⣿⣿⣿⡿⠟⠋⠀⣴⣞⣿⣿⣿⡿⢉⣴⣿⠁⢀⣿⣿⣿⠀⠀⣿⣿⣿⣿⡆⢈⡛⠻⣿⣿⣿

    

}
