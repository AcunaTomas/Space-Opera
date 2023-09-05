using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectableController2 : MonoBehaviour
{
    public GameObject _bullet;
    public GameObject _brillo;
    public GameObject _trigger;
    public void Detectado()
    {
        Instantiate(_bullet, transform.position, Quaternion.identity);
        _brillo.SetActive(true);
        try
        {
            _trigger.SetActive(true);
        }
        catch (System.Exception)
        {
            
        }
    }
}
