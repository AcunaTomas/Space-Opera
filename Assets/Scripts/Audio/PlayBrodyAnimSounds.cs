using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBrodyAnimSounds : MonoBehaviour
{
    public void PlayFootstepsSound()
    {
        AudioController.instance.PlayOneShot(FMODEvents.instance.FootstepsBrody, this.transform.position);
    }

    public void PlayLaserAttackSound()
    {
        AudioController.instance.PlayOneShot(FMODEvents.instance.ShootBrody, this.transform.position);
    }

    public void PlayJumpSound()
    {
        AudioController.instance.PlayOneShot(FMODEvents.instance.JumpSound, this.transform.position);
    }
}
