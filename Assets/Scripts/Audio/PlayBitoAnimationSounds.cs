using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBitoAnimationSounds : MonoBehaviour
{
    public void PlayFootstepsSound()
    {
        AudioController.instance.PlayOneShot(FMODEvents.instance.FootstepsBrody, this.transform.position);
    }

    public void PlaySwordSound()
    {
        AudioController.instance.PlayOneShot(FMODEvents.instance.ShootBrody, this.transform.position);
    }
}
