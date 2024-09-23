using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControllerBrody : MonoBehaviour
{
    [Header("Animators")]
    [SerializeField] Animator _upperAnimator;
    [SerializeField] Animator _lowerAnimator;
    [SerializeField] SpriteRenderer _upperBrodySR;
    [SerializeField] SpriteRenderer _lowerBrodySR;
    [Header("Controllers")]
    [SerializeField] private RuntimeAnimatorController _upperBrodyRight;
    [SerializeField] private RuntimeAnimatorController _upperBrodyLeft;
    private AnimatorClipInfo[] clipInfo;
    void Start()
    {
        
    }

    void Update()
    {
        Flip();
    }

    private void Flip()
    {
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            _lowerBrodySR.flipX = true;
            _upperAnimator.runtimeAnimatorController = _upperBrodyLeft;
            clipInfo = _upperAnimator.GetCurrentAnimatorClipInfo(0);
            //if(!clipInfo[0].clip.name.Contains("BrodyRun")) _upperBrodySR.flipX = true;
            //else  _upperBrodySR.flipX = false;
            _upperBrodySR.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0) //Cambiar una vez estén todas las animaciones
        {
            _lowerBrodySR.flipX = false;
            _upperBrodySR.flipX = false;
            _upperAnimator.runtimeAnimatorController =_upperBrodyRight;
        }
    }

    public void SetAnimBool(string _animName, bool _bool)
    {
        _upperAnimator.SetBool(_animName, _bool);
        _lowerAnimator.SetBool(_animName, _bool);
    }

    public void SetAnimTrigger(string _animName)
    {
        _upperAnimator.SetTrigger(_animName);
        //_lowerAnimator.SetTrigger(_animName);
    }

    public void SetAnimFloat(string _animName, float _float)
    {
        _upperAnimator.SetFloat(_animName, _float);
        _lowerAnimator.SetFloat(_animName, _float);
    }

    public void ResetVars()
    {
        SetAnimTrigger("idle");
        SetAnimBool("isDead", false);
    }

    public void Die()
    {
        SetAnimTrigger("Die");
        SetAnimBool("isDead", true);
    }
}
