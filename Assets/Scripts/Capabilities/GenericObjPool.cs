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



    public virtual void AddObject(GameObject Obj2add)
    {
        _objectList.Add(Obj2add);
    }

    public virtual void DisableObj(GameObject Obj2Disable)
    {
        _objectList.Find(x => x.name == Obj2Disable.name).SetActive(false);
    }
    public virtual void EnableObj(GameObject Obj2Enable)
    {
        _objectList.Find(x => x.name == Obj2Enable.name).SetActive(true);
    }
    public virtual GameObject GetObj(string name = "")
    {
        if (name != "")
        {
            return _objectList.Find(x => x.name == name);
        }
        return _objectList.Find(x => x.activeSelf == false);
    }

}
