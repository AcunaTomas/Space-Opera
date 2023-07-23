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
    private bool _checkpoint;
    [SerializeField]
    private bool _interactableOnly;
   
    private bool _ePressed = false;
    private bool _eAvailable = false;
    private bool _originalFlip;
    private bool _actualFlip;

    void Start()
    {
        if (_checkpoint || _interactableOnly)
        {
            return;
        }
        _originalFlip = gameObject.GetComponent<SpriteRenderer>().flipX;
        _actualFlip = _originalFlip;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (_checkpoint)
        {
            StartDialogue();
        }
        if (_interactableOnly || _checkpoint)
        {
            return;
        }
        gameObject.GetComponent<SpriteRenderer>().flipX = !_player.GetComponent<SpriteRenderer>().flipX;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (_panelDialogue.activeSelf || _checkpoint)
        {
            return;
        }

        if (_player.tag == col.tag)
        {
            _eAvailable = true;
            _interactable.SetActive(true);

            if (_interactableOnly)
            {
                return;
            }

            if (_player.transform.localPosition.x > transform.localPosition.x && !_actualFlip)
            {
                _actualFlip = !_actualFlip;
                gameObject.GetComponent<SpriteRenderer>().flipX = !_actualFlip;
            }
            else if (_player.transform.localPosition.x <= transform.localPosition.x && _actualFlip)
            {
                _actualFlip = !_actualFlip;
                gameObject.GetComponent<SpriteRenderer>().flipX = !_actualFlip;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (_checkpoint)
        {
            return;
        }

        _eAvailable = false;
        _interactable.SetActive(false);

        if (_interactableOnly)
        {
            return;
        }

        gameObject.GetComponent<SpriteRenderer>().flipX = _originalFlip;
    }

    public void StartDialogue()
    {
        if (!_checkpoint)
        {
            _eAvailable = false;
            _interactable.SetActive(false);
            if(!_interactableOnly)
            {
                _player.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
            }
        }
        else if (_checkpoint)
        {
            gameObject.SetActive(false);
        }

        _player.GetComponent<Player>().enabled = false;
        _player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        _player.GetComponent<Animator>().SetFloat("Speed", 0f);

        _panelDialogue.SetActive(true);
        _panelDialogue.GetComponent<ButtonDialogue>().ZONENAME = _id;
        _panelDialogue.GetComponent<ButtonDialogue>().FirstDialogue();

    }

    public void ChangeId(string id)
    {
        _id = id;
    }

    void Update()
    {
        if (!_eAvailable || _checkpoint)
        {
            return;
        }

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
