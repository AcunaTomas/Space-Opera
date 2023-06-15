using UnityEngine;
using TMPro;

public class ButtonMenu : MonoBehaviour
{
    private TMP_Text _buttonText;
    private Vector3 _originalScale;
    
    void Start()
    {
        _buttonText = GetComponentInChildren<TMP_Text>();
        _originalScale = _buttonText.transform.localScale;
    }

    public void OnPointerEnter()
    {
        Vector3 newScale = _originalScale * 1.15f;
        _buttonText.transform.localScale = newScale;
    }

    public void OnPointerExit()
    {
        _buttonText.transform.localScale = _originalScale;
    }
}
