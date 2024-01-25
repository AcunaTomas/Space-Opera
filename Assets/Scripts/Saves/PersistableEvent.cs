using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistableEvent : PersistableObject
{
    [SerializeField]
    string Event = "";
    
    void Awake()
    {
        keyName = gameObject.name;
        ReadSaveData();
    }

    public override void ReadSaveData()
    {
        print("EventSaveData");

    }

    public void StatusChanged()
    {
        state = 1;
    }

}
