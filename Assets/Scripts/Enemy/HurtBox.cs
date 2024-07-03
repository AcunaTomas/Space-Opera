using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HurtBox : MonoBehaviour
{
    public bool _persistant = false;
    protected int attackDamage = 1;
    protected float orientation = 1f;
    protected GameObject player;
    [SerializeField]protected float KnockbackDaño = .5f;

    private Enemy _enemyRef;
    public virtual void Awake()
    {
        player = GameObject.FindWithTag("Player");

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
            other.GetComponent<Player>().LoseHP(attackDamage, transform.position, KnockbackDaño);
            //other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 2), ForceMode2D.Impulse);
                //other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll; Volver a poner luego?
            other.GetComponent<PlayerCombat>().enabled = false;
            //other.GetComponent<Rigidbody2D>().AddForce(new Vector2(orientation * 5, 0), ForceMode2D.Impulse);
            if (!_persistant)
            {
                gameObject.SetActive(false);
            }
            
            if(transform.parent != null)
            {
                try
                {
                    _enemyRef = transform.parent.gameObject.GetComponent<Enemy>();
                    switch (_enemyRef.GetEnemyType())
                {
                    case Enemy.EnemyType.melee:
                        transform.parent.gameObject.GetComponent<EnemyBehaviour2>().DONTMOVE = true;
                        break;
                    case Enemy.EnemyType.gun:
                        Debug.Log("pistola");
                        break;
                    default:
                        break;
                }
                }
                catch
                {
                    return;
                }
                
                
            }
            DelayMoveAgain(0.166f);
            Debug.Log("hit");
        }
    }
    protected void DelayMoveAgain(float cd)
    {
        Invoke(nameof(EnablePlayer), cd);
        Invoke(nameof(MoveAgain), cd*2);
    }
    public virtual void MoveAgain()
    {
        if(transform.parent != null)
            {
                switch (transform.parent.gameObject.GetComponent<Enemy>().GetEnemyType())
            {
                case Enemy.EnemyType.melee:
                    transform.parent.gameObject.GetComponent<EnemyBehaviour2>().DONTMOVE = false;
                    break;
                case Enemy.EnemyType.gun:
                    Debug.Log("pistola");
                    break;
                default:
                    break;
            }
            }
        //player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        //player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        
    }

    public void ChangeAttackDMG(int _newDMG)
    {
        attackDamage = _newDMG;
    }

    void EnablePlayer()
    {
        if (player.GetComponent<Player>().GetHP() > 0)
        {
            player.GetComponent<PlayerCombat>().enabled = true;
        }
    }
}
