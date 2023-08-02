using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonMenu : MonoBehaviour
{
    private TMP_Text _buttonText;
    private Vector3 _originalScale;
    private Image _activeImage;
    private Transform _parent;
    private GameObject _lastSelected;
    public bool ACTIVE = false;
    private bool _spacebarPressed = false;
    private Button _button;
    
    void Start()
    {
        _buttonText = GetComponentInChildren<TMP_Text>();
        _originalScale = _buttonText.transform.localScale;
        _activeImage = GetComponent<Image>();
        _parent = transform.parent;
        _lastSelected = gameObject;
        _button = GetComponent<Button>();
        if (ACTIVE)
        {
            _activeImage.enabled = true;
            Vector3 newScale = _originalScale * 1.15f;
            _buttonText.transform.localScale = newScale;
        }
    }

    public void OnPointerEnter()
    {
        for (int i = 0; i < _parent.childCount; i++)
        {
            Transform child = _parent.GetChild(i);
            if (child.GetComponent<ButtonMenu>().ACTIVE)
            {
                child.GetComponent<Image>().enabled = false;
                child.GetComponent<ButtonMenu>().ACTIVE = false;
                child.GetComponentInChildren<TMP_Text>().transform.localScale = _originalScale;
                if (!EventSystem.current.alreadySelecting)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                }
                break;
            }
        }

        ACTIVE = true;
        _activeImage.enabled = true;
        if (!EventSystem.current.alreadySelecting)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }

        Vector3 newScale = _originalScale * 1.15f;
        _buttonText.transform.localScale = newScale;
    }

    private void Update()
    {
        if (!ACTIVE)
        {
            return;
        }

        if(EventSystem.current.currentSelectedGameObject == null)
        {
            if (_lastSelected.gameObject.activeSelf && _lastSelected.GetComponent<Button>() != null && _lastSelected.GetComponent<Button>().interactable)
            {
                EventSystem.current.SetSelectedGameObject(_lastSelected);
            }            
        }
        else
        {
            _lastSelected = EventSystem.current.currentSelectedGameObject;
        }

        if (Input.GetButtonDown("Jump") && !_spacebarPressed)
        {
            _spacebarPressed = true;
            _button.onClick.Invoke();
        }

        if (Input.GetButtonUp("Jump"))
        {
            _spacebarPressed = false;
        }
    }
}
