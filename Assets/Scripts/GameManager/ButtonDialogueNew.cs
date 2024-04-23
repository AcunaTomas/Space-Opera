using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class ButtonDialogueNew : MonoBehaviour
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


    private int _contLinea = 0;
    private int _contRepe = 0;

    [SerializeField] private TextAsset lvlDialog;
    
    [System.Serializable]
    public class CSVFormat
    {
        public string ID;
        public string PROFILE;
        public string IMAGE;
        public string EMOTION;
        public string TEXT;
        public string SFX;
        public string LANG;
    }

    [System.Serializable]
    public class CSVList
    {
        public CSVFormat[] list;
    }
    [SerializeField] private CSVList ListaDialogo = new CSVList();
    void ReadCSV()
    {
        string[] data = lvlDialog.text.Split(new string[] { ";", "\n" }, StringSplitOptions.None);

        int tableSize = data.Length / 7 - 1;
        ListaDialogo.list = new CSVFormat[tableSize];

        for(int i = 0; i < tableSize; i++)
        {
            ListaDialogo.list[i] = new CSVFormat();

            ListaDialogo.list[i].ID = data[7 * (i + 1)];
            ListaDialogo.list[i].PROFILE = data[7 * (i + 1)+ 1];
            ListaDialogo.list[i].IMAGE = data[7 * (i + 1) + 2];
            ListaDialogo.list[i].EMOTION = data[7 * (i + 1) + 3];
            ListaDialogo.list[i].TEXT = data[7 * (i + 1) + 4];
            ListaDialogo.list[i].SFX = data[7 * (i + 1) + 5];
            ListaDialogo.list[i].LANG = data[7 * (i + 1) + 6];
        }
    }
    [SerializeField]
    //private Zone _zone;
    private int _zoneLines;
    private string[] _zoneNames;
    public string ZONENAME;
    [SerializeField]
    private string[] _textParts;
    private int _index = 0;
    private bool _notFirstDialogue = false;
    private bool _endLine;
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
        ReadCSV();
        _dip = GetComponent<DialogueImgPj>();
        if (lvlDialog != null)
        {
            return;
        }
        _player = GameManager.INSTANCE.PLAYER;
        lifeBarAnim = transform.parent.GetChild(0).gameObject.GetComponent<Animator>();
    }

    void Start()
    {
        ReadCSV();
        //Debug.Log("ZoneName " + ZONENAME + " Length xd: " + ListaDialogo.list.Length);
    }
    
    public void FirstDialogue(CollisionDialogue.ChangeAudio _changeAudio)
    {
        _buttonPressed = false;
        Debug.Log("ZoneName First Dialogue:" + ZONENAME);
        if (_notFirstDialogue == false)
        {
            _zoneLines = 0;
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

            //for (int i = 0; i < _zone.DIALOGUES.Length; i++)
            for (int i = 0; i < ListaDialogo.list.Length; i++)
            {
                if (ListaDialogo.list[i].ID == ZONENAME)
                {
                    if(_index == -1) _index = i;
                    _zoneLines++;
                    Debug.Log("Tengo el index");
                    //break;
                }
            }
            _contLinea = _index;
            DifferentDialogues();
            _notFirstDialogue = true;
            return;
        }
        
        MoreDialoguePlz();

    }
    
    public void MoreDialoguePlz()
    {
        _contRepe++;
        _contLinea = _index + _contRepe;
        try
        {
            /*
            if (_textParts[5] != null)
            {
                GameObject _go = GameObject.Find(_textParts[5]);
                _go.BroadcastMessage(_textParts[4], SendMessageOptions.DontRequireReceiver);
            }
            */
        }
        catch (System.Exception)
        {

        }
        
        if (_contRepe >= _zoneLines)
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

    private void DifferentDialogues()
    {
        //_textParts = _zone.DIALOGUES[_index].STRINGS[_cont].Split(';');
        _endLine = false;
        if (ListaDialogo.list[_contLinea].PROFILE == "Narrator")
        {
            _characterPanelName.SetActive(false);
            if(!_endLine) //texto separado en caracteres.
            {
                _textParts = ListaDialogo.list[_contLinea].TEXT.Split(' ');
                StartCoroutine(WaitToText()); //Espera para cada palabra.
            }
            //_dialogueText.text = ListaDialogo.list[_contLinea].TEXT;
            _characterImage.color = new Color (255, 255, 255, 0);
        }
        else
        {
            _characterPanelName.SetActive(true);
            print(_dip.CHARACTERS);
            if(ListaDialogo.list[_contLinea].IMAGE != "")
            {
                List<Emotion> _emos = _dip.CHARACTERS_TRUE[ListaDialogo.list[_contLinea].IMAGE];
                Emotion emo = _emos.FirstOrDefault(e => e.EMOTION == ListaDialogo.list[_contLinea].EMOTION);
                _characterImage.color = new Color (255, 255, 255, 255);
                _characterImage.texture = emo.ICON;
            }
            _characterName.text = ListaDialogo.list[_contLinea].PROFILE;
            if(!_endLine) //texto separado en caracteres.
            {
                _textParts = ListaDialogo.list[_contLinea].TEXT.Split(' ');
                StartCoroutine(WaitToText()); //Espera para cada palabra.
            }
            //_dialogueText.text = ListaDialogo.list[_contLinea].TEXT;
        }
        if(_endLine)
        {
            _contLinea++;
        }
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
        _index = -1;
        _contRepe = 0;
        //_cont = 0;
        _notFirstDialogue = false;
    }
    private IEnumerator WaitToText()
    {
        _dialogueText.text = "";
        for(int line = 0; line < _textParts.Length; line++)
        {
            SoundManager.INSTANCE.SendMessage("PlaySound",new SoundManager.SoundInfo(ListaDialogo.list[_contLinea].SFX,2));
            //Debug.Log("Line nº:" + line + " =" + _textParts[line] + " of " + _textParts.Length);
            if(line == _textParts.Length - 1)
                {
                    _dialogueText.text += _textParts[line];
                    _endLine = true;
                }
            else
            {
                _dialogueText.text +=  _textParts[line] + " ";
            }
            if(_endLine)
            {
                _dialogueText.text = ListaDialogo.list[_contLinea].TEXT;
                break;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
    public string AddText(string _zoneName)
    {
        Awake();
        int _ix = 0;
        for (int i = 0; i < ListaDialogo.list.Length; i++)
        {
            if (ListaDialogo.list[i].ID == _zoneName)
            {
                _ix = i;
                break;
            }
        }
        return ListaDialogo.list[_ix].ID;
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
        if (lvlDialog != null && _player != null)
        {
            return;
        }
        //_zone = GameManager.INSTANCE.lvlDiag;
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
            if(!_endLine)
            {
                _endLine = true;
            }
            else
            {
                MoreDialoguePlz();
            }
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

    public void SetLvlDialog(TextAsset newText)
    {
        lvlDialog = newText;
    }
}