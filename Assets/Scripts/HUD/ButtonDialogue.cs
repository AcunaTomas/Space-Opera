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

    private int _cont = 0;
    private Zone _zone;
    private int _zoneLines;
    private string[] _zoneNames;
    public string ZONENAME;
    [SerializeField]
    private string[] _textParts;
    private int _index = 0;
    private bool _ePressed = false;
    private bool _notFirstDialogue = false;

    void Awake()
    {
        _zone = JsonUtility.FromJson<Zone>(LoadJson.CONTENT);
    }

    public void FirstDialogue()
    {
        if (_notFirstDialogue == false)
        {
            for (int i = 2; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1200f, 300f);

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

    public void MoreDialoguePlz()
    {
        print("mor");
        _cont++;

        if (_cont >= _zoneLines)
        {
            StartCoroutine(Fold());
            return;
        }

        DifferentDialogues();
    }

    private IEnumerator Fold()
    {
        RectTransform _elementUI = gameObject.GetComponent<RectTransform>();
        float _time = 0f;
        float _firstWidth = _elementUI.sizeDelta.x;

        for (int i = 2; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        while (_time < 0.5f)
        {
            float _percentage = _time / 0.5f;
            float _newWidth = Mathf.Lerp(_firstWidth, 100f, _percentage);
            _elementUI.sizeDelta = new Vector2(_newWidth, _elementUI.sizeDelta.y);

            _time += Time.deltaTime;
            yield return null;
        }

        _elementUI.sizeDelta = new Vector2(100f, _elementUI.sizeDelta.y);

        yield return new WaitForSeconds(0.1f);

        _player.GetComponent<Player>().enabled = true;
        _player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        gameObject.SetActive(false);

        _cont = 0;
        _notFirstDialogue = false;
        _ePressed = false;
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
    }

    void Update()
    {
        if (!_notFirstDialogue)
        {
            return;
        }
/* 
        if (Input.GetKeyDown(_keyNextDialogue) && !_ePressed)
        {
            _ePressed = true;
            MoreDialoguePlz();
        }

        if (Input.GetKeyUp(_keyNextDialogue))
        {
            _ePressed = false;
        } */

        // if ((Input.GetAxis("Submit") > 0) && )
        // {
        //     _ePressed = false;
        //     MoreDialoguePlz();
        // }
        // else
        // {
        //     _ePressed = true;
        // }
    }
    
}