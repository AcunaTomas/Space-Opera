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
    private float Xspeed = 0f;
    private Vector2 caps = new Vector2(4f, 7f);
    [SerializeField]
    private int extrajumpcount = 1;
    [SerializeField]
    private float _lastJumpPress = 0f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        _plaseJump();
        FakeGravity();
        if (Input.GetButton("Jump"))
        {
            _lastJumpPress += 0.17f;
        }
        else
        {
            _lastJumpPress = 0f;
        }
    }

    private void _plaseJump()
    {
        
        if (canIjump && Input.GetButton("Jump"))
        {
           
            jumpLimit += 0.3f;
            vertspid = 5f + (jumpLimit * 0.12f);

        }
        if (jumpLimit >= 3f || Input.GetButton("Jump") == false)
        {
            jumpLimit = 0f;
            canIjump = false;
        }
        if (wallijumpy)
        {
            WallJump();
        }
        //Debug.Log(canIjump);

    }
    private void WallJump()
    {
        if (Input.GetButton("Jump") && _lastJumpPress <= 0.9f)
        {
            vertspid = 18f;
            Xspeed = Input.GetAxis("Horizontal") + 10f;
            wallijumpy = false;
            Debug.Log("WallE");
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

    void OnCollisionStay2D(Collision2D collision)
    {
        Debug.DrawRay(collision.GetContact(0).normal, collision.GetContact(0).normal, Color.red);
        Debug.DrawRay(transform.position, transform.up, Color.green);
        //Debug.Log(collision.GetContact(0).normal);
        Vector3 normalcoll = collision.GetContact(0).normal;

        //Debug.Log(normalcoll.y);
        //Debug.Log(normalcoll.x);


        if (Mathf.Abs(normalcoll.y) > Mathf.Abs(normalcoll.x) && normalcoll.y > 0)
        {
            canIjump = true;
            wallijumpy = false;
            extrajumpcount = 1;

        }
        if (Mathf.Abs(normalcoll.y) < Mathf.Abs(normalcoll.x) && extrajumpcount > 0)
        {

          wallijumpy = true;
           extrajumpcount += -1;

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

    
    void FakeGravity()
    {
        body.AddRelativeForce(new Vector2(Xspeed, vertspid));
        Xspeed = Input.GetAxis("Horizontal") * 5f;
        vertspid += -0.3f;
        body.velocity = new Vector2(Clamp(Xspeed, -caps.x, caps.x), vertspid);
        vertspid = Clamp(vertspid, -caps.y, caps.y);
    }

}
