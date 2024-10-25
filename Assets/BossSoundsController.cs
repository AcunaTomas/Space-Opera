using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class BossSoundsController : MonoBehaviour
{
    [field: SerializeField] public EventReference Electricity { get; private set; }
    [field: SerializeField] public EventReference Hacking { get; private set; }
    [field: SerializeField] public EventReference Recharge { get; private set; }

    public void PlayElectricitySound()
    {
        AudioController.instance.PlayOneShot(Electricity, this.transform.position);
    }

    public void PlayHackingSound()
    {
        AudioController.instance.PlayOneShot(Hacking, this.transform.position);
    }

    public void PlayRechargeSound()
    {
        AudioController.instance.PlayOneShot(Recharge, this.transform.position);
    }
}
