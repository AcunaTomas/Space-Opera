using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    void OnEnable()
    {
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);
        if(UIManager.INSTANCE.CURRENTPLAYER == "Brody")
            {
                gameObject.GetComponent<Animator>().SetBool("isBrody", true);
                gameObject.GetComponent<Animator>().SetBool("isBito", false);
                gameObject.GetComponent<Animator>().Play("OpenBrody");
            }
            else
            {
                gameObject.GetComponent<Animator>().SetBool("isBito", true);
                gameObject.GetComponent<Animator>().SetBool("isBrody", false);
                gameObject.GetComponent<Animator>().Play("OpenBito");       
            }
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
    void Update()
    {
        if (InputManager.INSTANCE.CloseMenuReleased)
        {
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
        if(InputManager.INSTANCE.SubmitInput)
        {
            Debug.Log("Estoy cómo loquita");
        }
    }

    void OnDisable()
    {
        Debug.Log("Cerrando menú");
        gameObject.GetComponent<Image>().sprite = null;
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
        Debug.Log("Me funaron");
    }

}
