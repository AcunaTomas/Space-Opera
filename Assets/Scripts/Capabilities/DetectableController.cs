using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectableController : MonoBehaviour
{
    //public GameObject _bullet;
    public GameObject _brillo;
    public void Detectado()
    {
        //Instantiate(_bullet, transform.position, Quaternion.identity);
        _brillo.SetActive(true);
    }
}
