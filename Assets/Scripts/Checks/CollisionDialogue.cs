using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionDialogue : MonoBehaviour
{
    
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject _panelDialogue;
    [SerializeField]
    private string _id;
    [SerializeField]
    private GameObject _interactable;
    [SerializeField]
    private KeyCode _startDialogue;
    [SerializeField]
    private bool _activeDialogue;
   
    private bool _ePressed = false;
    private bool _eAvailable = false;
    private bool _originalFlip;
    private bool _actualFlip;

    void Start()
    {
        _originalFlip = gameObject.GetComponent<SpriteRenderer>().flipX;
        _actualFlip = _originalFlip;
    }

    // void OnTriggerEnter2D(Collider2D col)
    // {
    //     gameObject.GetComponent<SpriteRenderer>().flipX = !_player.GetComponent<SpriteRenderer>().flipX;
    // }

    void OnTriggerStay2D(Collider2D col)
    {
        if (_panelDialogue.activeSelf)
        {
            return;
        }

        if (_player.tag == col.tag)
        {
            // if (_player.transform.localPosition.x > transform.localPosition.x && !_actualFlip)
            // {
            //     _actualFlip = !_actualFlip;
            //     gameObject.GetComponent<SpriteRenderer>().flipX = !_actualFlip;
            // }
            // else if (_player.transform.localPosition.x <= transform.localPosition.x && _actualFlip)
            // {
            //     _actualFlip = !_actualFlip;
            //     gameObject.GetComponent<SpriteRenderer>().flipX = !_actualFlip;
            // }
            // if (!_activeDialogue)
            // {
            //     return;
            // }
            //_eAvailable = true;
            _interactable.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        //_eAvailable = false;
        _interactable.SetActive(false);
        //gameObject.GetComponent<SpriteRenderer>().flipX = _originalFlip;
    }

    public void StartDialogue()
    {   
        _interactable.SetActive(false);

        _player.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
        _player.GetComponent<Player>().enabled = false;
        _player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        _player.GetComponent<Animator>().SetFloat("Speed", 0f);

        _panelDialogue.SetActive(true);
        _panelDialogue.GetComponent<ButtonDialogue>().ZONENAME = _id;
        _panelDialogue.GetComponent<ButtonDialogue>().FirstDialogue();

    }

    void Update()
    {
        // if (!_eAvailable || !_activeDialogue)
        // {
        //     return;
        // }

        // if (Input.GetKeyDown(_startDialogue) && (!_ePressed))
        // {
        //     _ePressed = true;
        //     StartDialogue();
        // }

        // if (Input.GetKeyUp(_startDialogue))
        // {
        //     _ePressed = false;
        // }

        if ((Input.GetAxis("Submit") > 0))
        {
            _eAvailable = false;
        }
        else
        {
            _eAvailable = true;
        }
    }

}
