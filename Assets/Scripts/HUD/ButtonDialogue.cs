using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class ButtonDialogue : MonoBehaviour
{
    
    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private int _cont = 0;

    private string _jsonName = "dialogues.json";
    private ZoneData _zoneData;
    private int _zoneLines;
    private string[] _zoneNames;
    public string _zoneName;

    void Start()
    {
        string locationJson = "Assets/Text/" + _jsonName;
        string content = File.ReadAllText(locationJson);
        _zoneData = JsonUtility.FromJson<ZoneData>(content);

        _zoneNames = GetZoneLines(_zoneName);

        _zoneLines = _zoneNames.Length;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _zoneNames[_cont];
    }

    private string[] GetZoneLines(string zoneName)
    {
        return (string[])typeof(ZoneData).GetField(zoneName).GetValue(_zoneData);
    }

    [System.Serializable]
    public class ZoneData
    {
        public string[] zone_01;
        public string[] zone_02;
    }

    public void MoreDialoguePlz()
    {
        _cont++;

        if (_cont >= _zoneLines)
        {
            _player.GetComponent<Player>().enabled = true;
            _player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            gameObject.SetActive(false);
            _cont = 0;
            return;
        }

        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _zoneNames[_cont];

        if (_zoneLines-1 == _cont)
        {
            transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "END";
        }
    }
}