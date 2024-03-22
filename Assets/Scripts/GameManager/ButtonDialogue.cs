using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Linq;

public class ButtonDialogue : MonoBehaviour
{
    [SerializeField]
    private RawImage _characterImage;
    [SerializeField]
    private TextMeshProUGUI _dialogueText;
    [SerializeField]
    private GameObject _characterPanelName;
    [SerializeField]
    private TextMeshProUGUI _characterName;

    public GameObject _player;
    [SerializeField]
    private DialogueImgPj _dip;
    [SerializeField]
    private GameObject[] _dialogueSkipAction;
    private int _dialogueNumber;
    private bool _dialogueSkipEnd;
    [SerializeField]
    private Image _skipBar;


    private int _cont = 0;
    [SerializeField]
    private Zone _zone;
    private int _zoneLines;
    private string[] _zoneNames;
    public string ZONENAME;
    [SerializeField]
    private string[] _textParts;
    private int _index = 0;
    private bool _notFirstDialogue = false;
    private GameObject _dialogueDeactivate;
    private bool _playerMovesAfterDialogue = true;
    private bool _buttonPressed = false;
    private bool _stopSubmit = false;

    //DialogueSkip
    private float _holdSkip = 0f;
    private float _holdToSkip = 3f;

    [SerializeField]
    private Animator lifeBarAnim;

    void Awake()
    {
        _dip = GetComponent<DialogueImgPj>();
        if (_zone != null)
        {
            return;
        }
        _zone = GameManager.INSTANCE.lvlDiag;
        _player = GameManager.INSTANCE.PLAYER;
        lifeBarAnim = transform.parent.GetChild(0).gameObject.GetComponent<Animator>();

    }


    public string[] AddText(string _zoneName)
    {
        Awake();
        int _ix = 0;
        for (int i = 0; i < _zone.DIALOGUES.Length; i++)
        {
            if (_zone.DIALOGUES[i].ID == _zoneName)
            {
                _ix = i;
                break;
            }
        }

        return _zone.DIALOGUES[_ix].STRINGS[0].Split('#');
    }

    public void FirstDialogueSelectLvl ()
    {
        FirstDialogue(CollisionDialogue.ChangeAudio.dialogo);
    }

    public void FirstDialogue(CollisionDialogue.ChangeAudio _changeAudio)
    {
        _buttonPressed = false;
        if (_notFirstDialogue == false)
        {
            _stopSubmit = false;
            GameManager.INSTANCE.ALTSKIPENABLED = "AltDialogueSkip";
            switch (_changeAudio)
            {
                case CollisionDialogue.ChangeAudio.especial:
                    SoundManager.INSTANCE.SendMessage("PlaySound",new SoundManager.SoundInfo("Dialogue_Interact",2));
                    break;
                default:
                    SoundManager.INSTANCE.SendMessage("PlaySound",new SoundManager.SoundInfo("Dialogue_Interact",2));
                    break;
            }
            for (int i = 2; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1200f, 250f);

            for (int i = 0; i < _zone.DIALOGUES.Length; i++)
            {
                if (_zone.DIALOGUES[i].ID == ZONENAME)
                {
                    _index = i;
                    break;
                }
            }

            _zoneLines = _zone.DIALOGUES[_index].STRINGS.Length;

            DifferentDialogues();
            _notFirstDialogue = true;
            return;
        }
        
        MoreDialoguePlz();

    }

    [System.Serializable]
    public class ZoneData
    {
        public string ID;
        public string[] STRINGS;
    }

    [System.Serializable]
    public class Zone
    {
        public ZoneData[] DIALOGUES;
    }

    public void DeactivateGO(GameObject go)
    {
        _dialogueDeactivate = go;
    }

    public void PlayerMovesAfterDialogue(bool bl)
    {
        _playerMovesAfterDialogue = bl;
    }

    public void MoreDialoguePlz()
    {
        _cont++;

        try
        {
            if (_textParts[5] != null)
            {
                GameObject _go = GameObject.Find(_textParts[5]);
                _go.BroadcastMessage(_textParts[4], SendMessageOptions.DontRequireReceiver);
            }
        }
        catch (System.Exception)
        {

        }

        if (_cont >= _zoneLines)
        {
            GameManager.INSTANCE.ALTSKIPENABLED = "Disabled";
            StartCoroutine(Fold());
            if (_dialogueDeactivate != null)
            {
                _dialogueDeactivate.SetActive(false);
            }
            GameManager.INSTANCE.AllMovementToggle(true);
            return;
        }

        DifferentDialogues();
    }

