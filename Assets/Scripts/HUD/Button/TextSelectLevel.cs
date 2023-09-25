using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextSelectLevel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] _dialogueText;
    [SerializeField]
    private TextMeshProUGUI _planetDescription;
    private Zone _zone;
    public string ZONENAME;
    private string[] _textParts;
    private int index = 0;

    void Start()
    {
        switch (GameManager.INSTANCE.LEVEL)
        {
            case 4:
                _zone = JsonUtility.FromJson<Zone>(LoadJson.LVL_SELECT);
                break;
            default:
                break;
        }
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

    public void ChangeText(TextMeshProUGUI text, string str)
    {
        ZONENAME = str;

        for (int i = 0; i < _zone.DIALOGUES.Length; i++)
        {
            if (_zone.DIALOGUES[i].ID == ZONENAME)
            {
                index = i;
                break;
            }
        }

        _textParts = _zone.DIALOGUES[index].STRINGS[0].Split('*');
        text.text = _textParts[0];
        _planetDescription.text = _textParts[1];
    }

}
