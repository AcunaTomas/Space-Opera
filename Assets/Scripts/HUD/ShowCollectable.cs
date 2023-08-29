using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCollectable : MonoBehaviour
{
    public Image IMAGE;

    public void Awake()
    {
        IMAGE.sprite = Collect.instance.IMAGESOURCE;
    }
}
