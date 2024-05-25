using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSwitcherLogic : MonoBehaviour
{
    
    public Player bi2;
    public Player brodi;
    public bool _change = false;

    private string who = "Bito";

    private bool cooldown = false;

    bool enabled = true;

    private Object effect; 

    private float effectDuration = 0.4f;
    void Start()
    {
        effect = Resources.Load("Prefabs/Sparkle");
    }
    void Update()
    {
        if (enabled)
        {
            if (Input.GetAxis("CharSwitch1") > 0 && cooldown == false)
                {
                    _change = false;

                    switch (who)
                    {
                        case "Bito":
                            {
                            
                                bi2.gameObject.SetActive(false);
                                brodi.gameObject.SetActive(true);
                                brodi.gameObject.transform.position = bi2.gameObject.transform.position;
                                who = "Brody";
                                StartCoroutine(cooldown2());
                                var a = Instantiate(effect) as GameObject;
                                a.transform.position = brodi.gameObject.transform.position;
                                Destroy(a,effectDuration);
                                break;
                            }
                        case "Brody":
                            {

                                brodi.gameObject.SetActive(false);
                                bi2.gameObject.SetActive(true);
                                bi2.gameObject.transform.position = brodi.gameObject.transform.position;
                                who = "Bito";
                                StartCoroutine(cooldown2());
                                var a = Instantiate(effect) as GameObject;
                                a.transform.position = bi2.gameObject.transform.position;
                                Destroy(a,effectDuration);
                                break;
                            }
                    }
                }
                if (who == "Brody")
                {
                    bi2.gameObject.transform.position = brodi.gameObject.transform.position;
                }
                else
                {
                    brodi.gameObject.transform.position = bi2.gameObject.transform.position;
                }
        }
   

    }

    public void ChangePlayer()
    {
        _change = true;
    }


    public void ToggleCharSwitching(bool a, float time=0f)
    {
        if (time == 0f)
        {
            enabled = a;
            return;
        }
        
    }


    IEnumerator cooldown2()
    {
        cooldown = true;
        yield return new WaitForSeconds(1f);
        cooldown = false;
    }
}
