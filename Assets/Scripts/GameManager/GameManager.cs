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

    //LEVEL 1
    [Header("LEVEL 1\n")]
    public GameObject CANVAS_WS_LVL1;
    public GameObject TUTO_TRIG_LVL1;
    public GameObject CHECKPOINTS_LVL1;
    public GameObject DIALOGUES_LVL1;

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
        if (Input.GetKeyDown("1"))
        {
            DataPersistentManager.INSTANCE.DeleteSave();
        }
    }

    void IDataPersistance.LoadData(GameData data)
    {
        _playerScript.SetMaxHP(data.PLAYER_MAX_HP);
        _playerScript.SetHP(data.PLAYER_ACTUAL_HP);
        PLAYER.transform.localPosition = data.PLAYER_POSITION;

        switch (LEVEL)
        {
            //LEVEL 1
            case 1: //LEVEL 1
            //LEVEL 1

                for (int i = 0; i < data.CANVAS_WS_LVL1_GENERAL.Length; i++)
                {
                    if (!data.CANVAS_WS_LVL1_GENERAL[i])
                    {
                        CANVAS_WS_LVL1.transform.GetChild(i).gameObject.SetActive(false);
                    }
                }

                for (int i = 0; i < data.CANVAS_WS_LVL1_ACTIVATE_EVENTS.Length; i++)
                {
                    if (data.CANVAS_WS_LVL1_ACTIVATE_EVENTS[i])
                    {
                        CANVAS_WS_LVL1.transform.GetChild(i).BroadcastMessage("manualDo", SendMessageOptions.DontRequireReceiver);
                    }
                }

                for (int i = 0; i < data.TUTO_TRIG_LVL1.Length; i++)
                {
                    if (!data.TUTO_TRIG_LVL1[i])
                    {
                        TUTO_TRIG_LVL1.transform.GetChild(i).gameObject.SetActive(false);
                    }
                }

                for (int i = 0; i < data.CHECKPOINTS_LVL1.Length; i++)
                {
                    if (!data.CHECKPOINTS_LVL1[i])
                    {
                        CHECKPOINTS_LVL1.transform.GetChild(i).gameObject.SetActive(false);
                    }
                    else
                    {
                        CHECKPOINTS_LVL1.transform.GetChild(i).gameObject.SetActive(true);
                    }
                }

                for (int i = 0; i < data.DIALOGUES_LVL1.Length; i++)
                {

                    if (!data.DIALOGUES_LVL1[i])
                    {
                        if (i == data.DIALOGUES_LVL1.Length-1)
                        {
                            DIALOGUES_LVL1.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
                        }
                        else
                        {
                            DIALOGUES_LVL1.transform.GetChild(i).gameObject.SetActive(false);
                        }
                    }
                    else
                    {
                        if (i == data.DIALOGUES_LVL1.Length-1)
                        {
                            DIALOGUES_LVL1.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
                        }
                        else
                        {
                            DIALOGUES_LVL1.transform.GetChild(i).gameObject.SetActive(true);
                        }
                    }
                }

                break;

            default:
                break;
        }
    }

    void IDataPersistance.SaveData(ref GameData data)
    {
        data.PLAYER_MAX_HP = _playerScript.GetMaxHP();
        data.PLAYER_ACTUAL_HP = _playerScript.GetHP();
        data.PLAYER_POSITION = CHECKPOINT;

        switch (LEVEL)
        {
            //LEVEL 1
            case 1: //LEVEL 1
            //LEVEL 1

                for (int i = 0; i < CANVAS_WS_LVL1.transform.childCount; i++)
                {
                    if (!CANVAS_WS_LVL1.transform.GetChild(i).gameObject.activeSelf)
                    {
                        data.CANVAS_WS_LVL1_GENERAL[i] = false;
                    }
                }

                for (int i = 0; i < CANVAS_WS_LVL1.transform.childCount; i++)
                {
                    if (CANVAS_WS_LVL1.transform.GetChild(i).GetComponent<Image>().enabled && CANVAS_WS_LVL1.transform.GetChild(i).gameObject.activeSelf)
                    {
                        data.CANVAS_WS_LVL1_ACTIVATE_EVENTS[i] = true;
                    }
                    else
                    {
                        data.CANVAS_WS_LVL1_ACTIVATE_EVENTS[i] = false;
                    }
                }

                for (int i = 0; i < TUTO_TRIG_LVL1.transform.childCount; i++)
                {
                    if (!TUTO_TRIG_LVL1.transform.GetChild(i).gameObject.activeSelf)
                    {
                        data.TUTO_TRIG_LVL1[i] = false;
                    }
                }

                for (int i = 0; i < CHECKPOINTS_LVL1.transform.childCount; i++)
                {
                    if (!CHECKPOINTS_LVL1.transform.GetChild(i).gameObject.activeSelf)
                    {
                        data.CHECKPOINTS_LVL1[i] = false;
                    }
                }

                for (int i = 0; i < DIALOGUES_LVL1.transform.childCount; i++)
                {
                    if (i == data.DIALOGUES_LVL1.Length-1)
                    {
                        if (!DIALOGUES_LVL1.transform.GetChild(i).GetChild(0).gameObject.activeSelf)
                        {
                            data.DIALOGUES_LVL1[i] = false;
                        }
                        else
                        {
                            data.DIALOGUES_LVL1[i] = true;
                        }
                    }
                    else
                    {
                        if (!DIALOGUES_LVL1.transform.GetChild(i).gameObject.activeSelf)
                        {
                            data.DIALOGUES_LVL1[i] = false;
                        }
                        else
                        {
                            data.DIALOGUES_LVL1[i] = true;
                        }
                    }
                }

                break;

            default:
                break;
        }
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
