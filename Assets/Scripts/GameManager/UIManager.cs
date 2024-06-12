using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager INSTANCE;
    public ButtonDialogueNew DIALOGUE_PANEL;
    public ObjectivesManager OBJECTIVES_DISPLAY;
    public AchievementsManager ACHIEVEMENTS_POPUP;
    public GameObject UPGRADE;
    public GameObject PAUSE_MENU;
    public GameObject VFX_FADE;
    public UpdateBars LifeBar;


    void Awake()
    {
        if (INSTANCE != null)
        {
            Destroy(gameObject);
            return;
        }
        INSTANCE = this;
    }


    public void OpenDialogueBox(string TextID = "default")
    {
        GameManager.INSTANCE.AllMovementToggle(false);
        DIALOGUE_PANEL.ZONENAME = TextID;
        DIALOGUE_PANEL.gameObject.SetActive(true);
        DIALOGUE_PANEL.FirstDialogue(CollisionDialogue.ChangeAudio.dialogo);
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

}
