using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDFollower : MonoBehaviour
{
    [Header("Follow Target")]
    [SerializeField]
    private Transform _target;

    
    public Vector3 relativePos;

    void Awake()
    {
        
    }

    public void SetTarget(Transform trs)
    {
        _target = trs;
    }

    void Update()
    {
        transform.position = _target.position + relativePos;
    }
}
