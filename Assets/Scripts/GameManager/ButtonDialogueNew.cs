using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using FMODUnity;

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
    [SerializeField] private StudioEventEmitter _nextLine;


    private int _contLinea = 0;
    private int _contRepe = 0;

    [SerializeField] private TextAsset lvlDialog;
    [SerializeField] float typingTime = .02f;
    
    [System.Serializable]
    public class CSVFormat
    {
        public string ID;
        public string PROFILE;
        public string IMAGE;
        public string EMOTION;
        public string SFX;
        public string TEXT;
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
            ListaDialogo.list[i].SFX = data[7 * (i + 1) + 4];
            if(DataPersistentManager.INSTANCE !=null)
            {
                if(DataPersistentManager.INSTANCE.Language() == "ES") ListaDialogo.list[i].TEXT = data[7 * (i + 1) + 5];
                else ListaDialogo.list[i].TEXT = data[7 * (i + 1) + 6];
            }
            else ListaDialogo.list[i].TEXT = data[7 * (i + 1) + 5];
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
        SetLvlDialog(GameManager.INSTANCE.lvlDialog);
        ReadCSV();
        _dip = GetComponent<DialogueImgPj>();
        if (lvlDialog != null)
        {
            return;
        }
        _player = GameManager.INSTANCE.GetActivePlayer().gameObject;
        lifeBarAnim = transform.parent.GetChild(0).gameObject.GetComponent<Animator>();
    }

    void Start()
    {
        ReadCSV();
        //Debug.Log("ZoneName " + ZONENAME + " Length xd: " + ListaDialogo.list.Length);
    }
    
    public void FirstDialogue(CollisionDialogue.ChangeAudio _changeAudio, bool MoveAfterDialogue)
    {
        _buttonPressed = false;
        Debug.Log("ZoneName First Dialogue:" + ZONENAME);
        _playerMovesAfterDialogue = MoveAfterDialogue;
        if (_notFirstDialogue == false)
        {
            StartCoroutine(UnFold());
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
            GameManager.INSTANCE.AllMovementToggle(_playerMovesAfterDialogue);
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
            //print(_dip.CHARACTERS); print de srchenko
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
    private IEnumerator UnFold() //Acá se llama al primer díalogo 'u'
    {
        RectTransform _elementUI = gameObject.GetComponent<RectTransform>();
        float _time = 0f;
        float _firstWidth = 1200;

        while (_time < 0.3f)
        {
            float _percentage = _time / 0.3f;
            float _newWidth = Mathf.Lerp(0f, _firstWidth, _percentage);
            _elementUI.sizeDelta = new Vector2(_newWidth, _elementUI.sizeDelta.y);
            _time += Time.deltaTime;
            yield return null;
        }
        _elementUI.sizeDelta = new Vector2(_firstWidth, _elementUI.sizeDelta.y);
        yield return new WaitForSeconds(0.1f);
        _zoneLines = 0;
            _stopSubmit = false;
            GameManager.INSTANCE.ALTSKIPENABLED = "AltDialogueSkip";
            //SoundManager.INSTANCE.SendMessage("PlaySound",new SoundManager.SoundInfo("Dialogue_Interact",2));
            for (int i = 2; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
                    //gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1200f, 250f);

            //for (int i = 0; i < _zone.DIALOGUES.Length; i++)
            for (int i = 0; i < ListaDialogo.list.Length; i++)
            {
                if (ListaDialogo.list[i].ID == ZONENAME)
                {
                    if(_index == -1) _index = i;
                    _zoneLines++;
                    Debug.Log("Se qué estás leyendo esto ( ͡° ͜ʖ ͡°)"); //index
                    //break;
                }
            }
            GameManager.INSTANCE.PAUSABLE = false;
            InputManager.INSTANCE.UIInputs();
            _contLinea = _index;
            DifferentDialogues();
            _notFirstDialogue = true;
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
            //_player.GetComponent<PlayerCombat>().enabled = _playerMovesAfterDialogue;
            // if (GameManager.INSTANCE.PLAYER_COMBAT)
            // {
            //     _player.GetComponent<PlayerCombat>().enabled = true;
            // }
            // else
            // {
            //     _player.GetComponent<PlayerCombat>().enabled = false;
            // }
            GameManager.INSTANCE.PAUSABLE = true;
            InputManager.INSTANCE.InGameInputs();
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
        _dialogueText.text = string.Empty;
        _dialogueText.text = ListaDialogo.list[_contLinea].TEXT;
        _dialogueText.maxVisibleCharacters = 0;
        int chCounterAll = 0;
        bool color = false;
        
        for(int word = 0; word < _textParts.Length; word++)
        {
            //SoundManager.INSTANCE.SendMessage("PlaySound", new SoundManager.SoundInfo(ListaDialogo.list[_contLinea].SFX,2));
            int chCounterWord = 0;
            
            foreach(char ch in _textParts[word])
            {
                chCounterWord++;
                if(_endLine)
                {
                    _dialogueText.text = ListaDialogo.list[_contLinea].TEXT;
                    _dialogueText.maxVisibleCharacters = 200;
                    break;
                }
                if(ch == '<') color = true;
                if(color)
                {
                    if(ch == '>') color = false;
                    yield return new WaitForSeconds(0);
                }
                else
                {
                chCounterAll++;
                _dialogueText.maxVisibleCharacters = chCounterAll;
                    //SoundManager.INSTANCE.SendMessage("PlaySound", new SoundManager.SoundInfo(ListaDialogo.list[_contLinea].SFX,2));
                //_dialogueText.text += ch;
                yield return new WaitForSeconds(typingTime);
                }
            }
            
            //Debug.Log("Caracteres: "+ chCounterWord);
            //Debug.Log("Caracteres split: "+_textParts[word].Length);
            if(word == _textParts.Length - 1 && chCounterWord == _textParts[word].Length) //Si es la última palabra y caracter entonces es la última linea
            {
                //_dialogueText.text += _textParts[word];
                _endLine = true;
            }
            else //Esto para tener en cuenta el espacio después de cada palabra
            {
                chCounterAll++;
                _dialogueText.maxVisibleCharacters = chCounterAll;
                yield return new WaitForSeconds(typingTime);
                //_dialogueText.text +=  _textParts[word] + " ";
            }
            if(_endLine)
            {
                _dialogueText.text = ListaDialogo.list[_contLinea].TEXT;
                _dialogueText.maxVisibleCharacters = 200;
                //Debug.Log("Caracteres: " + chCounterAll);
                break;
            }
            //Debug.Log(_dialogueText.maxWidth);
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
        return ListaDialogo.list[_ix].TEXT;
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
        _player = GameManager.INSTANCE.GetActivePlayer().gameObject;
        if (lvlDialog != null && _player != null)
        {
            return;
        }
        //_zone = GameManager.INSTANCE.lvlDiag;
        
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

        if(InputManager.INSTANCE.SubmitInput && !_buttonPressed)
        {
            _buttonPressed = true;
            if(!_endLine)
            {
                _endLine = true;
            }
            else
            {
                MoreDialoguePlz();
                _nextLine.Play();
            }
        }

        if (InputManager.INSTANCE.SubmitReleased)
        {
            ActualizarSkip(-_holdSkip);
            _buttonPressed = false;
        }

        if(InputManager.INSTANCE.SubmitHeld && LoadJson.DEBUG_MODE)
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

    public void UpdateLanguage()
    {
        ReadCSV();
    }
}