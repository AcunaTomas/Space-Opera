using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateBars : MonoBehaviour
{
    [SerializeField]
    private GameObject _leftBar;
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
        SetHP();
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
            if (int.Parse(_player.GetHP()) < MaxHP())
            {
                _player.AddHP(1);
            }
        }
        UpdateEnergy();
    }

    public int MaxHP()
    {
        return _playerHP;
    }

    public void SetHP()
    {
        _playerHP = int.Parse(_player.GetHP());
        for (int i = 0; i < _playerHP; i++)
        {
            GameObject go;
            if (i == (_playerHP-1))
            {
                go = Instantiate(_rightBar, transform);
            }
            else
            {
                go = Instantiate(_leftBar, transform);
            }
        }
    }

    public void UpdateHP()
    {
        int _actualHP = int.Parse(_player.GetHP());
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
