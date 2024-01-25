using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistableObject : MonoBehaviour
{

    public string keyName = "";
    
    public int state = 0; // 0 Initial state, 1  restore state from save
    
    void Start()
    {
        ReadSaveData();
    }

    public virtual void ReadSaveData()
    {
        print("Test");
    }

}
