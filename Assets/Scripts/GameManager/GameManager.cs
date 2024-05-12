using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{


    public string ALTSKIPENABLED = "Disabled";
    public static GameManager INSTANCE;
    public int LEVEL;
    public GameObject PLAYER;
    public Vector3 CHECKPOINT;
    public GameObject ACTUAL_CHECKPOINT;
    public bool PLAYER_COMBAT = false;
    public bool PAUSED = false;
    public float MUSIC_VOLUME;
    public float SFX_VOLUME;
    public  UIManager UIManager;
    public SoundManager SoundManager;
    public CameraController CameraController;

    private Player _playerScript;
    private bool _escapePressed = false;
    private float restartTime;
    public float dustcap = 0;
    public TextAsset lvlDialog;
    //public ButtonDialogue.Zone lvlDiag;
    public bool DIALOGUESKIPEND = false;
    public int DIALOGUESKIPCOUNT;

    public GameObject DebugInfo;

    //Later is now...
    
    private void Awake()
    {
        INSTANCE = this;
        CameraController = gameObject.GetComponentInChildren<CameraController>();

        switch (LEVEL)
        {
            case 999:
                    //lvlDiag = JsonUtility.FromJson<ButtonDialogue.Zone>(LoadCSV.LVL999);
                lvlDialog = LoadCSV.LVL999;
                break;
            /*
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
            case 999:
                    //lvlDiag = JsonUtility.FromJson<ButtonDialogue.Zone>(LoadCSV.LVL999);
                ButtonDialogueNew.SetLvlDialog(LoadCSV.LVL999);
                break;
            */
            default:
                    //lvlDiag = Boss(LoadCSV.LVL999);
                //ButtonDialogueNew.SetLvlDialog(LoadCSV.LVL999);
                break;
        }
        //print(lvlDiag.DIALOGUES);
        try
        {
            _playerScript = GetActivePlayer();
        }
        catch (System.Exception e)
        {
            print("Oh no \n" + e);
        }
    }

    private void Start()
    {
        UIManager = UIManager.INSTANCE;
        SoundManager = SoundManager.INSTANCE;
        ActivateCursor(false);
        CameraController.LookAtPlayer();

    }

    public void SaveGame(int level)
    {
        LEVEL = level;
        DataPersistentManager.INSTANCE.SaveGame();
    }

    public Player GetActivePlayer()
    {
        if (PLAYER.transform.GetChild(0).gameObject.activeSelf)
        {
            return PLAYER.transform.GetChild(0).GetComponent<Player>();
        }
        return PLAYER.transform.GetChild(1).GetComponent<Player>();
    }
    
    void Update()
    {
        if(LoadJson.DEBUG_MODE)
        {
            DebugLowLevels();
            DebugDeleteSaveData();
            DebugReset();
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                DebugInfo.SetActive(!DebugInfo.activeSelf);
            }
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
            ActivateCursor(bl);
            ChangeTimeScale(System.Convert.ToSingle(bl));

        PAUSED = !bl;
    }

    public void ChangeTimeScale(float n)
    {
        Time.timeScale = n;
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

        // if (Input.GetKeyDown("3"))
        // {
        //     ScenesManager.Instance.LoadNextScene("NewLevel3");
        // }
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
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else
        {
            restartTime = 0f;
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

    public void TogglePlayerCombat(bool a)
    {
        PLAYER_COMBAT = a;
    }
    public void DeactivatePlayerCombat()
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

    public void SetFps(int fps)
    {
        Application.targetFrameRate = fps;
    }


}
