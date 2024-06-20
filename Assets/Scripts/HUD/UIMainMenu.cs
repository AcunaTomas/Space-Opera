using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    private void Update()
    {
        switch (DataPersistentManager.INSTANCE.Language())
        {
            case "ES":
            gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Jugar";
            gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Idioma";
            gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Salir";
            break;
            case "EN":
            gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Start";
            gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Language";
            gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Exit";
            break;
            default:
            break;
        }
        if (LoadJson.DEBUG_MODE)
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
    }

    public void ExitGame()
    {
        #if UNITY_STANDALONE
            AudioController.instance.PlayOneShot(FMODEvents.instance.sfx_ui_close, this.transform.position);
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

}
