using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Fades : MonoBehaviour
{
    private Tilemap _tilemap;

    private SpriteRenderer img;

    private float _fadeTime = 0.05f;

    private bool _visible = false;

    [SerializeField]
    private bool _mode;

    void Start()
    {
        if (_mode == true)
        {
            _tilemap = GetComponent<Tilemap>();
        }
        else
        {
            img = GetComponent<SpriteRenderer>();
        }
    }

    void FixedUpdate()
    {
        if (_mode == true)
        {
            TilemapFader();
        }
        else
        {
            SpriteFade();
        }
    }

    public void ToggleVisible(bool mode)
    {
        _visible = mode;
    }

    private void TilemapFader()
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

    private void SpriteFade()
    {
        if (_visible == true && img.color.a < 1)
        {
            img.color += new Color(0, 0, 0,  _fadeTime);
            print("FadeIN");
        }
        if (_visible == false && img.color.a > 0)
        {
            img.color -= new Color(0, 0, 0, _fadeTime);
            print("FadeOUT");
        }
    }
}
