using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTwo : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _detectable;
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private GameObject _triggerEnd;
    private int _cont = 0;

    public void DeactivateObject ()
    {
        GameObject go = null;
        for (int i = 0; i < _detectable.Length; i++)
        {
            if (_detectable[i].GetComponent<CollisionDialogue>().HasEntered())
            {
                go = _detectable[i];
                break;
            }
        }
        _cont++;

        if (_cont == 1)
        {
            for (int i = 0; i < _detectable.Length; i++)
            {
                if (go.name == _detectable[i].name)
                {
                    _detectable[i].transform.GetChild(0).BroadcastMessage("manualDo", SendMessageOptions.DontRequireReceiver);
                    _player.GetChild(_player.childCount - 1).GetChild(i).gameObject.SetActive(false);
                }
                else
                {
                    _detectable[i].GetComponent<CollisionDialogue>().ChangeId("lvl02_tools_02");
                }
            }
        }

        if (_cont == 2)
        {
            for (int i = 0; i < _detectable.Length; i++)
            {
                if (go.name == _detectable[i].name)
                {
                    _detectable[i].transform.GetChild(0).BroadcastMessage("manualDo", SendMessageOptions.DontRequireReceiver);
                    _player.GetChild(_player.childCount - 1).GetChild(i).gameObject.SetActive(false);
                }
                else
                {
                    _detectable[i].GetComponent<CollisionDialogue>().ChangeId("lvl02_ship");
                }
            }
        }

        go.SetActive(false);
    }
}
