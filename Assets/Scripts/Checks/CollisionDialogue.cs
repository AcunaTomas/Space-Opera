using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionDialogue : MonoBehaviour
{
    
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private ButtonDialogue _panelDialogue;
    [SerializeField]
    private string _id;
    [SerializeField]
    private bool _checkpoint;
    [SerializeField]
    private bool _interactableOnly;
    [SerializeField]
    private bool _panelDialogueDown;

    private bool _eAvailable = false;
    private bool _originalFlip;
    private bool _actualFlip;
    private GameObject _interactable;
    private bool _buttonPressed = false;
    private bool _entered = false;

    public ChangeAudio _changeAudio;
    public enum ChangeAudio
    {
        dialogo,
        especial
    }

    void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        //Physics2D.IgnoreCollision(_player.transform.GetChild(5).GetComponent<CircleCollider2D>(), GetComponent<Collider2D>());
        //Physics2D.IgnoreCollision(_player.transform.GetChild(6).GetComponent<CircleCollider2D>(), GetComponent<Collider2D>());
    }
    void Start()
    {
        _panelDialogue = GameManager.INSTANCE.CANVAS;
        if (_checkpoint || _interactableOnly)
        {
            return;
        }
        _originalFlip = gameObject.GetComponent<SpriteRenderer>().flipX;
        _actualFlip = _originalFlip;
        
    }

    public void GetPlayer()
    {
        _player = GameObject.FindWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (_player == null)
        {
            GetPlayer();
        }
        if(!col.CompareTag("Player"))
        {
            return;
        }

        if (_checkpoint)
        {
            StartDialogue();
            return;
        }

        _interactable = GameManager.INSTANCE.BUTTON_INTERACT;
        _interactable.transform.parent = transform;
        _interactable.transform.localPosition = new Vector3 (0f, 0.2f, 0f);
        
        if (_interactableOnly)
        {
            return;
        }
        gameObject.GetComponent<SpriteRenderer>().flipX = !_player.GetComponent<SpriteRenderer>().flipX;

    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (_panelDialogue.gameObject.activeSelf || _checkpoint)
        {
            return;
        }

        if (_player.tag == col.tag)
        {
            _entered = true;
            _eAvailable = true;
            _buttonPressed = false;
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
        if (!col.CompareTag("Player"))
        {
            return;
        }

        _entered = false;
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

    public bool HasEntered()
    {
        return _entered;
    }

    public void StartDialogue()
    {

        if (!_checkpoint)
        {
            _eAvailable = false;
            try
            {
                _interactable.SetActive(false);
            }
            catch (System.Exception e)
            {
                Debug.Log("no es un error xd " + e);
            }
            if(!_interactableOnly)
            {
                _player.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
            }
        }
        else if (_checkpoint)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            _panelDialogue.DeactivateGO(gameObject);
        }

        if (_panelDialogue.GetComponent<Player>() != null) // Null check for cutscenes, since those don't need an actual player
        {
            _player.GetComponent<Player>().enabled = false;
            _player.GetComponent<PlayerCombat>().enabled = false;
            _player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            _player.GetComponent<Animator>().SetFloat("Speed", 0f);
            _player.GetComponent<Animator>().SetFloat("speedY", 0.1f);
        }


        _panelDialogue.gameObject.SetActive(true);
        if (_panelDialogueDown)
        {
            _panelDialogue.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f,0f);
            _panelDialogue.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f,0f);
            _panelDialogue.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f,160f);
        }
        else
        {
            _panelDialogue.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f,1f);
            _panelDialogue.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f,1f);
            _panelDialogue.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f,-160f);
        }
        _panelDialogue.ZONENAME = _id;
        _panelDialogue.FirstDialogue(_changeAudio);

    }

    public void ChangeId(string id)
    {
        _id = id;
    }

    void Update()
    {
        if (!_eAvailable || _checkpoint || GameManager.INSTANCE.PAUSED)
        {
            return;
        }

        if (Input.GetButtonDown("Submit") && !_buttonPressed)
        {
            _buttonPressed = true;
            StartDialogue();
        }

        if (Input.GetButtonUp("Submit"))
        {
            _buttonPressed = false;
        }
    }

}
