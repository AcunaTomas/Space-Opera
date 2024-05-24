using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSMainMenu : MonoBehaviour
{

    bool a  = true;
    void Update()
    {
        if (a)
        {
            StartCoroutine(LoadAnim());
        }
    }

    IEnumerator LoadAnim()
    {
        a = false; 

        yield return new WaitForSeconds(15);

        gameObject.GetComponent<Animator>().SetTrigger("Anim");
        
        yield return new WaitForSeconds(5);
        a = true;
    }



}
