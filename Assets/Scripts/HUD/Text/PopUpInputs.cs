using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpInputs : MonoBehaviour
{
    [SerializeField] private GameObject bitoInputs;
    [SerializeField] private GameObject brodyInputs;
    [SerializeField] private string _skillID;
    [SerializeField] private GameObject[] skillCanvas;
    private string _text;
    private bool _canInteract = true;
    private bool _buttonPressed = false;
    private bool _fold = false;
    private bool _objectiveChanged = true;
    private float _timeAfk = 0f;
    private float _movementX;

    void Start()
    {
        _movementX = -710f;
        UpdateLanguage();
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
                //GameManager.INSTANCE.ChangeTimeScale(0f);
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
            UpdateLanguage();
                //GameManager.INSTANCE.ChangeTimeScale(1f);
            _canInteract = false;
            _buttonPressed = true;
            StartCoroutine(FoldUnfold(_fold));
        }

        if (Input.GetAxis("CharSwitch1") > 0 && !_buttonPressed)
        {
            _buttonPressed = true;
            bitoInputs.SetActive(!bitoInputs.activeInHierarchy);
            brodyInputs.SetActive(!brodyInputs.activeInHierarchy);
        }

        if(Input.GetAxis("CharSwitch1") == 0)
        {
            _buttonPressed = false;
        }

        if (Input.GetButtonUp("Select"))
        {
            _buttonPressed = false;
        }
        
    }

    public void UpdateLanguage()
    {
        foreach (GameObject skill in skillCanvas)
        {
            _skillID = skill.GetComponent<ButtonMenu>().NAME;
            _text = UIManager.INSTANCE.DialogueBoxLoadText(_skillID + "name");
            skill.transform.GetChild(0).GetComponent<TMP_Text>().text = _text;
            _text = UIManager.INSTANCE.DialogueBoxLoadText(_skillID + "desc");
            skill.transform.GetChild(3).GetComponent<TMP_Text>().text = _text;
        }
    }
}
