using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dust : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer _sp;
    Transform _position;

    void Start()
    {
        _sp = GetComponent<SpriteRenderer>();
        _position = GetComponent<Transform>();
    }

    public void Initialize(Player.parama a)
    {
        transform.position = a.b;
        _sp = GetComponent<SpriteRenderer>();
        _sp.flipX = a.a;
        if (GameManager.INSTANCE.PLAYER.GetComponent<SpriteRenderer>().flipX)
        {
            _sp.flipX = false;
            transform.position = new Vector2(transform.position.x +0.15f, transform.position.y -0.018f);
        }
        else
        {
            _sp.flipX = true;
            transform.position = new Vector2(transform.position.x -0.15f, transform.position.y -0.018f);
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
