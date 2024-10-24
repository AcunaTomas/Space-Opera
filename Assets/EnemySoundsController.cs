using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class EnemySoundsController : MonoBehaviour
{
    public StudioEventEmitter _soundEmitter;
    private Enemy _enemy;

    public void UpdateParameter(string parameterName, int value)
    {
        if (_soundEmitter != null && _soundEmitter.EventInstance.isValid())
        {
            _soundEmitter.EventInstance.setParameterByName(parameterName, value);
        }
        else
        {
            Debug.LogWarning("falta referencia o el parámetro es incorrecto.");
        }
    }
}
