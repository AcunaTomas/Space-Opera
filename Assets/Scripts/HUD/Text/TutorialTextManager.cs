using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialTextManager : MonoBehaviour
{
    [SerializeField]
    private string _zoneName;
    private string _text;

    void Start()
    {
        _text = UIManager.INSTANCE.DialogueBoxLoadText(_zoneName);
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _text;
    }

    public void UpdateLanguage()
    {
        _text = UIManager.INSTANCE.DialogueBoxLoadText(_zoneName);
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _text;
    }
}
