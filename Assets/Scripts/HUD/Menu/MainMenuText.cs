using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI[] _titles;
    private Zone _zone;

    private void Awake()
    {
        _zone = JsonUtility.FromJson<Zone>(LoadJson.MAIN_MENU);
    }

    private void Start()
    {
        ChangeText();
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

    public void ChangeText()
    {
        for (int i = 0; i < _zone.DIALOGUES.Length; i++)
        {
            for (int x = 0; x < _titles.Length; x++)
            {
                if (_zone.DIALOGUES[i].ID == _titles[x].name)
                {
                    _titles[x].text = _zone.DIALOGUES[i].STRINGS[0];
                    break;
                }
            }
        }
    }
}
