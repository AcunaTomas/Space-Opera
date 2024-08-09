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

    bool IsEnabled = true;

    private Object effect; 

    private float effectDuration = 0.4f;

    public float RespawnCooldown = 2f;
    void Start()
    {
        effect = Resources.Load("Prefabs/Sparkle");
    }
    void Update()
    {
        if (IsEnabled)
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
                                //GameManager.INSTANCE.UIManager.LifeBar.UpdatePlayer();
                                UIManager.INSTANCE.CURRENTPLAYER = "Brody";
                                brodi.gameObject.GetComponent<SpriteRenderer>().color += new Color (0, 0, 0, 100);
                                UIManager.INSTANCE.ToggleHPBars();
                                brodi.SetHP(bi2.GetHP());
                                brodi._spriteRenderer.flipX = bi2._spriteRenderer.flipX;
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
                                //GameManager.INSTANCE.UIManager.LifeBar.UpdatePlayer();
                                UIManager.INSTANCE.CURRENTPLAYER = "Bito";
                                bi2.gameObject.GetComponent<SpriteRenderer>().color += new Color (0, 0, 0, 100);
                                UIManager.INSTANCE.ToggleHPBars();
                                bi2.SetHP(brodi.GetHP());
                                bi2.gameObject.GetComponent<PlayerCombat>()._jabulaniActive = false;
                                bi2._spriteRenderer.flipX = brodi._spriteRenderer.flipX;
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
                    if (bi2.GetOrientation() != brodi.GetOrientation())
                    {
                        brodi.SendMessage("Flip", SendMessageOptions.DontRequireReceiver);
                    }
                }

            
        }
   

    }

    public void ForceChangeBito()
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
                                //GameManager.INSTANCE.UIManager.LifeBar.UpdatePlayer();
                                UIManager.INSTANCE.CURRENTPLAYER = "Brody";
                                brodi.gameObject.GetComponent<SpriteRenderer>().color += new Color (0, 0, 0, 100);
                                UIManager.INSTANCE.ToggleHPBars();
                                brodi.SetHP(bi2.GetHP());
                                brodi._spriteRenderer.flipX = bi2._spriteRenderer.flipX;
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
                                //GameManager.INSTANCE.UIManager.LifeBar.UpdatePlayer();
                                UIManager.INSTANCE.CURRENTPLAYER = "Bito";
                                bi2.gameObject.GetComponent<SpriteRenderer>().color += new Color (0, 0, 0, 100);
                                UIManager.INSTANCE.ToggleHPBars();
                                bi2.SetHP(brodi.GetHP());
                                bi2.gameObject.GetComponent<PlayerCombat>()._jabulaniActive = false;
                                bi2._spriteRenderer.flipX = brodi._spriteRenderer.flipX;
                                break;
                            }
                    }
                
                if (who == "Brody")
                {
                    bi2.gameObject.transform.position = brodi.gameObject.transform.position;

                }
                else
                {
                    brodi.gameObject.transform.position = bi2.gameObject.transform.position;
                    if (bi2.GetOrientation() != brodi.GetOrientation())
                    {
                        brodi.SendMessage("Flip", SendMessageOptions.DontRequireReceiver);
                    }
                }
                                
    }

    public void ChangePlayer()
    {
        _change = true;
    }


    public void ToggleCharSwitching(bool a)
    {
        IsEnabled = a;  
    }


    IEnumerator cooldown2()
    {
        cooldown = true;
        yield return new WaitForSeconds(1f);
        cooldown = false;
    }

    public void RespawnBoludos()
    {
        StartCoroutine(Respawn(RespawnCooldown));
    }


    IEnumerator Respawn(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        brodi.ResetVars();
        bi2.ResetVars();
        ToggleCharSwitching(true);
    }
}
