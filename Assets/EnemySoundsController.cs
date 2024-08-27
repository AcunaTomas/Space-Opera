using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class EnemySoundsController : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter _soundEmitter;

    void Start()
    {
        _soundEmitter = GetComponent<StudioEventEmitter>();
    }

    public void UpdateParameter(string parameterName, int value)
    {
        if (_soundEmitter != null && _soundEmitter.EventInstance.isValid())
        {
            // cambia el valor del parametro usando el nombre del mismo
            _soundEmitter.EventInstance.setParameterByName(parameterName, value);
        }
        else
        {
            Debug.LogWarning("falta referencia o el parámetro es incorrecto.");
        }
    }
}
