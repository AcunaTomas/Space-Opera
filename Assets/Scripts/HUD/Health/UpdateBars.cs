using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateBars : MonoBehaviour
{
    [SerializeField]
    private GameObject _leftBar0;
    [SerializeField]
    private GameObject _leftBar1;
    [SerializeField]
    private GameObject _leftBar2;
    [SerializeField]
    private GameObject _leftBar3;
    [SerializeField]
    private GameObject _rightBar;
    [SerializeField]
    private Player _player;
    [SerializeField]
    private GameObject _energyBar;
    private int _playerHP;
    private int _energy = 0;
    
    void Start()
    {
        _player = GameManager.INSTANCE.PLAYER.GetComponent<Player>();
        _playerHP = _player.GetMaxHP();
        SetHP();
    }

    public void ResetHP()
    {
        _energy = 0;
        _playerHP = 0;

        _player = GameManager.INSTANCE.PLAYER.GetComponent<Player>();
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        Debug.Log(GameManager.INSTANCE.PLAYER.name);
        Debug.Log("resetHP");

        SetHP();
        UpdateEnergy();
    }

    public void SetEnergy(int e)
    {
        _energy = e;
        UpdateEnergy();
    }

    public void EnergyPlusOne()
    {
        _energy++;
        if (_energy == _energyBar.transform.childCount)
        {
            _energy = 0;
            if (_player.GetHP() < _playerHP)
            {
                _player.AddHP(1);
            }
        }
        UpdateEnergy();
    }

    public void SetHP()
    {
        _playerHP = _player.GetHP();
        for (int i = 0; i < _playerHP; i++)
        {
            GameObject go;
            if (i == (_playerHP-1))
            {
                go = Instantiate(_rightBar, transform);
            }
            else if (i == _playerHP-2)
            {
                go = Instantiate(_leftBar3, transform);
            }
            else if (i == _playerHP - 3)
            {
                go = Instantiate(_leftBar2, transform);
            }
            else if (i == _playerHP - 4)
            {
                go = Instantiate(_leftBar1, transform);
            }
            else
            {
                go = Instantiate(_leftBar0, transform);
            }
        }
    }

    public void UpdateHP()
    {
        int _actualHP = _player.GetHP();
        for (int i = 0; i < _playerHP; i++)
        {
            HealthBar hb = transform.GetChild(i).GetComponent<HealthBar>();
            if (_actualHP <= i)
            {
                hb.ChangeBool(true);
            }
            else
            {
                hb.ChangeBool(false);
            }
        }
    }

    public void UpdateEnergy()
    {
        for (int i = 1; i <= _energyBar.transform.childCount; i++)
        {
            HealthBar hb = _energyBar.transform.GetChild(i-1).GetComponent<HealthBar>();
            if (i <= _energy)
            {
                hb.ChangeBool(false);
            }
            else
            {
                hb.ChangeBool(true);
            }
        }
    }
}
