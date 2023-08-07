using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Transform _textPressSpace;
    [SerializeField]
    private Transform _allButtons;

    private bool _goDown = false;
    private bool _spacebarPressed = false;
    private bool _stopDoingThis = false;

    void Start()
    {
        AudioManager.INSTANCE.PlayMusic();
    }
    
    IEnumerator Jump()
    {
        yield return new WaitForSeconds(6f);
        if (!_stopDoingThis)
        {
            _allButtons.gameObject.SetActive(true);
            _stopDoingThis = true;
        }
    }

    void Update()
    {
        if (_stopDoingThis)
        {
            return;
        }

        if (Input.GetButtonDown("Jump") && !_spacebarPressed)
        {
            _spacebarPressed = true;

            if (!_goDown)
            {
                _animator.SetBool("Camera", true);
                _textPressSpace.gameObject.SetActive(false);
                StartCoroutine(Jump());
            }
            else
            {
                _animator.CrossFade(_animator.GetCurrentAnimatorStateInfo(0).fullPathHash, 0, 0, 1f);
                _allButtons.gameObject.SetActive(true);
                _stopDoingThis = true;
            }

            _goDown = !_goDown;
        }

        if (Input.GetButtonUp("Jump"))
        {
            _spacebarPressed = false;
        }
    }
}
