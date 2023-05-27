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

    private string jsonName = "dialogues.json";
    private DataText _data;

    void Start()
    {
        string locationJson = "Assets/Text/" + jsonName;
        string content = File.ReadAllText(locationJson);
        _data = JsonUtility.FromJson<DataText>(content);

        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _data.ID1;
    }

    [System.Serializable]
    public class DataText
    {
        public string ID1;
        public string ID2;
        public string ID3;
    }

    public void MoreDialoguePlz()
    {
        _cont++;
        switch (_cont)
        {
            case 1:
                transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _data.ID2;
                break;
            case 2:
                transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _data.ID3;
                transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "END";
                break;
            default:
                break;
        }
        if (_cont >= 3)
        {
            _player.GetComponent<Player>().enabled = true;
            _player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            gameObject.SetActive(false);
            _cont = 0;
        }
    }

}
