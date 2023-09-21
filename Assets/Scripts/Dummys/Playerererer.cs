using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerererer : Player
{
    

    public override void SpecialJump()
   {
        
   }
    public override void TheTheSkill()
    {
        Debug.Log("aaaaaaaaaaaaaaaaaaa");
        setXStunVariables();
        body.AddForce(new Vector2(3 * GetOrientation(), 0), ForceMode2D.Impulse);
        _animator.SetTrigger("Dash");
    }

}
