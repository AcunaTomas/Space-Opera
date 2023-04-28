using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHud : MonoBehaviour
{
    [SerializeField]
    private GameObject textbox;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Confirm"))
        {
            textbox.SetActive(true);
        }
        else if (Input.GetButton("Cancel"))
        {
            textbox.SetActive(false);
        }
    }
}
