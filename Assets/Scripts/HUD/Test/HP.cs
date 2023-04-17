using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HP : MonoBehaviour
{
    [SerializeField]
    string DisplayText;
    TextMeshProUGUI TMProDisplay;
    GameObject[] palyer;
    [SerializeField]
    GameObject P1;
    // Start is called before the first frame update
    void Start()
    {
        TMProDisplay = GetComponent<TextMeshProUGUI>();
        palyer = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject i in palyer)
        {
            P1 = i.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        DisplayText = P1.GetComponent<Player>().GetHP();
        TMProDisplay.text = "HP: " + DisplayText;
    }
}
