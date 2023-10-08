using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button _continue;

    private void Start()
    {
        if (!DataPersistentManager.INSTANCE.HasGameData())
        {
            _continue.interactable = false;
        }
    }

    public void NewGameClicked()
    {
        DataPersistentManager.INSTANCE.NewGame();
        ScenesManager.Instance.LoadNewGame();
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
