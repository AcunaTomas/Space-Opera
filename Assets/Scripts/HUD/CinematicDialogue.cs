using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private bool _ePressed = false;

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

    void Update()
    {
        if ((Input.GetButtonDown("Jump") || Input.GetButtonDown("Submit")) && !_ePressed)
        {
            _ePressed = true;
            MoreDialoguePlz();
        }

        if (Input.GetButtonUp("Jump") || Input.GetButtonDown("Submit"))
        {
             _ePressed = false;
        }
    }

}
