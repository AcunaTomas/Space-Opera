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
    private GameObject[] _dialogues;
    [SerializeField]
    private GameObject _triggerEnd;
    private int _cont = 0;

    public void DeactivateObject (GameObject go)
    {
        go.SetActive(false);
        _cont++;

        if (_cont == 1)
        {
            for (int i = 0; i < _detectable.Length; i++)
            {
                if (go.name == _detectable[i].name)
                {
                    StartCoroutine(Check(i));
                    _player.GetChild(_player.childCount - 1).GetChild(i).gameObject.SetActive(false);
                }
                else
                {
                    _dialogues[i].GetComponent<CollisionDialogue>().ChangeId("lvl02_tools_02");
                }
            }
        }

        if (_cont == 2)
        {
            for (int i = 0; i < _detectable.Length; i++)
            {
                if (go.name == _detectable[i].name)
                {
                    StartCoroutine(Check(i));
                    _player.GetChild(_player.childCount - 1).GetChild(i).gameObject.SetActive(false);
                }
            }
        }

        if (_cont == 3)
        {
            for (int i = 0; i < _detectable.Length; i++)
            {
                if (go.name == _detectable[i].name)
                {
                    _player.GetChild(_player.childCount - 1).GetChild(i).gameObject.SetActive(false);
                }
            }
        }

        int x = 0;
        for (int i = 0; i < _detectable.Length; i++)
        {
            if (!_detectable[i].activeSelf)
            {
                x++;
            }
        }

        if (x == _detectable.Length)
        {
            _triggerEnd.BroadcastMessage("manualDo", SendMessageOptions.DontRequireReceiver);
        }
    }

    IEnumerator Check(int x)
    {
        yield return new WaitForSeconds(0.25f);
        _dialogues[x].SetActive(true);
    }
}
