using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectableController : MonoBehaviour
{
    public GameObject _brillo;
    public void Detectado()
    {
        _brillo.SetActive(true);
    }
}
