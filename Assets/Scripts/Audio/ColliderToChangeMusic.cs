using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderToChangeMusic : MonoBehaviour
{
    private AdaptativeMusic _adaptativeMusic;

    [Header("0:CALM 1:ATTACK 2:HEAVY MODE")]

    [Range(0, 2)]
    public int parameterValue;

    private bool _doOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        _adaptativeMusic = FindAnyObjectByType<AdaptativeMusic>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && !_doOnce)
        {
            _adaptativeMusic.UpdateParameter("Music", parameterValue);
            _doOnce = true;
        }
    }
}
