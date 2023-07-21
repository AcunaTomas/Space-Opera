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
        s.transform.SetParent(GameObject.Find("Canvas").transform);
        s.transform.position = new Vector2(0,0);
        GameObject.Find("NPCs").SetActive(false);
        
    }
}
