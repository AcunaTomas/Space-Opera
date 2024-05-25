using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundStrings : MonoBehaviour
{
    [SerializeField] private float minTime, maxTime;
    [SerializeField] private float randomTime;

    private FMODUnity.StudioEventEmitter soundToInstantiate;


    void Start()
    {
        soundToInstantiate = GetComponent<FMODUnity.StudioEventEmitter>();
        SetRandomTime();
    }


    void Update()
    {
        if(randomTime > 0)
        {
            randomTime -= Time.deltaTime;
        }
        else if(randomTime <= 0)
        {
            soundToInstantiate.Play();
            SetRandomTime();
        }
    }

    void SetRandomTime()
    {
        randomTime = Random.Range(minTime, maxTime);
    }
}
