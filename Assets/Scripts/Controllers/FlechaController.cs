using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlechaController : MonoBehaviour
{

    public GameObject _player;

    public GameObject[] detectables;

    public void Desaparece(GameObject go)
    {
        for (int i = 0; i < detectables.Length; i++)
        {
            if (go.name == detectables[i].name)
            {
                _player.transform.GetChild(_player.transform.childCount - 1).GetChild(i).gameObject.SetActive(false);
            }
        }
    }

}
