using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float HP = 193f;
    [SerializeField]
    float vertspid = -4f;
    [SerializeField]
    bool canIjump = true;
    bool wallijumpy = false;
    private Rigidbody2D body;
    [SerializeField]
    private float jumpLimit = 0f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        _plaseJump();
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * 5f, vertspid);
        vertspid += -0.03f;
        vertspid = Clamp(vertspid, -7f, 8f);
    }

    private void _plaseJump()
    {
        
        if (canIjump && Input.GetButton("Jump"))
        {
           
            jumpLimit += 0.02f;
            vertspid = 6f + (jumpLimit * 0.12f);
            if(wallijumpy)
            {
                
            }
        }
        if (jumpLimit >= 3f || Input.GetButton("Jump") == false)
        {
            jumpLimit = 0f;
            canIjump = false;
        }
        Debug.Log(transform.position);

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

    void OnCollisionStay2D(Collision2D collision)
    {
        Debug.DrawRay(collision.GetContact(0).normal, collision.GetContact(0).normal, Color.red);
        Debug.DrawRay(transform.position, transform.up, Color.green);
        //Debug.Log(collision.GetContact(0).normal);
        Vector3 normalcoll = collision.GetContact(0).normal;

        Debug.Log(normalcoll.y);
        Debug.Log(normalcoll.x);


        if (Mathf.Abs(normalcoll.y) > Mathf.Abs(normalcoll.x) && normalcoll.y > 0)
        {
            canIjump = true;
            wallijumpy = false;

        }


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 normalcoll = collision.GetContact(0).normal;
        if (normalcoll.y < 0)
        {
            canIjump = false;
            wallijumpy = false;
            jumpLimit = 3.5f;
            vertspid =  -0.3f;
        }
    }

    


}
