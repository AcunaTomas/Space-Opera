using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PulsatingFilter : MonoBehaviour
{
    Color a = new Color(1, 0, 0, 0.5f);
    Color c = new Color(1, 0, 0, 0.0f);
    Graphic b;

    void Awake()
    {
        b = GetComponent<Graphic>();
        gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(1f,1f);
        gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(0f,0f);
    }

    void Start()
    {
        gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(0f,0f);
        gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(0f,0f);
    }

    void Update()
    {
        b.color = Color.Lerp(a, c, Mathf.PingPong(Time.time, 1));
    }
}
