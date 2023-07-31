using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Promp : MonoBehaviour
{
    [SerializeField]
    private Vector3 _position;
    private GameObject _interactable;

    void OnTriggerEnter2D(Collider2D col)
    {
        _interactable = GameManager.INSTANCE.BUTTON_INTERACT;
        _interactable.transform.parent = transform;
        _interactable.transform.localPosition = _position;
        _interactable.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        _interactable.SetActive(false);
    }
}
