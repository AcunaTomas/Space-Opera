using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager INSTANCE;
    public ButtonDialogueNew DIALOGUE_PANEL;
    public GameObject CANVASWORLDSPACE;
    public ObjectivesManager OBJECTIVES_DISPLAY;
    public AchievementsManager ACHIEVEMENTS_POPUP;
    public GameObject UPGRADE;
    public GameObject PAUSE_MENU;
    public GameObject VFX_FADE;
    public UpdateBars LifeBar;

    public UpdateBars LifeBar2;
    public string CURRENTPLAYER;
    [SerializeField]bool _moveAfterDialogue;

    void Awake()
    {
        if (INSTANCE != null)
        {
            Destroy(gameObject);
            return;
        }
        INSTANCE = this;

        transform.GetChild(6).SetSiblingIndex(1);

    }

    void Start()
    {
        ToggleHPBars();
    }
    public void OpenDialogueBox(string TextID = "default")
    {
        GameManager.INSTANCE.AllMovementToggle(false);
        DIALOGUE_PANEL.ZONENAME = TextID;
        DIALOGUE_PANEL.gameObject.SetActive(true);
        DIALOGUE_PANEL.FirstDialogue(CollisionDialogue.ChangeAudio.dialogo, _moveAfterDialogue);
    }
    public void MoveAfterDialogue(bool yorn = true) //do before
    {
        _moveAfterDialogue = yorn;
    }
    public bool DialogueBoxStatus()
    {
        return DIALOGUE_PANEL.gameObject.activeSelf;
    }

    public string DialogueBoxLoadText(string zoneName)
    {
        return DIALOGUE_PANEL.AddText(zoneName);
    }
    
    public void UpdateObjectiveText(string a)
    {
        OBJECTIVES_DISPLAY.ChangeZoneName(a);
        OBJECTIVES_DISPLAY.ChangeObjectiveBool();
    }

    public void ToggleHP(bool a) //Lmao this goes unused
    {
        if (a)
        {
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("Appear");
            return;
        }
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("Disappear");
        
    }
    
    public void UpdateLanguage()
    {
        //DIALOGUE_TUTORIAL.UpdateLanguage();
        DIALOGUE_PANEL.UpdateLanguage();
        OBJECTIVES_DISPLAY.UpdateLanguage();
        CANVASWORLDSPACE.BroadcastMessage("UpdateLanguage",SendMessageOptions.DontRequireReceiver);
    }

    public void ToggleHPBars() //HardCoded piece of shit why do i have to do this oh my god
    {
        if(GameManager.INSTANCE.GetActivePlayer()._playerType == Player.PlayerType.Bito)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            LifeBar2._energy = LifeBar._energy;
            return;
        }
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
        LifeBar._energy = LifeBar2._energy;
    }
}
