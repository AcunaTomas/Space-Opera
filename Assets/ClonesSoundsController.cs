using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ClonesSoundsController : MonoBehaviour
{
   [field: SerializeField] public EventReference GetDamage { get; private set; }
   [field: SerializeField] public EventReference Death { get; private set; }


    public void PlayGetDamageSound()
    {
        AudioController.instance.PlayOneShot(GetDamage, this.transform.position);
    }
    public void PlayDeathSound()
    {
        AudioController.instance.PlayOneShot(Death, this.transform.position);
    }
}
