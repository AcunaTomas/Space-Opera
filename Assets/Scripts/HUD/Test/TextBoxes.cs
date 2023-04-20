using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBoxes : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _daText;
    private string _text;
    public string texto
    {
        get => _text;
        set
        {
            _text = value;
        }
    
    }

    void Start()
    {
        _daText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        texto = _daText.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
