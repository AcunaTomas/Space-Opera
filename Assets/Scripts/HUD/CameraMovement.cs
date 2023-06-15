using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Transform _textMainTitle;
    [SerializeField]
    private Transform _textPressSpace;
    [SerializeField]
    private Transform _allButtons;

    private bool _goDown = false;
    private bool _spacebarPressed = false;

    void Start()
    {
        
    }

    IEnumerator Jump()
    {
        yield return new WaitForSeconds(5f);
        _allButtons.gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !_spacebarPressed)
        {
            _spacebarPressed = true;

            if (!_goDown)
            {
                _animator.SetBool("Camera", true);
                _textMainTitle.gameObject.SetActive(false);
                _textPressSpace.gameObject.SetActive(false);
                StartCoroutine(Jump());
            }
            else
            {
                _animator.CrossFade(_animator.GetCurrentAnimatorStateInfo(0).fullPathHash, 0, 0, 1f);
                _allButtons.gameObject.SetActive(true);
            }

            _goDown = !_goDown;
        }

        if (Input.GetButtonUp("Jump"))
        {
            _spacebarPressed = false;
        }
    }
}
