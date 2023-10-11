using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerererer : Player
{
    public bool _canDash = true;
    public GameObject _dashEffect;

    public override void SpecialJump()
   {
        
   }
    public override void TheTheSkill()
    {
        if (_canDash)
        {
            Debug.Log("aaaaaaaaaaaaaaaaaaa");
            setXStunVariables();
            body.AddForce(new Vector2(3 * GetOrientation(), 0), ForceMode2D.Impulse);
            _animator.SetBool("Dash", true);
            _effectAnimator.SetTrigger("Effect");
            if (_spriteRenderer.flipX)
            {
                _dashEffect.transform.localPosition = new Vector2(0.14f, -0.03f);
                _dashEffect.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                _dashEffect.transform.localPosition = new Vector2(-0.14f, -0.03f);
                _dashEffect.GetComponent<SpriteRenderer>().flipX = true;
            }
            _fallingTime = 0f;
            _animator.SetBool("IsJumping", false);
            _coolingHit = true;
            StartCoroutine(StartCooldownIFrame());
            _coolingDashAnim = true;
            StartCoroutine(StartCooldownDashingAnimation());
        }
        
    }



}
