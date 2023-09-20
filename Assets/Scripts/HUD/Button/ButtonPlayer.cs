using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject _keys;
    [SerializeField]
    private CombatBro _playerCombat;
    private float _time = 0f;

    public void ResetTime()
    {
        _time = 0f;
        _keys.SetActive(false);
    }

    // Update is called once per frame
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
            _keys.SetActive(false);
        }

        if (_time > 30f)
        {
            _keys.SetActive(true);
        }
    }
}
