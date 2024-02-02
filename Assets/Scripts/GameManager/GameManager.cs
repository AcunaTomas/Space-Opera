using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, IDataPersistance
{
    public string ALTSKIPENABLED = "Disabled";
    public static GameManager INSTANCE;
    public int LEVEL;
    public GameObject PLAYER;
    public GameObject BUTTON_INTERACT;
    public Vector3 CHECKPOINT;
    public GameObject ACTUAL_CHECKPOINT;
    public bool PLAYER_COMBAT = false;
    public ButtonDialogue CANVAS;
    public GameObject PAUSE_MENU;
    public GameObject PANEL_OBJECTIVE;
    public bool PAUSED = false;
    public bool QUILOMB_MODE = false;
    public float MUSIC_VOLUME;
    public float SFX_VOLUME;

    private Player _playerScript;
    private bool _escapePressed = false;
    private float restartTime;
    public float dustcap = 0;
    public GameObject VFX_FADE;
    public ButtonDialogue.Zone lvlDiag;
    public bool DIALOGUESKIPEND = false;
    public int DIALOGUESKIPCOUNT;

    //Delete this later
    [Header("LEVEL 1\n")]
    public GameObject CANVAS_WS_LVL1;
    public GameObject TUTO_TRIG_LVL1;
    public GameObject CHECKPOINTS_LVL1;
    public GameObject DIALOGUES_LVL1;
    public GameObject CHANGE_MUSIC_LVL1;
    public GameObject EVENTS_GO_HERE_LVL1;
    public GameObject DOORS_LVL1;
    public GameObject INVISIBLE_TROLL_LVL1;
    public Transform[] ELEVATORS_LVL1;
    public GameObject ELEVATOR_DOOR_LVL1;
    public GameObject[] DUMMIES_LVL1;
    public GameObject PILOT_QUILOMB_LVL1;
    public GameObject EVENTS_QUILOMB_LVL1;
    public Transform ELEVATOR_SHIP_LVL1;
    public Transform SPIKES_LVL1;
    public GameObject BTTON_QUILOMB_LVL1;
    public GameObject NPCS_LVL1;

    
    private void Awake()
    {
        INSTANCE = this;
        VFX_FADE.SetActive(true);
        if (LEVEL == 4)
        {
            CANVAS = transform.GetChild(2).gameObject.GetComponent<ButtonDialogue>();
        }
        else
        {
            CANVAS = transform.GetChild(1).gameObject.GetComponent<ButtonDialogue>();
        }

        

        switch (GameManager.INSTANCE.LEVEL)
        {
            case 1:
                lvlDiag = JsonUtility.FromJson<ButtonDialogue.Zone>(LoadJson.LVL1_DIALOGUES);
                break;
            case 2:
                lvlDiag = JsonUtility.FromJson<ButtonDialogue.Zone>(LoadJson.LVL2);
                break;
            case 3:
                lvlDiag = JsonUtility.FromJson<ButtonDialogue.Zone>(LoadJson.LVL3);
                break;
            case 4:
                lvlDiag = JsonUtility.FromJson<ButtonDialogue.Zone>(LoadJson.LVL_SELECT);
                break;
            default:
                break;
        }

        try
        {
            _playerScript = PLAYER.GetComponent<Player>();
        }
        catch (System.Exception e)
        {
            print("Oh no" + e);
        }
    }

    private void Start()
    {
        ActivateCursor(false);
        
    }

    public void SaveGame(int level)
    {
        LEVEL = level;
        DataPersistentManager.INSTANCE.SaveGame();
    }

    public void GetPlayer()
    {
        PLAYER = GameObject.FindWithTag("Player");
    }
    
    void Update()
    {
        if(LoadJson.DEBUG_MODE)
        {
            DebugLowLevels();
            DebugDeleteSaveData();
            DebugReset();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !_escapePressed)
        {
            PauseUnPause(PAUSED);
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            _escapePressed = false;
        } 
    }

    public void PauseUnPause(bool bl)
    {
        if (bl)
        {
            ActivateCursor(false);
            PAUSE_MENU.SetActive(false);
            PAUSE_MENU.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            ChangeTimeScale(1f);
        }
        else
        {
            ActivateCursor(true);
            PAUSE_MENU.SetActive(true);
            ChangeTimeScale(0f);
        }
        PAUSED = !bl;
    }

    public void ChangeTimeScale(float n)
    {
        Time.timeScale = n;
    }

    public void setQuilombo()
    {
        QUILOMB_MODE = true;
    }
    private void DebugLowLevels()
    {
        if (Input.GetKeyDown("1"))
        {
            ScenesManager.Instance.LoadNextScene("Tutorial");
        }

        if (Input.GetKeyDown("2"))
        {
            ScenesManager.Instance.LoadNextScene("Lvl2_Radar");
        }

        if (Input.GetKeyDown("3"))
        {
            ScenesManager.Instance.LoadNextScene("NewLevel3");
        }
    }

    private void DebugDeleteSaveData()
    {
        if (Input.GetKeyDown("5"))
        {
            DataPersistentManager.INSTANCE.DeleteSave();
        }
    }

    private void DebugReset()
    {
        if (Input.GetAxis("Debug Reset") > 0)
        {
            restartTime += Time.deltaTime;
            if (restartTime >= 2f)
            {
                ScenesManager.Instance.ReloadScene();
            }
        }
        else
        {
            restartTime = 0f;
        }
    }
    void IDataPersistance.LoadData(GameData data)
    {
        MUSIC_VOLUME = data.MUSIC_VOLUME;
        SFX_VOLUME = data.SFX_VOLUME;

        if (QUILOMB_MODE)
        {
            return;
        }

        if (LEVEL == 0)
        {
            LEVEL = data.LEVEL;
            return;
        }

        if (LEVEL == 1)
        {
            _playerScript.SetMaxHP(data.PLAYER_MAX_HP);
            _playerScript.SetHP(data.PLAYER_ACTUAL_HP);
            PLAYER.transform.localPosition = data.PLAYER_POSITION;
            PLAYER_COMBAT = data.PLAYER_COMBAT;
            PLAYER.GetComponent<PlayerCombat>().enabled = data.PLAYER_COMBAT;
            PLAYER.GetComponent<SpriteRenderer>().flipX = data.PLAYER_FLIP_X;
            PANEL_OBJECTIVE.transform.GetChild(0).GetComponent<ObjectivesManager>().ChangeZoneName(data.OBJECTIVE);
        }

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
                    else
                    {
                        TUTO_TRIG_LVL1.transform.GetChild(i).gameObject.SetActive(true);
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

                if (!data.DIALOGUES_LVL1[0])
                {
                    PANEL_OBJECTIVE.SetActive(true);
                }

                for (int i = 0; i < data.CHANGE_MUSIC_LVL1.Length; i++)
                {
                    if (!data.CHANGE_MUSIC_LVL1[i])
                    {
                        CHANGE_MUSIC_LVL1.transform.GetChild(i).gameObject.SetActive(true);
                        CHANGE_MUSIC_LVL1.transform.GetChild(i).gameObject.BroadcastMessage("manualDo", SendMessageOptions.DontRequireReceiver);
                    }
                }

                for (int i = 0; i < data.EVENTS_GO_HERE_LVL1.Length; i++)
                {
                    if (!data.EVENTS_GO_HERE_LVL1[i])
                    {
                        EVENTS_GO_HERE_LVL1.transform.GetChild(i).gameObject.SetActive(false);
                    }
                    else
                    {
                        EVENTS_GO_HERE_LVL1.transform.GetChild(i).gameObject.SetActive(true);
                    }
                }

                if (!data.EVENTS_GO_HERE_LVL1[2])
                {
                    DOORS_LVL1.transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
                    DOORS_LVL1.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                }

                if (!data.EVENTS_GO_HERE_LVL1[8])
                {
                    DOORS_LVL1.transform.GetChild(1).GetComponent<BoxCollider2D>().enabled = false;
                    DOORS_LVL1.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
                }

                INVISIBLE_TROLL_LVL1.SetActive(data.INVISIBLE_TROLL_LVL1);

                for (int i = 0; i < data.ELEVATORS_LVL1.Length; i++)
                {
                    ELEVATORS_LVL1[i].localPosition = data.ELEVATORS_LVL1[i];
                }

                if (!data.ELEVATOR_DOOR_LVL1)
                {
                    ELEVATOR_DOOR_LVL1.SetActive(false);
                }
                else
                {
                    ELEVATOR_DOOR_LVL1.SetActive(true);
                }

                for (int i = 0; i < data.DUMMIES_LVL1.Length; i++)
                {
                    if (!data.DUMMIES_LVL1[i])
                    {
                        DUMMIES_LVL1[i].SetActive(false);
                    }
                    else
                    {
                        DUMMIES_LVL1[i].SetActive(true);
                        if (data.DUMMIES_LAYER_LVL1[i] == 7)
                        {
                            DUMMIES_LVL1[i].GetComponent<ChangeLayers>().EnemyLayer();
                        }
                        else
                        {
                            DUMMIES_LVL1[i].GetComponent<ChangeLayers>().DefaultLayer();
                        }
                    }
                }

                NPCS_LVL1.SetActive(data.NPCS_LVL1);

                PILOT_QUILOMB_LVL1.SetActive(data.PILOT_QUILOMB_LVL1);
                if (data.PILOT_QUILOMB_LVL1 && !data.BTTON_QUILOMB_LVL1)
                {
                    PILOT_QUILOMB_LVL1.GetComponent<Enemy>().TakeDamage(40);
                    ELEVATOR_SHIP_LVL1.localPosition = data.ELEVATOR_SHIP_LVL1;
                }

                EVENTS_QUILOMB_LVL1.SetActive(data.EVENTS_QUILOMB_LVL1);
                if (data.EVENTS_QUILOMB_LVL1)
                {
                    for (int i = 1; i < data.EVENTS_ALL_QUILOMB_LVL1.Length; i++)
                    {
                        if (!data.EVENTS_ALL_QUILOMB_LVL1[i])
                        {
                            if (i == 1)
                            {
                                SPIKES_LVL1.localPosition = data.SPIKES_LVL1;
                            }
                            EVENTS_QUILOMB_LVL1.transform.GetChild(i).gameObject.SetActive(false);
                        }
                        else
                        {
                            EVENTS_QUILOMB_LVL1.transform.GetChild(i).gameObject.SetActive(true);
                        }
                    }
                }

                break;

            default:
                break;
        }
    }

    void IDataPersistance.SaveData(GameData data)
    {
        data.LEVEL = LEVEL;
        data.MUSIC_VOLUME = MUSIC_VOLUME;
        data.SFX_VOLUME = SFX_VOLUME;

        if (QUILOMB_MODE)
        {
            return;
        }

        if (LEVEL == 1)
        {
            data.PLAYER_MAX_HP = _playerScript.GetMaxHP();
            data.PLAYER_ACTUAL_HP = _playerScript.GetHP();
            data.PLAYER_POSITION = CHECKPOINT;
            data.PLAYER_FLIP_X = PLAYER.GetComponent<SpriteRenderer>().flipX;
            data.PLAYER_COMBAT = PLAYER.GetComponent<PlayerCombat>().enabled;
            data.OBJECTIVE = PANEL_OBJECTIVE.transform.GetChild(0).GetComponent<ObjectivesManager>().GetZoneName();
        }

        switch (LEVEL)
        {
            //MENU
            case 0: //MENU
            //MENU
                data.PLAYER_POSITION = new Vector3(1.21f, 13.87508f, 0f);
                data.PLAYER_MAX_HP = 5;
                data.PLAYER_ACTUAL_HP = 5;
                data.OBJECTIVE = "objective01_explore";
                data.LEVEL = 1;
                data.PLAYER_FLIP_X = false;
                data.PLAYER_COMBAT = false;
                data.CANVAS_WS_LVL1_GENERAL = new bool[5] { true, true, true, true, true };
                data.CANVAS_WS_LVL1_ACTIVATE_EVENTS = new bool[5] { false, false, false, false, false };
                data.TUTO_TRIG_LVL1 = new bool[4] { true, true, true, false };
                data.CHECKPOINTS_LVL1 = new bool[8] { true, true, true, true, true, true, true, true };
                data.DIALOGUES_LVL1 = new bool[15] { true, true, true, true, true, true, true, true, true, true, true, true, true, true, false };
                data.CHANGE_MUSIC_LVL1 = new bool[2] { false, true };
                data.EVENTS_GO_HERE_LVL1 = new bool[13] { false, true, true, true, false, false, true, false, true, true, true, false, false };
                data.INVISIBLE_TROLL_LVL1 = true;
                data.ELEVATORS_LVL1 = new Vector3[3]{ new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f) };
                data.ELEVATOR_DOOR_LVL1 = true;
                data.DUMMIES_LVL1 = new bool[3] { false, false, false };
                data.DUMMIES_LAYER_LVL1 = new int[3] { 7, 7, 7 };
                data.PILOT_QUILOMB_LVL1 = false;
                data.EVENTS_QUILOMB_LVL1 = false;
                data.EVENTS_ALL_QUILOMB_LVL1 = new bool[14] { true, true, true, true, true, true, true, true, true, true, true, true, true, true };
                data.ELEVATOR_SHIP_LVL1 = new Vector3(0f, 0f, 0f);
                data.SPIKES_LVL1 = new Vector3(0f, 0.32f, 0f);
                data.BTTON_QUILOMB_LVL1 = true;
                data.NPCS_LVL1 = true;
                break;
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
                    else
                    {
                        data.TUTO_TRIG_LVL1[i] = true;
                    }

                }

                for (int i = 0; i < CHECKPOINTS_LVL1.transform.childCount; i++)
                {
                    if (!CHECKPOINTS_LVL1.transform.GetChild(i).gameObject.activeSelf)
                    {
                        data.CHECKPOINTS_LVL1[i] = false;
                    }
                    else
                    {
                        data.CHECKPOINTS_LVL1[i] = true;
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

                for (int i = 0; i < CHANGE_MUSIC_LVL1.transform.childCount; i++)
                {
                    if (!CHANGE_MUSIC_LVL1.transform.GetChild(i).gameObject.activeSelf)
                    {
                        data.CHANGE_MUSIC_LVL1[i] = false;
                    }
                    else
                    {
                        data.CHANGE_MUSIC_LVL1[i] = true;
                    }
                }

                for (int i = 0; i < EVENTS_GO_HERE_LVL1.transform.childCount; i++)
                {
                    if (!EVENTS_GO_HERE_LVL1.transform.GetChild(i).gameObject.activeSelf)
                    {
                        data.EVENTS_GO_HERE_LVL1[i] = false;
                    }
                    else
                    {
                        data.EVENTS_GO_HERE_LVL1[i] = true;
                    }
                }

                data.INVISIBLE_TROLL_LVL1 = INVISIBLE_TROLL_LVL1.activeSelf;

                for (int i = 0; i < ELEVATORS_LVL1.Length; i++)
                {
                    data.ELEVATORS_LVL1[i] = ELEVATORS_LVL1[i].localPosition;
                }

                data.ELEVATOR_DOOR_LVL1 = ELEVATOR_DOOR_LVL1.activeSelf;

                for (int i = 0; i < DUMMIES_LVL1.Length; i++)
                {
                    if (!DUMMIES_LVL1[i].activeSelf)
                    {
                        data.DUMMIES_LVL1[i] = false;
                    }
                    else
                    {
                        data.DUMMIES_LVL1[i] = true;
                        data.DUMMIES_LAYER_LVL1[i] = DUMMIES_LVL1[i].layer;
                    }
                }
                
                data.NPCS_LVL1 = NPCS_LVL1.activeSelf;

                data.PILOT_QUILOMB_LVL1 = PILOT_QUILOMB_LVL1.activeSelf;
                if (data.PILOT_QUILOMB_LVL1)
                {
                    data.BTTON_QUILOMB_LVL1 = BTTON_QUILOMB_LVL1.activeSelf;
                }

                data.EVENTS_QUILOMB_LVL1 = EVENTS_QUILOMB_LVL1.activeSelf;
                if (EVENTS_QUILOMB_LVL1.activeSelf)
                {
                    for (int i = 1; i < EVENTS_QUILOMB_LVL1.transform.childCount; i++)
                    {
                        if (!EVENTS_QUILOMB_LVL1.transform.GetChild(i).gameObject.activeSelf)
                        {
                            data.EVENTS_ALL_QUILOMB_LVL1[i] = false;
                        }
                        else
                        {
                            data.EVENTS_ALL_QUILOMB_LVL1[i] = true;
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

    public void ActivatePlayerCombat()
    {
        PLAYER_COMBAT = true;
    }

    public void ActivateCursor(bool bl)
    {
        //Cursor.visible = bl;
    }


    public void AllMovementToggle(bool a)
    {
        PLAYER_COMBAT = a;
        _playerScript.GetComponent<PlayerCombat>().enabled = a;
        _playerScript.MovementEnableToggle(a);
    }
    public void TellThePlayerToMoveSomewhere(Transform a)
    {
        _playerScript.setDestination(a.position.x,a.position.y);
    }

    public void IsSkippable(bool toggle)
    {
        if(DIALOGUESKIPEND && !toggle)
        {
            DIALOGUESKIPCOUNT++;
        }
        DIALOGUESKIPEND = toggle;
        
    } 

}
