using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HurtBox : MonoBehaviour
{
    protected int attackDamage = 1;
    protected float orientation = 1f;
    protected GameObject player;
    

    public virtual void Awake()
    {
        player = GameObject.FindWithTag("Player");
        Physics2D.IgnoreCollision(player.GetComponent<CircleCollider2D>(), GetComponent<Collider2D>());
    }

    public virtual float setOrientation(float orientationValue)
    {
        orientation = orientationValue;
        return orientation;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = GameObject.FindWithTag("Player");
            other.GetComponent<Player>().LoseHP(attackDamage, transform.position);
            //other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 2), ForceMode2D.Impulse);
                //other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll; Volver a poner luego?
            other.GetComponent<PlayerCombat>().enabled = false;
            //other.GetComponent<Rigidbody2D>().AddForce(new Vector2(orientation * 5, 0), ForceMode2D.Impulse);
            gameObject.SetActive(false);
            transform.parent.gameObject.GetComponent<EnemyBehaviour2>().DONTMOVE = true; 
            DelayMoveAgain(0.2f);
            Debug.Log("hit");
        }
    }
    protected void DelayMoveAgain(float cd)
    {
        Invoke("MoveAgain", cd);
    }
    public virtual void MoveAgain()
    {
        transform.parent.gameObject.GetComponent<EnemyBehaviour2>().DONTMOVE = false; 
        //player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        //player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        if (player.GetComponent<Player>().GetHP() > 0)
        {
            player.GetComponent<PlayerCombat>().enabled = true;
        }
    }

    public void ChangeAttackDMG(int _newDMG)
    {
        attackDamage = _newDMG;
    }
}
