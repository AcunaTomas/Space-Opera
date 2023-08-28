using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeInputs : MonoBehaviour
{

    private GameObject _keyboard;
    private GameObject _controller;
    private int _previousControllerCount = 0;
    private int _connectedControllerCount = 0;

    void Start()
    {
        _keyboard = transform.GetChild(1).gameObject;
        _controller = transform.GetChild(2).gameObject;
    }

    void Update()
    {
        _connectedControllerCount = 0;
        foreach (string name in Input.GetJoystickNames())
        {
            if (!string.IsNullOrEmpty(name))
            {
                _connectedControllerCount++;
            }
        }

        if (_connectedControllerCount != _previousControllerCount)
        {
            _previousControllerCount = _connectedControllerCount;
            _keyboard.SetActive(_connectedControllerCount == 0);
            _controller.SetActive(_connectedControllerCount > 0);
        }
    }
}