    private IEnumerator Fold()
    {
        _stopSubmit = true;
        RectTransform _elementUI = gameObject.GetComponent<RectTransform>();
        float _time = 0f;
        float _firstWidth = _elementUI.sizeDelta.x;

        for (int i = 2; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        while (_time < 0.3f)
        {
            float _percentage = _time / 0.3f;
            float _newWidth = Mathf.Lerp(_firstWidth, 0f, _percentage);
            _elementUI.sizeDelta = new Vector2(_newWidth, _elementUI.sizeDelta.y);

            _time += Time.deltaTime;
            yield return null;
        }

        _elementUI.sizeDelta = new Vector2(0f, _elementUI.sizeDelta.y);

        yield return new WaitForSeconds(0.1f);

        try
        {
            _player.GetComponent<Player>().enabled = _playerMovesAfterDialogue;
            if (GameManager.INSTANCE.PLAYER_COMBAT)
            {
                _player.GetComponent<PlayerCombat>().enabled = true;
            }
            else
            {
                _player.GetComponent<PlayerCombat>().enabled = false;
            }
            _player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        catch (System.Exception e)
        {
            Debug.Log("no es un error xd " + e);
        }
        
        gameObject.SetActive(false);

        _cont = 0;
        _notFirstDialogue = false;
    }

    private void DifferentDialogues()
    {
        _textParts = _zone.DIALOGUES[_index].STRINGS[_cont].Split('*');

        if (_textParts[0] == "Narrator")
        {
            _characterPanelName.SetActive(false);
            _dialogueText.text = _textParts[3];
            _characterImage.color = new Color (255, 255, 255, 0);
        }
        else
        {
            _characterPanelName.SetActive(true);
            print(_dip.CHARACTERS);
            List<Emotion> _emos = _dip.CHARACTERS_TRUE[_textParts[1]];
            Emotion emo = _emos.FirstOrDefault(e => e.EMOTION == _textParts[2]);
            _characterImage.color = new Color (255, 255, 255, 255);
            _characterImage.texture = emo.ICON;

            _dialogueText.text = _textParts[3];
            _characterName.text = _textParts[0];
        }

    }
    private void ActualizarSkip(float cantidade)
    {
        _holdSkip += cantidade;
        _skipBar.fillAmount = _holdSkip / _holdToSkip;
    }

    void OnEnable()
    {
        _dialogueSkipEnd = GameManager.INSTANCE.DIALOGUESKIPEND;
        _dialogueNumber = GameManager.INSTANCE.DIALOGUESKIPCOUNT;
        lifeBarAnim.SetTrigger("Disappear");
        if (_zone != null && _player != null)
        {
            return;
        }
        _zone = GameManager.INSTANCE.lvlDiag;
        _player = GameManager.INSTANCE.PLAYER;
    }
    void OnDisable()
    {
        ActualizarSkip(-_holdSkip);
        lifeBarAnim.SetTrigger("Appear");

    }

    void Update()
    {
        if(_stopSubmit || GameManager.INSTANCE.PAUSED)
        {
            return;
        }

        if((Input.GetButtonDown("Jump") || Input.GetButtonDown("Submit")) && !_buttonPressed)
        {
            _buttonPressed = true;
            MoreDialoguePlz();
        }

        if (Input.GetButtonUp("Jump") || Input.GetButtonUp("Submit"))
        {
            ActualizarSkip(-_holdSkip);
            _buttonPressed = false;
        }

        if((Input.GetButton("Jump") || Input.GetButton("Submit")) && LoadJson.DEBUG_MODE)
        {
            if(_dialogueSkipEnd)
            {
                ActualizarSkip(Time.deltaTime);         
                if(_holdSkip >= _holdToSkip)
                {
                    _dialogueSkipAction[_dialogueNumber].SetActive(true);
                    _dialogueSkipEnd = false;
                    StartCoroutine(Fold());
                }
            }
        }
    }


    public void ChangeLifeBar(Animator _newLifeBar)
    {
        lifeBarAnim = _newLifeBar;
    }
}