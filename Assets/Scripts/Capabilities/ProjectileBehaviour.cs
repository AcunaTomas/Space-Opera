using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float speed = 4; 
    public Vector3 launchOffset;
    public bool thrown;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        GameObject Player = GameObject.FindWithTag("Player");
        spriteRenderer = Player.GetComponent<SpriteRenderer>();
        Throw();
    }

    public void Update()
    {
        if(!thrown)
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
    }

    void Throw()
    {
        if(thrown && spriteRenderer.flipX)
        {
            var direction = -transform.right + Vector3.up;
            GetComponent<Rigidbody2D>().AddForce(direction * speed ,ForceMode2D.Impulse);
        }
        else
        {
            var direction = transform.right + Vector3.up;
            GetComponent<Rigidbody2D>().AddForce(direction * speed ,ForceMode2D.Impulse);
        }
        transform.Translate(launchOffset);
    }
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        Destroy(gameObject);
    }

}
