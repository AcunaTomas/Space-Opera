using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CinematicDialogue : MonoBehaviour
{
    
    [SerializeField]
    private TextMeshProUGUI _dialogueText;
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
        _cont++;

        if (_cont >= _zoneLines)
        {
            transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "MAS";
            gameObject.SetActive(false);

            _cont = 0;
            return;
        }

        DifferentDialogues();

        if (_zoneLines-1 == _cont)
        {
            transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "END";
        }
    }

    private void DifferentDialogues()
    {
        _textParts = _zone.DIALOGUES[index].STRINGS[_cont].Split('*');
        _dialogueText.text = _textParts[2];
    }

}
