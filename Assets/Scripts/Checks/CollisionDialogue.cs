using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDialogue : MonoBehaviour
{
    
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject _panelDialogue;
    [SerializeField]
    private string _id;

    void OnTriggerEnter2D(Collider2D col)
    {
        _player.GetComponent<Player>().enabled = false;
        _player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        _panelDialogue.SetActive(true);
        _panelDialogue.GetComponent<ButtonDialogue>()._zoneName = _id;
        gameObject.SetActive(false);
    }

}
