using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalasPool : GenericObjPool
{
    
    public string BALAPATH = "Prefabs/Bowlett";
    public int SIZE; 

    [SerializeField]
    GameObject Bala;
    void Start()
    {
        Bala =  Resources.Load(BALAPATH) as GameObject;
        Initialize();
    }

    private void Initialize()
    {
        for (int i = 0; i<SIZE; i++)
        {
            GameObject a = Instantiate(Bala);
            a.SetActive(false);
            AddObject(a);
            a.transform.SetParent(transform);
        }
    }

}
