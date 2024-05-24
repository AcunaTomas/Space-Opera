using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrompOnlyBrody : Promp
{

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && col.GetComponent<Player>()._playerType == Player.PlayerType.Brody)
        {
            _interactable.transform.parent = transform;
            _interactable.transform.localPosition = _position;
            _interactable.SetActive(true);
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
           _interactable.SetActive(false);
        }

    }
}
