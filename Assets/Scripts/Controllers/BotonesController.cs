using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class BotonesController : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private GameObject _puerta;

    [SerializeField]
    private UnityEvent _callWhat;


    public void changeCallback(UnityEvent a)
    {
        _callWhat = a;
    }
    
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("BalaPlayer"))
        {
            _animator.SetTrigger("ButtonPress");
            _puerta.GetComponent<Animator>().SetTrigger("OpenDoor");
            _callWhat.Invoke();
        }
            
    }
}
