using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHittableEntity : MonoBehaviour
{

    public List<LayerMask> AllowedLayers;
    public bool SkipDefaults = false;

    private int[] layernums = {14,15,16,20};

    void Awake()
    {
        if (AllowedLayers is null)
        {
            AllowedLayers = new List<LayerMask>();
        }
        AddDefaults(SkipDefaults);
 
    }

    private void AddDefaults(bool skip)
    {
        if (skip == true)
        {
            return;
        }
        for (int i = 0; i < layernums.Length; i++)
        {
            print("dosomethinh");
            AllowedLayers.Add(1 << layernums[i]);
        }

    }

    public bool LayersCheckAllowed(LayerMask layer2check)
    {
        if (AllowedLayers.Contains(layer2check))
        {
            return true;
        }
        return false;
    }


}