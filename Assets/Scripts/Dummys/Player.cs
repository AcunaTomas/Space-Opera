using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float HP = 193f;
    float vertspid = -4f;
    bool canIjump = true;
    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        _plaseJump();
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * 5f, vertspid);
        vertspid += -0.03f;
        vertspid = Clamp(vertspid, -4f, 8f);
    }

    private void _plaseJump()
    {
        Debug.Log(Input.GetAxis("Jump"));
        if (canIjump && Input.GetButton("Jump"))
        {
            vertspid +=  8f;
            canIjump = false;
        }

    }


    private float Clamp(float x, float y, float z)
    {
        if (x.CompareTo(y) < 0) return y;
        else if (x.CompareTo(z) > 0) return z;
        else return x;
    }

    public string GetHP()
    {
        return HP.ToString();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.contacts[0]);
        if (collision.contacts[0].point.y < transform.position.y)
        {
            canIjump = true;
        }
        else
        {
            canIjump = false;
        }
    }
    

}
