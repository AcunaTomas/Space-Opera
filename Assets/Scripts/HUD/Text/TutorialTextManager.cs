using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TutorialTextManager : MonoBehaviour
{
    
    [SerializeField]
    private KeyCode[] _keys;
    private List<bool> _usedKeys = new List<bool>();

    private bool _noMoreFors = false;

    void Start()
    {
        for (int i = 0; i < _keys.Length; i++)
        {
            _usedKeys.Add(false);
        }
    }

    private IEnumerator TutoGoneForever()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (_noMoreFors)
        {
            return;
        }

        for (int i = 0; i < _keys.Length; i++)
        {
            if (Input.GetKeyDown(_keys[i]))
            {
                _usedKeys[i] = true;
            }
        }

        int x = 0;
        for (int i = 0; i < _usedKeys.Count; i++)
        {
            if (_usedKeys[i])
            {
                x++;
            }

            if (x == _usedKeys.Count)
            {
                _noMoreFors = true;
                StartCoroutine(TutoGoneForever());
            }
        }
    }
}
