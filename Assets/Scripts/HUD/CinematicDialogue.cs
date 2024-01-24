using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class CinematicDialogue : MonoBehaviour
{
    
    [SerializeField]
    private TextMeshProUGUI _dialogueText;
    private int _cont = 0;
    private int _contCinematic = 2;
    private Zone _zone;
    private int _zoneLines;
    public string ZONENAME;
    private string[] _textParts;
    private int index = 0;
    [SerializeField]
    private KeyCode _keyNextDialogue;
    [SerializeField]
    private Animator _animatorCinematic;
    [SerializeField]
    private string _sceneName;
    //DialogueSkip
    private bool _dialogueSkipEnd = true;
    [SerializeField]
    private Image _skipBar;
    private float _holdSkip = 0f;
    private float _holdToSkip = 3f;

    void Start()
    {
        switch (GameManager.INSTANCE.LEVEL)
        {
            case 1:
                _zone = JsonUtility.FromJson<Zone>(LoadJson.LVL1_CINEMATIC);
                break;
            case 2:
                _zone = JsonUtility.FromJson<Zone>(LoadJson.LVL2_CINEMATIC);
                break;
            case 3:
                _zone = JsonUtility.FromJson<Zone>(LoadJson.LVL_2X);
                break;
            case 4:
                _zone = JsonUtility.FromJson<Zone>(LoadJson.LVL_SELECT);
                break;
            case 5:
                _zone = JsonUtility.FromJson<Zone>(LoadJson.LVL3_CINEMATIC);
                break;
            default:
                break;
        }

        for (int i = 0; i < _zone.DIALOGUES.Length; i++)
        {
            if (_zone.DIALOGUES[i].ID == ZONENAME)
            {
                index = i;
                break;
            }
        }

        _zoneLines = _zone.DIALOGUES[index].STRINGS.Length;
        DifferentDialogues();
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
        if (_cont >= _zoneLines)
        {
            gameObject.SetActive(false);
            if (_sceneName != "")
            {
                ScenesManager.Instance.LoadNextScene(_sceneName);
            }
            else
            {
                try
                {
                    GameObject _go = GameObject.Find("ActivatePanel");
                    _go.BroadcastMessage("manualDo", SendMessageOptions.DontRequireReceiver);
                }
                catch (System.Exception)
                {

                }
            }

            _cont = 0;
            return;
        }
        _animatorCinematic.SetTrigger(_cont.ToString());
        DifferentDialogues();
    }

    private void DifferentDialogues()
    {
        _textParts = _zone.DIALOGUES[index].STRINGS[_cont].Split('*');
        if (_textParts.Length > 3)
        {
            _dialogueText.text = _textParts[_contCinematic];
            _contCinematic++;
            if (_contCinematic == _textParts.Length){
                _cont++;
                _contCinematic = 2;
            }
        }
        else
        {
            _dialogueText.text = _textParts[_contCinematic];
            _cont++;
        }
    }
    private void ActualizarSkip(float cantidade)
    {
        _holdSkip += cantidade;
        _skipBar.fillAmount = _holdSkip / _holdToSkip;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Submit"))
        {
            MoreDialoguePlz();
        }

        if (Input.GetButtonUp("Jump") || Input.GetButtonUp("Submit"))
        {
            ActualizarSkip(-_holdSkip);
        }

        
        if(Input.GetButton("Jump") || Input.GetButton("Submit"))
        {
            if(_dialogueSkipEnd)
            {
                Debug.Log("charging skip");
                ActualizarSkip(Time.deltaTime);         
                if(_holdSkip >= _holdToSkip)
                {
                    ScenesManager.Instance.LoadNextScene(_sceneName);
                    _dialogueSkipEnd = false;
                }
            }
        }
    }

}
