using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lvl1_QUILOMBO_TIME : MonoBehaviour
{
    [SerializeField]
    private GameObject scren;
    public void A()
    {
        var s = Instantiate(scren);
        s.transform.parent = GameObject.Find("Canvas").transform;
        GameObject.Find("NPCs").SetActive(false);
        
    }
}
