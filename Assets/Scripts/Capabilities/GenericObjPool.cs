using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericObjPool : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _objectList;
    void Awake()
    {
        _objectList = new List<GameObject>();
    }



    public void AddObject(GameObject Obj2add)
    {
        _objectList.Add(Obj2add);
    }

    public void DisableObj(GameObject Obj2Disable)
    {
        _objectList.Find(x => x.name == Obj2Disable.name).SetActive(false);
    }
    public void EnableObj(GameObject Obj2Enable)
    {
        _objectList.Find(x => x.name == Obj2Enable.name).SetActive(true);
    }
    public GameObject GetObj(string name = "")
    {
        if (name != "")
        {
            return _objectList.Find(x => x.name == name);
        }
        return _objectList.Find(x => x.activeSelf);
    }
}
