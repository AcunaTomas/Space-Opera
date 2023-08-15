using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class TutorialTextManager : MonoBehaviour
{
    [SerializeField]
    private string _zoneName;
    private string[] _text;

    void Start()
    {
        _text = GameManager.INSTANCE.CANVAS.AddText(_zoneName);
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _text[0];
    }
}
