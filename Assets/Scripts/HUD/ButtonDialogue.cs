using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class ButtonDialogue : MonoBehaviour
{
    [SerializeField]
    private GameObject _characterImage;
    [SerializeField]
    private TextMeshProUGUI _dialogueText;
    [SerializeField]
    private GameObject _characterPanelName;
    [SerializeField]
    private TextMeshProUGUI _characterName;
    [SerializeField]
    private GameObject _player;
    private int _cont = 0;

    private Zone _zone;
    private int _zoneLines;
    private string[] _zoneNames;
    public string ZONENAME;
    private string[] _textParts;
    private int index = 0;
    
    void Start()
    {
        _zone = JsonUtility.FromJson<Zone>(LoadJson.CONTENT);
        gameObject.SetActive(false);
    }

    public void LoadZoneText(string RequestedZoneName = "lvl1_intro")
    {
        
        for (int i = 0; i < _zone.DIALOGUES.Length; i++)
        {
            if (_zone.DIALOGUES[i].ID == RequestedZoneName)
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
        _cont++;

        if (_cont >= _zoneLines)
        {
            _player.GetComponent<Player>().enabled = true;
            _player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

            transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "MAS";
            
            gameObject.SetActive(false);

            _cont = 0;
            return;
        }

        DifferentDialogues();

        if (_zoneLines-1 == _cont)
        {
            transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "END";
        }
    }

    private void DifferentDialogues()
    {
        _textParts = _zone.DIALOGUES[index].STRINGS[_cont].Split('*');

        if (_textParts[0] == "Narrator")
        {
            _characterPanelName.SetActive(false);
            _dialogueText.text = "<i>"+_textParts[2]+"</i>";
        }
        else
        {
            _characterPanelName.SetActive(true);
            _dialogueText.text = _textParts[2];
            _characterName.text = _textParts[0];
        }
    }
}