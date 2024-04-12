using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dust : MonoBehaviour
{
    SpriteRenderer _sp;
    Transform _position;
    GameObject _controller;

    public struct parama
    {
        public bool a;
        public Vector2 b;
        
        public parama(bool ab, Vector2 bb)
        {
            a = ab;
            b = bb;
        }
    }


    void Start()
    {
        _sp = GetComponent<SpriteRenderer>();
        _position = GetComponent<Transform>();
    }

    public void Initialize(parama a)
    {
        transform.position = a.b;
        _sp = GetComponent<SpriteRenderer>();
        _sp.flipX = a.a;

        _controller = GameObject.FindWithTag("ControllerCheck");

        if (_controller.GetComponent<ChangeInputs>()._control == false) 
        {
            if (GameManager.INSTANCE.PLAYER.GetComponent<SpriteRenderer>().flipX)
            {
                _sp.flipX = false;
                transform.position = new Vector2(transform.position.x + 0.15f, transform.position.y - 0.018f);
            }
            else
            {
                _sp.flipX = true;
                transform.position = new Vector2(transform.position.x - 0.15f, transform.position.y - 0.018f);
            }
        }
        else
        {
            if (GameManager.INSTANCE.PLAYER.GetComponent<SpriteRenderer>().flipX)
            {
                _sp.flipX = true;
                transform.position = new Vector2(transform.position.x - 0.05f, transform.position.y - 0.018f);
            }
            else
            {
                _sp.flipX = false;
                transform.position = new Vector2(transform.position.x + 0.05f, transform.position.y - 0.018f);
            }
        }
        

        StartCoroutine(xd());
    }
    private IEnumerator xd()
    {
        yield return new WaitForSeconds(0.4f);
        GameManager.INSTANCE.dustcap -= 1;
        Destroy(gameObject);

    }

}
