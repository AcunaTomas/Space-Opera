using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeLogic : MonoBehaviour
{
    [SerializeField] GameObject upgradeW1;
    [SerializeField] GameObject upgradeW2;
    [SerializeField] Image fade;
    [SerializeField] float _showDelay = 1f; //Cuando tarda en mostrarlo
    [SerializeField] float _fadeDelay = 1f; //cuanto tarda el fade
    [SerializeField] Animator transition;
    float time;

    bool _startCounting;
    void Update()
    {
        if(gameObject.activeSelf && !upgradeW1.activeSelf && !upgradeW2.activeSelf) Reboot();
        if(_startCounting) time += Time.unscaledDeltaTime;
        if(upgradeW1.activeSelf)
        {
            //if(fade.color.a < .4f) fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, fade.color.a +.1f);
            if (!transition.GetCurrentAnimatorStateInfo(0).IsName("PopUp_star2"))
            {
                transition.SetTrigger("PopUpStart");
            }
            
            upgradeW1.transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, time / _fadeDelay);
            upgradeW1.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alpha += Time.unscaledDeltaTime / _fadeDelay;
            upgradeW1.transform.GetChild(2).GetComponent<Image>().color = new Color(255, 255, 255, time/ _fadeDelay);
            
            //idioma xd
            if(DataPersistentManager.INSTANCE != null)
            {
                switch (DataPersistentManager.INSTANCE.Language())
                {
                    case "ES":
                    upgradeW1.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "NUEVA HABILIDAD DESBLOQUEADA";
                    break;
                    case "EN":
                    upgradeW1.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "NEW SKILL UNLOCKED";
                    break;
                    default:
                    break;
                }
            }
            if(time > _showDelay)
            {
                upgradeW1.transform.GetChild(3).GetComponent<TextMeshProUGUI>().alpha += Time.unscaledDeltaTime / _fadeDelay;
                upgradeW1.transform.GetChild(4).GetComponent<TextMeshProUGUI>().alpha += Time.unscaledDeltaTime / _fadeDelay;
            }
            if(time > _showDelay * 1.5f) upgradeW1.transform.GetChild(5).GetComponent<Image>().color = new Color(255, 255, 255, time/ _fadeDelay);
            
        }
        if(upgradeW2.activeSelf && time > _showDelay)
        {
            //upgradeW2.transform.GetChild(2).GetComponent<TextMeshProUGUI>().alpha += Time.unscaledDeltaTime / _fadeDelay;
            upgradeW2.transform.GetChild(3).GetComponent<TextMeshProUGUI>().alpha += Time.unscaledDeltaTime / _fadeDelay;
        }
        if(Input.GetButtonDown("Submit"))
        {
            if(upgradeW1.activeSelf)
            {
                if(time > _showDelay)
                {
                    time = 0;
                    SetW1(0f);//reset
                    upgradeW1.SetActive(false);
                    upgradeW2.SetActive(true);
                }
                else
                {
                    SetW1(1f);
                }
                
            }
            else
            {
                upgradeW2.SetActive(false);
                GameManager.INSTANCE.ChangeTimeScale(1);
                _startCounting = false;
                //fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, 0f);
                gameObject.SetActive(false);
                //transition.enabled = !transition.enabled;
                transition.SetTrigger("PopUpEnd");
            }
        }
    }

    void Reboot()
    {
        //fade.SetActive(true);
        SetW1(0f);//reset
        transition.enabled = !transition.enabled;
        GameManager.INSTANCE.ChangeTimeScale(0);
        upgradeW1.SetActive(true);
        _startCounting = true;
        time = 0;
        //transition.SetBool("SceneStart", false);
    }

    void SetW1(float value)
    {
        upgradeW1.transform.GetChild(0).GetComponent<Image>().color = new Color(255, 255, 255, value);
        upgradeW1.transform.GetChild(1).GetComponent<TextMeshProUGUI>().alpha = value;
        upgradeW1.transform.GetChild(2).GetComponent<Image>().color = new Color(255, 255, 255, value);
        upgradeW1.transform.GetChild(3).GetComponent<TextMeshProUGUI>().alpha = value;
        upgradeW1.transform.GetChild(4).GetComponent<TextMeshProUGUI>().alpha = value;
        upgradeW1.transform.GetChild(5).GetComponent<Image>().color = new Color(255, 255, 255, value);
    }
}
