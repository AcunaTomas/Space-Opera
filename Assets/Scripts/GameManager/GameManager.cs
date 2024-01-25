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

    //LEVEL 1
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
        CANVAS = transform.GetChild(1).gameObject.GetComponent<ButtonDialogue>();
        try
        {
            _playerScript = PLAYER.GetComponent<Player>();
        }
        catch (System.Exception e)
        {
            print(e);
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
        if (ScenesManager.Instance.GetSceneCurrentName() == "SceneMainMenu")
        {
            LEVEL = data.LEVEL;
            return;

        }    


        _playerScript.SetMaxHP(data.PLAYER_MAX_HP);
        _playerScript.SetHP(data.PLAYER_ACTUAL_HP);
        PLAYER.transform.localPosition = data.PLAYER_POSITION;
        PLAYER_COMBAT = data.PLAYER_COMBAT;
        PLAYER.GetComponent<PlayerCombat>().enabled = data.PLAYER_COMBAT;
        PLAYER.GetComponent<SpriteRenderer>().flipX = data.PLAYER_FLIP_X;
        PANEL_OBJECTIVE.transform.GetChild(0).GetComponent<ObjectivesManager>().ChangeZoneName(data.OBJECTIVE);




        
    }

    void IDataPersistance.SaveData(GameData data)
    {
        if (ScenesManager.Instance.GetSceneCurrentName() == "SceneMainMenu")
        {
            data.LEVEL = 1;
            return;

        }
        data.LEVEL = LEVEL;
        data.PLAYER_MAX_HP = _playerScript.GetMaxHP();
        data.PLAYER_ACTUAL_HP = _playerScript.GetHP();
        data.PLAYER_POSITION = CHECKPOINT;
        data.PLAYER_FLIP_X = PLAYER.GetComponent<SpriteRenderer>().flipX;
        data.PLAYER_COMBAT = PLAYER.GetComponent<PlayerCombat>().enabled;
        data.OBJECTIVE = PANEL_OBJECTIVE.transform.GetChild(0).GetComponent<ObjectivesManager>().GetZoneName();
        data.SavedEvents = DataPersistentManager.INSTANCE.SavedEvents.ToArray();
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
