using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SImpleFadeController : MonoBehaviour
{
    SpriteRenderer _sp;

    public bool fademode = false ;

    public float fadetime ;

    

    void Start()
    {
        if (fadetime == 0)
        {
            fadetime = 0.05f;
        }


        _sp = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (fademode)
        {
            if (_sp.color.a >= 1f)
            {
                return;
            }
            _sp.color += new Color(0,0,0, fadetime);

        }
        if (!fademode)
        {
            if (_sp.color.a <= 0f)
            {
                return;
            }
            _sp.color += new Color(0,0,0, -fadetime);
        }

        Debug.Log(_sp.color.a);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            fademode = false;
        }
        Debug.Log("Enter");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            fademode = true;
        }
        Debug.Log("Exit");
    }
}
