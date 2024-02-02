using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private GameObject _keys;
    [SerializeField]
    private CombatBro _playerCombat;
    private float _time = 0f;

    public void ResetTime()
    {
        _time = 0f;
        //_keys.SetActive(false);
    }

    void Update()
    {
        if (!_playerCombat.enabled)
        {
            return;
        }

        _time += Time.deltaTime;

        if (Input.GetButton("Fire2"))
        {
            _time = 0f;
            if (_player.GetChild(_player.childCount - 1).name == "Oval")
            {
                _player.GetChild(_player.childCount - 1).gameObject.SetActive(true);
            }
            //_keys.SetActive(false);
        }

        if (_time > 4f)
        {
            _player.GetChild(_player.childCount - 1).gameObject.SetActive(false);
        }

        if (_time > 30f)
        {
            //_keys.SetActive(true);
        }
    }
}
