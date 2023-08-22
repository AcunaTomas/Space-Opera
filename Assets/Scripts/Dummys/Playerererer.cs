using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerererer : Player
{

   public override void SpecialJump()
   {
        Debug.Log("aaaaaaaaaaaaaaaaaaa");
        setXStunVariables();
        body.AddForce(new Vector2(3 * Mathf.Clamp(Input.GetAxis("Horizontal"), -1, 1), 0), ForceMode2D.Impulse);
    }
    public override void TheTheSkill()
    {
        Debug.Log("aaaaaaaaaaaaaaaaaaa");
        setXStunVariables();
        body.AddForce(new Vector2(3 * Mathf.Clamp(Input.GetAxis("Horizontal"), -1, 1), 0), ForceMode2D.Impulse);

    }

}
