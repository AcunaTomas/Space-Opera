using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterBossBattle : MonoBehaviour
{
    public GameObject _objectToActive;

    public void ActivateObject()
    {
        _objectToActive.SetActive(true);
    }
}
