using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, IDataPersistance
{
    
    public static GameManager INSTANCE;
    public int LEVEL;
    public GameObject PLAYER;
    public GameObject BUTTON_INTERACT;
    public Vector3 CHECKPOINT;
    public GameObject ACTUAL_CHECKPOINT;
    public bool PLAYER_COMBAT = false;
    public ButtonDialogue CANVAS;

    private Player _playerScript;

    private void Awake()
    {
        INSTANCE = this;
        CANVAS = transform.GetChild(1).gameObject.GetComponent<ButtonDialogue>();
        _playerScript = PLAYER.GetComponent<Player>();
    }

    void Start()
    {
        if (LEVEL < 1 || LEVEL > 4 )
        {
            Debug.LogError("El nivel se encuentra fuera del rango permitido (1-4)");
        }
    }
    
    void Update()
    {
        
    }

    void IDataPersistance.LoadData(GameData data)
    {
        _playerScript.SetMaxHP(data.PLAYER_MAX_HP);
        _playerScript.SetHP(data.PLAYER_ACTUAL_HP);
        PLAYER.transform.localPosition = data.PLAYER_POSITION;
    }

    void IDataPersistance.SaveData(ref GameData data)
    {
        data.PLAYER_MAX_HP = _playerScript.GetMaxHP();
        data.PLAYER_ACTUAL_HP = _playerScript.GetHP();
        data.PLAYER_POSITION = CHECKPOINT;
    }

    public void StartGameObject(GameObject obj)
    {
        StartCoroutine(StartTime(obj));   
    }

    private IEnumerator StartTime(GameObject obj)
    {
        yield return new WaitForSeconds(1f);
        obj.SetActive(true);
    }
}
