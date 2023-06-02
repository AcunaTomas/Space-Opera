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
    private TextData[] _zones;
    private TextData _indexZone;
    private int _zoneLines;
    private string desiredLevel;
    private string desiredZone;

    void Start()
    {
        string locationJson = "Assets/Text/" + _jsonName;
        string content = File.ReadAllText(locationJson);
        _levelData = JsonUtility.FromJson<LevelData>(content);

        desiredLevel = "lvl_01";
        desiredZone = "woods";

        if (LevelExists(desiredLevel) && ZoneExists(desiredLevel, desiredZone))
        {
            _indexZone = GetZone(desiredLevel, desiredZone);
        }

        _zoneLines = _indexZone.lines.Length;

        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _indexZone.lines[_cont];
    }

    private bool LevelExists(string levelName)
    {
        return _levelData != null && _levelData.levels.ContainsKey(levelName);
    }

    private bool ZoneExists(string levelName, string zoneName)
    {
        if (LevelExists(levelName))
        {
            ZoneData levelZones = _levelData.levels[levelName];
            return levelZones.zones.ContainsKey(zoneName);
        }
        return false;
    }

    private TextData GetZone(string levelName, string zoneName)
    {
        if (LevelExists(levelName))
        {
            ZoneData levelZones = _levelData.levels[levelName];
            if (ZoneExists(levelName, zoneName))
            {
                return levelZones.zones[zoneName];
            }
        }
        return null;
    }

    [System.Serializable]
    public class LevelData
    {
        public Dictionary<string, ZoneData> levels;
    }

    [System.Serializable]
    public class ZoneData
    {
        public Dictionary<string, TextData> zones;
    }

    [System.Serializable]
    public class TextData
    {
        public string[] lines;
    }

    public void MoreDialoguePlz()
    {
        _cont++;

        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _indexZone.lines[_cont];
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