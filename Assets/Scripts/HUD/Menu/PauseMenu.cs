using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] GameObject[] pauseFirstSelected;
    [SerializeField] GameObject[] unpauseFirstSelected;
    [SerializeField] GameObject[] ControllersMenu;
    [SerializeField] GameObject[] controllerFirstSelected;
    
    public void ChangeLanguage(string newLanguage = "ES")
    {
                //DataPersistentManager.INSTANCE.SetLanguage(newLanguage);
        if(DataPersistentManager.INSTANCE == null) return;
        if(DataPersistentManager.INSTANCE.Language() == "ES" ) DataPersistentManager.INSTANCE.SetLanguage("EN");

        else DataPersistentManager.INSTANCE.SetLanguage("ES");
        AudioController.instance.PlayOneShot(FMODEvents.instance.sfx_ui_select, this.transform.position);
        UIManager.INSTANCE.UpdateLanguage();
        if(DataPersistentManager.INSTANCE.Language() == "ES")
        {
            transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Continuar";
            transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Controles";
            transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Idioma";
            transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Salir";
            transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Continuar";
            transform.GetChild(2).transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Controles";
            transform.GetChild(2).transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Idioma";
            transform.GetChild(2).transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Salir";
        }
        else
        {
            transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Continue";
            transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Controller";
            transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Language";
            transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Exit";
            transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Continue";
            transform.GetChild(2).transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Controller";
            transform.GetChild(2).transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Language";
            transform.GetChild(2).transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Exit";
        }
    }

    public void OpenController()
    {
        EventSystem.current.SetSelectedGameObject(null);
        if(UIManager.INSTANCE.CURRENTPLAYER == "Brody")
        {
            EventSystem.current.SetSelectedGameObject(controllerFirstSelected[0]);
            ControllersMenu[0].SetActive(true);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(controllerFirstSelected[1]);
            ControllersMenu[1].SetActive(true);
        }
        

    }
    public void CloseController()
    {
        EventSystem.current.SetSelectedGameObject(null);
        if(UIManager.INSTANCE.CURRENTPLAYER == "Brody")
        {
            EventSystem.current.SetSelectedGameObject(pauseFirstSelected[0]);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(pauseFirstSelected[1]);
        }
        ControllersMenu[0].SetActive(false);
        ControllersMenu[1].SetActive(false);
    }

    void Start()
    {
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        if(UIManager.INSTANCE.CURRENTPLAYER == "Brody")
        {
            EventSystem.current.SetSelectedGameObject(unpauseFirstSelected[0]);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(unpauseFirstSelected[1]);
        }
        if(DataPersistentManager.INSTANCE == null) return;
        if(DataPersistentManager.INSTANCE.Language() == "ES")
        {
            transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Continuar";
            transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Controles";
            transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Idioma";
            transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Salir";
            transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Continuar";
            transform.GetChild(2).transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Controles";
            transform.GetChild(2).transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Idioma";
            transform.GetChild(2).transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Salir";
        }
        else
        {
            transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Continue";
            transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Controller";
            transform.GetChild(0).transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Language";
            transform.GetChild(0).transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Exit";
            transform.GetChild(2).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Continue";
            transform.GetChild(2).transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Controller";
            transform.GetChild(2).transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Language";
            transform.GetChild(2).transform.GetChild(3).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Exit";
        }
    }
    void Update()
    {
        if (Input.GetButton("Pause"))
        {
            if(UIManager.INSTANCE.CURRENTPLAYER == "Brody")
            {
                gameObject.GetComponent<Animator>().SetBool("isBrody", true);
                gameObject.GetComponent<Animator>().SetBool("isBito", false);
            }
            else
            {
                gameObject.GetComponent<Animator>().SetBool("isBito", true);
                gameObject.GetComponent<Animator>().SetBool("isBrody", false);       
            }
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
            if(UIManager.INSTANCE.CURRENTPLAYER == "Brody")
            {
                EventSystem.current.SetSelectedGameObject(unpauseFirstSelected[0]);
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(unpauseFirstSelected[1]);
            }
        }
        if(UIManager.INSTANCE.CURRENTPLAYER == "Brody")
        {
            gameObject.GetComponent<Animator>().SetBool("isBrody", true);
            gameObject.GetComponent<Animator>().SetBool("isBito", false);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("isBito", true);
            gameObject.GetComponent<Animator>().SetBool("isBrody", false);       
        }
    }

}
