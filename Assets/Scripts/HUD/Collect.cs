using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collect : MonoBehaviour
{
    public static Collect instance;
    public Image IMAGE;
    public Sprite IMAGESOURCE;

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }
    
    public void SetImage()
    {
        IMAGE.sprite = IMAGESOURCE;
    }
}
