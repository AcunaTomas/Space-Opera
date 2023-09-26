using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerererer : Player
{
    public bool _canDash = true;

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
            _animator.SetTrigger("Dash");
            _fallingTime = 0f;
            _animator.SetBool("IsJumping", false);
            _coolingHit = true;
            StartCoroutine(StartCooldownIFrame());
            _coolingDashAnim = true;
            StartCoroutine(StartCooldownDashingAnimation());
        }
        
    }



}
