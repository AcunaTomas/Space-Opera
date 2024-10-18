using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBitoAnimationSounds : MonoBehaviour
{
    public void PlayFootstepsSound()
    {
        AudioController.instance.PlayOneShot(FMODEvents.instance.FootstepsBrody, this.transform.position);
    }

    public void PlayJumpSound()
    {
        AudioController.instance.PlayOneShot(FMODEvents.instance.JumpSound, this.transform.position);
    }

    public void GetDamageSound()
    {
        AudioController.instance.PlayOneShot(FMODEvents.instance.GetDamageBito, this.transform.position);
    }

    public void PlayLandingSound()
    {
        AudioController.instance.PlayOneShot(FMODEvents.instance.FallSound, this.transform.position);
    }

    public void PlaySavingGame()
    {
        AudioController.instance.PlayOneShot(FMODEvents.instance.SaveLevel, this.transform.position);
    }
}
