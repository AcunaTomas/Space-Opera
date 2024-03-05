using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager INSTANCE;
    public ButtonDialogue DIALOGUE_PANEL;
    public ObjectivesManager OBJECTIVES_DISPLAY;
    public AchievementsManager ACHIEVEMENTS_POPUP;
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


}
