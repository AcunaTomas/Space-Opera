using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerererer : Player
{
    public bool _canDash = true;
    public bool _isDashing;
    private float _dashForce = 20f;
    private float _dashTime = 0.3f;
    private float _dashCooldown = 0.3f;
    public bool _failDash = false;
    public GameObject _dashEffect;

    public IEnumerator Dash() 
    //Si usamos este dash hay que resetear los booleanos cuando se hace el switch:
    //_canDash, canImove, _animator.SetBool("Dash", true), _isdashing, _coolingDashAnim, _coolingHit
    
//                              .-----.
//                             /7  .  (
//                            /   .-.  \
//                           /   /   \  \ 
//                          / `  )   (   )
//                         / `   )   ).  \
//                       .'  _.   \_/  . |
//      .--.           .' _.' )`.        |        puto el que lee
//     (    `---...._.'   `---.'_)    ..  \
//      \            `----....___    `. \  |
//       `.           _ ----- _   `._  )/  |
//         `.       /"  \   /"  \`.  `._   |
//           `.    ((O)` ) ((O)` ) `.   `._\
//             `-- '`---'   `---' )  `.    `-.
//                /                  ` \      `-.
//              .'                      `.       `.
//             /                     `  ` `.       `-.
//      .--.   \ ===._____.======. `    `   `. .___.--`     .''''.
//     ' .` `-. `.                )`. `   ` ` \          .' . '  8)
//    (8  .  ` `-.`.               ( .  ` `  .`\      .'  '    ' /
//     \  `. `    `-.               ) ` .   ` ` \  .'   ' .  '  /
//      \ ` `.  ` . \`.    .--.     |  ` ) `   .``/   '  // .  /
//       `.  ``. .   \ \   .-- `.  (  ` /_   ` . / ' .  '/   .'
//         `. ` \  `  \ \  '-.   `-'  .'  `-.  `   .  .'/  .'
//           \ `.`.  ` \ \    ) /`._.`       `.  ` .  .'  /
//            |  `.`. . \ \  (.'               `.   .'  .'
//         __/  .. \ \ ` ) \                     \.' .. \__
//  .-._.-'     '"  ) .-'   `.                   (  '"     `-._.--.
// (_________.-====' / .' /\_)`--..__________..-- `====-. _________)
//                  (.'(.'                                   

    {
        _canDash = false;
        canIMove = false;
        _isDashing = true;
        float originalGravity = body.gravityScale;
        body.gravityScale = 0.1f;
        if (_spriteRenderer.flipX)
        {
            body.velocity = new Vector2(-1 * _dashForce, 0);
        }
        else
        {
            body.velocity = new Vector2(1 * _dashForce, 0);
        }
        _animator.SetBool("Dash", true);
        //_effectAnimator.SetTrigger("Effect");
        _fallingTime = 0f;
        _animator.SetBool("IsJumping", false);
        _coolingHit = true;
        StartCoroutine(StartCooldownIFrame());
        _coolingDashAnim = true;
        StartCoroutine(StartCooldownDashingAnimation());


        yield return new WaitForSeconds(_dashTime);
        body.gravityScale = originalGravity;
        _isDashing = false;
        canIMove = true;
        yield return new WaitForSeconds(_dashCooldown);
        _canDash = true;
    }

    public override void SpecialJump()
   {
        
   }
    public override void TheTheSkill()
    {
        if (_canDash)
        {
            StartCoroutine(Dash());
        }
        

        // if (_canDash)
        // {
        //     setXStunVariables();
        //     body.AddForce(new Vector2(3 * GetOrientation(), 0), ForceMode2D.Impulse);
        //     
        //     if (_spriteRenderer.flipX)
        //     {
        //         _dashEffect.transform.localPosition = new Vector2(0.14f, -0.03f);
        //         _dashEffect.GetComponent<SpriteRenderer>().flipX = false;
        //     }
        //     else
        //     {
        //         _dashEffect.transform.localPosition = new Vector2(-0.14f, -0.03f);
        //         _dashEffect.GetComponent<SpriteRenderer>().flipX = true;
        //     }

        //     
        // }

        // if (_failDash)
        //     {
        //         setXStunVariables();
        //         _animator.SetBool("Dash", true);
        //         _animator.SetBool("IsJumping", false);
        //         _fallingTime = 0f;
        //         _coolingDashAnim = true;
        //         StartCoroutine(StartCooldownDashingAnimation());
        //     }
        
    }



}
