using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void ChangeLanguage(string newLanguage = "ES")
    {
                //DataPersistentManager.INSTANCE.SetLanguage(newLanguage);
        
        if(DataPersistentManager.INSTANCE.Language() == "ES" ) DataPersistentManager.INSTANCE.SetLanguage("EN");

        else DataPersistentManager.INSTANCE.SetLanguage("ES");
        AudioController.instance.PlayOneShot(FMODEvents.instance.sfx_ui_select, this.transform.position);
        UIManager.INSTANCE.UpdateLanguage();
        if(DataPersistentManager.INSTANCE.Language() == "ES")
        {
            transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Continuar";
            transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Idioma";
            transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Salir";
        }
        else
        {
            transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Continue";
            transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Language";
            transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Exit";
        }
    }

    void Start()
    {
        if(DataPersistentManager.INSTANCE.Language() == "ES")
        {
            transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Continuar";
            transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Idioma";
            transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Salir";
        }
        else
        {
            transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Continue";
            transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Language";
            transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Exit";
        }
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            transform.GetChild(0).gameObject.SetActive(false);
        } 
    }
}
