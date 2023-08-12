using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivesManager : MonoBehaviour
{
    private bool _canInteract = true;
    private bool _buttonPressed = false;
    private bool _fold = true;
    private float _timeAfk = 0f;

    private IEnumerator FoldUnfold(bool var)
    {
        _fold = !_fold;
        float _time = 0f;
        float _initialX;
        float _finalX;
        
        if (var)
        {
            _initialX = transform.localPosition.x;
            _finalX = -1260f;
        }
        else
        {
            _initialX = transform.localPosition.x;
            _finalX = -560f;
        }

        while (_time < 0.2f)
        {
            float _percentage = _time / 0.2f;
            float _newX = Mathf.Lerp(_initialX, _finalX, _percentage);
            transform.localPosition = new Vector2(_newX, transform.localPosition.y);

            _time += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = new Vector2(_finalX, transform.localPosition.y);

        yield return new WaitForSeconds(0.1f);
        
        _canInteract = true;
        _buttonPressed = false;
        _timeAfk = 0f;
    }

    void Update()
    {
        if (!_canInteract)
        {
            return;
        }

        if(_fold)
        {
            _timeAfk += Time.deltaTime;
            if (_timeAfk > 3f)
            {
                _canInteract = false;
                _timeAfk = 0f;
                StartCoroutine(FoldUnfold(_fold));
                return;
            }
        }

        if (Input.GetKeyDown("q") && !_buttonPressed)
        {
            _canInteract = false;
            _buttonPressed = true;
            StartCoroutine(FoldUnfold(_fold));
        }

        if (Input.GetKeyUp("q"))
        {
            _buttonPressed = false;
        }
        
    }
}
