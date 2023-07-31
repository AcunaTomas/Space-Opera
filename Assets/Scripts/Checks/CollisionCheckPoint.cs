using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheckPoint : MonoBehaviour
{

    /* [SerializeField]
    private bool _restoreCheckpoints = false; */

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            gameObject.SetActive(false);
            GameManager.INSTANCE.CHECKPOINT = GameManager.INSTANCE.PLAYER.transform.localPosition;
            Restore();
        }
    }

    void Restore()
    {
        /* if (!_restoreCheckpoints)
        {
            return;
        } */
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i).name != transform.name)
            {
                transform.parent.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
