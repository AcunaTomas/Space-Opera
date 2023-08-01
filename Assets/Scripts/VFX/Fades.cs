using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Fades : MonoBehaviour
{
    private Tilemap _tilemap;

    private float _fadeTime = 0.05f;

    private bool _visible = false;

    void Start()
    {
        _tilemap = GetComponent<Tilemap>();
    }

    void FixedUpdate()
    {
        print("FadeIN");
        print( _tilemap.color.a < 1);
        print("FadeOUT");
        print(_tilemap.color.a > 1);


        if (_visible == true && _tilemap.color.a < 1)
        {
            _tilemap.color += new Color(0, 0, 0,  _fadeTime);
            print("FadeIN");
        }
        if (_visible == false && _tilemap.color.a > 0)
        {
            _tilemap.color -= new Color(0, 0, 0, _fadeTime);
            print("FadeOUT");
        }
    }

    public void ToggleVisible(bool mode)
    {
        _visible = mode;
    }

}
