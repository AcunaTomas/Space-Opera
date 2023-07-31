using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    
    [SerializeField]
    private Sprite _fullLife;
    [SerializeField]
    private Sprite _emptyLife;
    [SerializeField]
    private bool _empty;

    public bool GetBool()
    {
        return _empty;
    }

    public void ChangeBool(bool bl)
    {
        if (_empty == bl)
        {
            return;
        }
        _empty = bl;
        OnBooleanValueChanged();
    }

    private void OnBooleanValueChanged()
    {
        if (_empty)
        {
            gameObject.GetComponent<Image>().sprite = _emptyLife;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = _fullLife;
        }
    }

}
