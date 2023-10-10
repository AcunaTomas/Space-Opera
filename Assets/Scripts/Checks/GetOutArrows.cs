using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOutArrows : MonoBehaviour
{
    [SerializeField]
    private Transform _player;

    void OnTriggerEnter2D(Collider2D col)
    {
        _player.GetChild(_player.childCount - 1).gameObject.SetActive(false);
        _player.GetComponent<ButtonPlayer>().ResetTime();
    }
}
