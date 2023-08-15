using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class ObjectivesManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform _pressButton;
    [SerializeField]
    private string _zoneName;

    private string[] _text;
    private bool _canInteract = true;
    private bool _buttonPressed = false;
    private bool _fold = true;
    private float _timeAfk = 0f;
    private float _movementX;

    void Start()
    {
        ChangeObjective(_zoneName);
        _movementX = GetComponent<RectTransform>().sizeDelta.x - _pressButton.sizeDelta.x;
    }

    public void ChangeObjective(string _zN)
    {
        _text = GameManager.INSTANCE.CANVAS.AddText(_zN);
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _text[0];
    }

    private IEnumerator FoldUnfold(bool var)
    {
        _fold = !_fold;
        float _time = 0f;
        float _initialX;
        float _finalX;
        
        if (var)
        {
            _initialX = transform.localPosition.x;
            _finalX = _initialX - _movementX;
        }
        else
        {
            _initialX = transform.localPosition.x;
            _finalX = _initialX + _movementX;
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
