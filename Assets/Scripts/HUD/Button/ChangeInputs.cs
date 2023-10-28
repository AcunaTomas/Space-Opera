using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeInputs : MonoBehaviour
{

    [SerializeField]
    private GameObject[] _keyboard;
    [SerializeField]
    private GameObject[] _controller;
    private int _previousControllerCount = 0;
    private int _connectedControllerCount = 0;
    public bool _control = false; 

    void Start()
    {
        foreach (string name in Input.GetJoystickNames())
        {
            if (!string.IsNullOrEmpty(name))
            {
                _connectedControllerCount++;
            }
        }

        _previousControllerCount = _connectedControllerCount;
        for (int i = 0; i < _keyboard.Length; i++)
        {
            _keyboard[i].SetActive(_connectedControllerCount == 0);
            _controller[i].SetActive(_connectedControllerCount > 0);
        }
    }

    void Update()
    {
        _connectedControllerCount = 0;
        foreach (string name in Input.GetJoystickNames())
        {
            if (!string.IsNullOrEmpty(name))
            {
                _connectedControllerCount++;
                ControlTrue();
            }
        }

        if (_connectedControllerCount != _previousControllerCount)
        {
            _previousControllerCount = _connectedControllerCount;
            for (int i = 0; i < _keyboard.Length; i++)
            {
                _keyboard[i].SetActive(_connectedControllerCount == 0);
                _controller[i].SetActive(_connectedControllerCount > 0);
                ControlFalse();
            }
        }
    }

    void ControlTrue()
    {
        _control = true;
    }    

    void ControlFalse()
    {
        _control = false;
    }
}
