using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button _continue;
    [SerializeField]
    private Button _newGame;
    [SerializeField]
    private EventSystem _eventSystem;

    private void Start()
    {
/*         if (!DataPersistentManager.INSTANCE.HasGameData())
        {
            _continue.interactable = false;
        }
        else
        {
            _eventSystem.firstSelectedGameObject = _newGame.gameObject;
            _newGame.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
            _newGame.gameObject.GetComponent<ButtonMenu>().ACTIVE = true;
            //_newGame.gameObject.GetComponent<ButtonMenu>().ACTIVE = false;
        } */
    }

    public void NewGameClicked()
    {
        //GameManager.INSTANCE.LEVEL = 0;
        //DataPersistentManager.INSTANCE.NewGame();
        //DataPersistentManager.INSTANCE.SaveGame();
        ScenesManager.Instance.LoadNewGame();
        
        AudioController.instance.PlayOneShot(FMODEvents.instance.sfx_ui_select, this.transform.position);
    }

    public void ChangeLanguage(string newLanguage = "ES")
    {
                //DataPersistentManager.INSTANCE.SetLanguage(newLanguage);
        
        if(DataPersistentManager.INSTANCE.Language() == "ES" ) DataPersistentManager.INSTANCE.SetLanguage("EN");

        else DataPersistentManager.INSTANCE.SetLanguage("ES");
        if(UIManager.INSTANCE != null) UIManager.INSTANCE.UpdateLanguage();
        AudioController.instance.PlayOneShot(FMODEvents.instance.sfx_ui_select, this.transform.position);
        
    }
    public void ContinueClicked()
    {
        //DataPersistentManager.INSTANCE.SaveGame();

        switch (GameManager.INSTANCE.LEVEL)
        {
            case 1:
                ScenesManager.Instance.LoadNextScene("Tutorial");
                break;
            case 2:
                ScenesManager.Instance.LoadNextScene("Lvl2_Radar");
                break;
            case 3:
                ScenesManager.Instance.LoadNextScene("NewLevel3");
                break;
            default:
                break;
        }
    }
}
