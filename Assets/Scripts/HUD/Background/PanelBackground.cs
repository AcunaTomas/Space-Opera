using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelBackground : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _go;

    public void Activate()
    {
        for (int i = 0; i < _go.Length; i++)
        {
            _go[i].SetActive(true);
        }
    }
}
