using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FMODUnity;
using FMOD.Studio;

public class BotonesController : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private GameObject _puerta;

    [SerializeField]
    private UnityEvent _callWhat;

    private StudioEventEmitter _soundEmitter;

    public bool _playedSound = false;


    void Start()
    {
        _soundEmitter = GetComponent<StudioEventEmitter>();
    }


    public void changeCallback(UnityEvent a)
    {
        _callWhat = a;
    }
    
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("BalaPlayer"))
        {
            _animator.SetTrigger("ButtonPress");
            if(_puerta)_puerta.GetComponent<Animator>().SetTrigger("OpenDoor");
            _callWhat.Invoke();
            this.enabled = false;
            if (!_playedSound)
            {
                _soundEmitter.Play();
                _playedSound = true;
            }
            
        }
            
    }
}
