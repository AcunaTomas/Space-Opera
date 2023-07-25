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
    private string[] _zoneNames;
    public string ZONENAME;
    private string[] _textParts;
    private int index = 0;
    [SerializeField]
    private KeyCode _keyNextDialogue;
    [SerializeField]
    private Animator _animatorCinematic;
   
    private bool _ePressed = false;

    void Start()
    {
        _zone = JsonUtility.FromJson<Zone>(LoadJson.CONTENT);

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
            transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "MAS";
            gameObject.SetActive(false);
            ScenesManager.Instance.LoadScene(ScenesManager.Scene.Tutorial);

            _cont = 0;
            return;
        }
        _animatorCinematic.SetTrigger(_cont.ToString());
        DifferentDialogues();

        if (_zoneLines-1 == _cont)
        {
            transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "END";
        }
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
        if ((Input.GetKeyDown(_keyNextDialogue) || Input.GetAxis("Submit") > 0) && !_ePressed)
        {
            _ePressed = true;
            MoreDialoguePlz();
        }

        if (Input.GetKeyUp(_keyNextDialogue))
        {
            _ePressed = false;
        }
    }

}
