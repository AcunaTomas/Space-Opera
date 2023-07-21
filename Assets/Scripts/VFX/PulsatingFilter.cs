using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PulsatingFilter : MonoBehaviour
{
    float alpha = 0.01f;
    Color a = new Color(1, 0, 0, 0.5f);
    Color c = new Color(1, 0, 0, 0.0f);
    Graphic b;
    void Awake()
    {
        b = GetComponent<Graphic>();

    }

    void Update()
    {
        b.color = Color.Lerp(a, c, Mathf.PingPong(Time.time, 1));
    }
}
