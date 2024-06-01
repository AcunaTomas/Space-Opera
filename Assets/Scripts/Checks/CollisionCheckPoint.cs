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
            GameManager.INSTANCE.CHECKPOINT = gameObject.transform.localPosition;  //GameManager.INSTANCE.PLAYER.transform.localPosition;
            GameManager.INSTANCE.ACTUAL_CHECKPOINT = gameObject; 
            Restore();
        }
    }

    void Restore()
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i).name != transform.name)
            {
                transform.parent.GetChild(i).gameObject.SetActive(true);
            }
        }
        Debug.LogWarning("Game Saved");

        //DataPersistentManager.INSTANCE.SaveGame();
    }
}
