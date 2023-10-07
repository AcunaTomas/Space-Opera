using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObjectAfter : MonoBehaviour
{

    [SerializeField]
    private GameObject _obj;
    [SerializeField]
    private float _time;

    private float _timeZero = 0;

    void Update()
    {
        _timeZero += Time.deltaTime;
        if (_timeZero > _time)
        {
            _obj.SetActive(true);
            _obj.BroadcastMessage("manualDo", SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject.GetComponent<ActivateObjectAfter>());
        }
    }
}
