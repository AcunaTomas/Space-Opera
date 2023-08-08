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
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private KeyCode _keyNextDialogue;
    [SerializeField]
    private DialogueImgPj _dip;
    [SerializeField]
    private bool _quilombo = false;

    private int _cont = 0;
    private Zone _zone;
    private int _zoneLines;
    private string[] _zoneNames;
    public string ZONENAME;
    [SerializeField]
    private string[] _textParts;
    private int _index = 0;
    private bool _notFirstDialogue = false;
    private GameObject _dialogueDeactivate;

    public Animator lifeBarAnim;

    void Awake()
    {
        switch (GameManager.INSTANCE.LEVEL)
        {
            case 1:
                _zone = JsonUtility.FromJson<Zone>(LoadJson.LVL1);
                break;
            case 2:
                _zone = JsonUtility.FromJson<Zone>(LoadJson.LVL2);
                break;
            default:
                break;
        }
    }

    public void FirstDialogue(CollisionDialogue.ChangeAudio _changeAudio)
    {
        if (_notFirstDialogue == false)
        {
            switch (_changeAudio)
            {
                case CollisionDialogue.ChangeAudio.dialogo:
                    AudioManager.INSTANCE.PlayDialogueInteractor();
                    Debug.Log(_changeAudio);
                    break;
                case CollisionDialogue.ChangeAudio.especial:
                    AudioManager.INSTANCE.PlayDialogueInteractor();
                    Debug.Log(_changeAudio);
                    break;
                default:
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

    public void MoreDialoguePlz()
    {
        _cont++;

        if (_cont >= _zoneLines)
        {
            bool piloto = true;
            if ((ZONENAME == "lvl01_pilot_with_key"))
            {
                GameObject.FindWithTag("Boton").SetActive(false);
                piloto = false;
            }
            
            if ((ZONENAME == "lvl01_ship_key"))
            {
                GameObject.FindWithTag("Key").SetActive(false);
            }   

            if ((ZONENAME == "lvl02_brody_03"))
            {
                ScenesManager.Instance.LoadNextScene("EndDemo");
            }

            StartCoroutine(Fold(piloto));
            if (_dialogueDeactivate != null)
            {
                _dialogueDeactivate.SetActive(false);
            }
            return;
        }
        
        if ((ZONENAME == "lvl01_pilot_with_key") && (_cont == 6))
        {
            GameObject.FindWithTag("AscensorNave").GetComponent<ElevatorController>().Interact_Action();
        }



        if ((ZONENAME == "lvl02_brody_03") && (_cont == 7))
        {
            GameObject.Find("HolyShitHereComeDatBOI").GetComponent<Animator>().SetBool("ModoJajas", true);
        }
        else
        {
            GameObject.Find("HolyShitHereComeDatBOI").GetComponent<Animator>().SetBool("ModoJajas", false);
        }

        if ((ZONENAME == "lvl02_brody_03") && (_cont == 27 || _cont == 28 || _cont == 38))
        {
            GameObject.Find("HolyShitHereComeDatBOI").GetComponent<Animator>().SetBool("EdgyModo", true);
        }
        else
        {
            GameObject.Find("HolyShitHereComeDatBOI").GetComponent<Animator>().SetBool("EdgyModo", false);
        }


        if ((ZONENAME == "lvl02_brody_03") && (_cont == 33))
        {
            _player.GetComponent<Animator>().SetBool("BitoMode", true);
        }
        

        DifferentDialogues();
    }

    private IEnumerator Fold(bool piloto)
    {
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

        _player.GetComponent<Player>().enabled = piloto;
        if (GameManager.INSTANCE.PLAYER_COMBAT)
        {
            _player.GetComponent<PlayerCombat>().enabled = true;
        }
        else{
            _player.GetComponent<PlayerCombat>().enabled = false;
        }
        _player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
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
            _dialogueText.text = _textParts[2];
            _characterImage.color = new Color (255, 255, 255, 0);
        }
        else
        {
            _characterPanelName.SetActive(true);
            
            List<Emotion> _emos = _dip.CHARACTERS_TRUE[_textParts[0]];
            Emotion emo = _emos.FirstOrDefault(e => e.EMOTION == _textParts[1]);
            _characterImage.color = new Color (255, 255, 255, 255);
            _characterImage.texture = emo.ICON;

            _dialogueText.text = _textParts[2];
            _characterName.text = _textParts[0];
        }

        if (_textParts[0] == "NarratorBrody")
        {
            _characterPanelName.SetActive(false);
        }
    }

    public void setQuilombo()
    {
        _quilombo = true;
    }
    void OnEnable()
    {
        lifeBarAnim.SetTrigger("Disappear");
    }
    void OnDisable()
    {
        if (_quilombo)
        {
            lifeBarAnim.SetTrigger("Appear");
        }

    }
    void Update()
    {
        if (!_notFirstDialogue)
        {
            return;
        }
    }
    
}