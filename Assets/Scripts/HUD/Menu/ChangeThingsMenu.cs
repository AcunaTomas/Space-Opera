using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeThingsMenu : MonoBehaviour
{

    public void ChangeValueImage(float n)
    {
        gameObject.GetComponent<SpriteRenderer>().material.color = Color.HSVToRGB(0, 0, n);
    }

}
