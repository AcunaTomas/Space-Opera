using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HurtBoxPinchos : HurtBox
{

    public float destX;
    public float destY;

    public float RespawnTimer = 0.3f;



    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.INSTANCE.Boludos.ToggleCharSwitching(false);
            if (other.GetComponent<Player>().GetHP() <= 1) {RespawnTimer = 1.7f;}
            other.GetComponent<Player>().LoseHP(attackDamage, transform.position, KnockbackDaño);
            other.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            other.GetComponent<PlayerCombat>().enabled = false;
            Debug.Log(orientation);
            player = other.gameObject;

            Invoke(nameof(MoveAgain), RespawnTimer);
            Debug.Log("hit");

            RespawnTimer = 0.3f;
        }
    }

    public override void MoveAgain()
    {
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        if (player.GetComponent<Player>().GetHP() > 0)
        {
            player.GetComponent<PlayerCombat>().enabled = true;
        }
        player.transform.position = new Vector2(destX, destY);
        GameManager.INSTANCE.Boludos.ToggleCharSwitching(true);
    }

}
