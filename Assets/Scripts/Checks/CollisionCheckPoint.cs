using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheckPoint : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            gameObject.SetActive(false);
            GameManager.INSTANCE.CHECKPOINT = GameManager.INSTANCE.PLAYER.transform.localPosition;
            GameManager.INSTANCE.ACTUAL_CHECKPOINT = gameObject; 
            Restore();
        }
    }

    void Restore()
    {
        //DataPersistentManager.INSTANCE.SaveGame();
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i).name != transform.name)
            {
                transform.parent.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
