using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class CattoEnemySounds : MonoBehaviour
{

    [field: SerializeField] public EventReference AlertSound { get; private set; }
    private EventInstance alertInstance;
    [field: SerializeField] public EventReference AttackSound { get; private set; }
    [field: SerializeField] public EventReference RunSound { get; private set; }
    

    public void PlayAlertSound()
    {
        // Crear una instancia del evento
        alertInstance = FMODUnity.RuntimeManager.CreateInstance(AlertSound);

        // Reproducir el evento
        alertInstance.start();
        
        // Liberar recursos cuando termine de reproducirse
        alertInstance.release();
    }
}
