using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBrodyAnimSounds : MonoBehaviour
{
    public void PlayFootstepsSound()
    {
        AudioController.instance.PlayOneShot(FMODEvents.instance.FootstepsBrody, this.transform.position);
    }
}
