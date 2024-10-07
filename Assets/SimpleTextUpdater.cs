using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimpleTextUpdater : MonoBehaviour //For updating the text according to the language
{
    [SerializeField] private string _zoneName;
    private string _text;

    void Start()
    {
        _text = UIManager.INSTANCE.DialogueBoxLoadText(_zoneName);
        transform.GetComponent<TextMeshProUGUI>().text = _text;
    }

    public void UpdateLanguage()
    {
        _text = UIManager.INSTANCE.DialogueBoxLoadText(_zoneName);
        transform.GetComponent<TextMeshProUGUI>().text = _text;
    }
}

