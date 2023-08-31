using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSwitcherLogic : MonoBehaviour
{
    
    public Player bi2;
    public Playerererer brodi;

    private string who = "Bito";

    private bool cooldown = false;

    void Update()
    {
        if (Input.GetAxis("CharSwitch1") > 0 && Input.GetAxis("CharSwitch2") > 0 && cooldown == false)
        {
            switch (who)
            {
                case "Bito":
                    {
                        bi2.gameObject.SetActive(false);
                        brodi.gameObject.SetActive(true);
                        brodi.gameObject.transform.position = bi2.gameObject.transform.position;
                        who = "Brody";
                        StartCoroutine(cooldown2());
                        break;
                    }
                case "Brody":
                    {
                        brodi.gameObject.SetActive(false);
                        bi2.gameObject.SetActive(true);
                        bi2.gameObject.transform.position = brodi.gameObject.transform.position;
                        who = "Bito";
                        StartCoroutine(cooldown2());
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

    IEnumerator cooldown2()
    {
        cooldown = true;
        yield return new WaitForSeconds(1f);
        cooldown = false;
    }
}
