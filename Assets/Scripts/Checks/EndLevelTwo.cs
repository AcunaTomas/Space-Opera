using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTwo : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _detectable;
    [SerializeField]
    private GameObject _triggerEnd;

    public void DeactivateObject (GameObject go)
    {
        go.SetActive(false);

        int x = 0;
        for (int i = 0; i < _detectable.Length; i++)
        {
            if (!_detectable[i].activeSelf)
            {
                x++;
            }
        }

        if (x == _detectable.Length)
        {
            _triggerEnd.BroadcastMessage("manualDo", SendMessageOptions.DontRequireReceiver);
        }
    }
}
