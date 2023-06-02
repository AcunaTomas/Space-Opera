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
    private LevelData _levelData;
    private int _zoneLines;
    private string desiredLevel;
    private string desiredZone;

    void Start()
    {
        string locationJson = "Assets/Text/" + _jsonName;
        string content = File.ReadAllText(locationJson);
        _levelData = JsonUtility.FromJson<LevelData>(content);

        Debug.Log(_levelData.lvl_01.zone_01.lines[0]);
        _zoneLines = _levelData.lvl_01.zone_01.lines.Length;

        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _levelData.lvl_01.zone_01.lines[_cont];
    }

    [System.Serializable]
    public class LevelData
    {
        public ZoneData lvl_01;
        public ZoneData lvl_02;
    }

    [System.Serializable]
    public class ZoneData
    {
        public TextData zone_01;
        public TextData zone_02;
    }

    [System.Serializable]
    public class TextData
    {
        public string[] lines;
    }

    public void MoreDialoguePlz()
    {
        _cont++;

        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _levelData.lvl_01.zone_01.lines[_cont];
        if (_zoneLines-1 == _cont)
        {
            transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "END";
        }

        if (_cont >= _zoneLines)
        {
            _player.GetComponent<Player>().enabled = true;
            _player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            gameObject.SetActive(false);
            _cont = 0;
        }
    }

}