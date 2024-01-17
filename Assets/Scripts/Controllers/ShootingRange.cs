using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingRange : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BalaPlayer"))
        {
            gameObject.GetComponent<Event>().BroadcastMessage("manualDo");
        }
    }
}
