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

    private string _jsonName = "dialogues.json";
    private ZoneData _zoneData;
    private int _zoneLines;
    private string[] _zoneNames;
    public string ZONENAME;
    private string[] _textParts;

    void Start()
    {
        string locationJson = "Assets/Text/" + _jsonName;
        string content = File.ReadAllText(locationJson);
        _zoneData = JsonUtility.FromJson<ZoneData>(content);

        _zoneNames = GetZoneLines(ZONENAME);
        _zoneLines = _zoneNames.Length;

        DifferentDialogues();
    }

    private string[] GetZoneLines(string zoneName)
    {
        return (string[])typeof(ZoneData).GetField(zoneName).GetValue(_zoneData);
    }

    [System.Serializable]
    public class ZoneData
    {
        public string[] intro_01;
        public string[] intro_02;


        public string[] lvl01_intro;
        public string[] lvl01_bombmaker;
        public string[] lvl01_door;
        public string[] lvl01_alarm;


        public string[] lvl02_portrait;
        public string[] lvl02_tv;
        public string[] lvl02_hammock;
        public string[] lvl02_scrap_not_knowing;
        public string[] lvl02_scrap_knowing;
        public string[] lvl02_brody_intro;
        public string[] lvl02_brody_01;
        public string[] lvl02_brody_02;
    }

    public void MoreDialoguePlz()
    {
        _cont++;

        if (_cont >= _zoneLines)
        {
            _player.GetComponent<Player>().enabled = true;
            _player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
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
        _textParts = _zoneNames[_cont].Split('*');

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