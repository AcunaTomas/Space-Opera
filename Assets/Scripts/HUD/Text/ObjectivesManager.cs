using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectivesManager : MonoBehaviour
{
    [SerializeField]
    private RectTransform _pressButton;
    [SerializeField]
    private string _zoneName;

    private string _text;
    private bool _canInteract = true;
    private bool _buttonPressed = false;
    private bool _fold = false;
    private bool _objectiveChanged = true;
    private float _timeAfk = 0f;
    private float _movementX;

    void Start()
    {
        ChangeObjective();
        _movementX = GetComponent<RectTransform>().sizeDelta.x - (_pressButton.sizeDelta.x - 10f);
    }

    private void ChangeObjective()
    {
        _text = UIManager.INSTANCE.DialogueBoxLoadText(_zoneName);
        gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = _text;
    }

    public void ChangeZoneName(string _zn)
    {
        _zoneName = _zn;
    }

    public string GetZoneName()
    {
        return _zoneName;
    }

    public void ChangeObjectiveBool()
    {
        _objectiveChanged = true;
    }

    private IEnumerator FoldUnfold(bool var)
    {
        _fold = !_fold;
        float _time = 0f;
        float _initialX;
        float _finalX;
        
        if (_objectiveChanged)
        {
            _objectiveChanged = false;
            ChangeObjective();
        }

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

        if (_objectiveChanged)
        {
            _timeAfk = 0f;
            if (_fold)
            {
                _objectiveChanged = false;
                ChangeObjective();
            }
            else
            {
                _canInteract = false;
                StartCoroutine(FoldUnfold(_fold));
            }
            return;
        }

        if (_fold)
        {
            _timeAfk += Time.deltaTime;
            if (_timeAfk > 2.5f)
            {
                _canInteract = false;
                _timeAfk = 0f;
                StartCoroutine(FoldUnfold(_fold));
                return;
            }
        }

        if (Input.GetButtonDown("Select") && !_buttonPressed)
        {
            _canInteract = false;
            _buttonPressed = true;
            StartCoroutine(FoldUnfold(_fold));
        }

        if (Input.GetButtonUp("Select"))
        {
            _buttonPressed = false;
        }
        
    }
    public void UpdateLanguage()
    {
        _text = UIManager.INSTANCE.DialogueBoxLoadText(_zoneName);
        gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = _text;
    }
}
