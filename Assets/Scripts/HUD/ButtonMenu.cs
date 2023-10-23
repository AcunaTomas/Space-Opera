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
    [SerializeField]
    private Sprite[] _backgrounds;
    
    void Start()
    {
        _buttonText = GetComponentInChildren<TMP_Text>();

        try
        {
            _originalScale = _buttonText.transform.localScale;
        }
        catch (System.Exception e)
        {

        }

        _activeImage = GetComponent<Image>();
        _parent = transform.parent;
        _lastSelected = gameObject;
        _button = GetComponent<Button>();

        if (ACTIVE)
        {
            _activeImage.enabled = true;
            try
            {
                Vector3 newScale = _originalScale * 1.15f;
                _buttonText.transform.localScale = newScale;
            }
            catch (System.Exception e)
            {

            }
        }
    }

    public void OnPointerEnter()
    {
        for (int i = 0; i < _parent.childCount; i++)
        {
            Transform child = _parent.GetChild(i);
            if (child.GetComponent<ButtonMenu>().ACTIVE)
            {
                if (child.name == transform.name)
                {
                    break;
                }

                AudioManager.INSTANCE.PlayUISelect();
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

    public void OnPointerEnterPause()
    {
        for (int i = 0; i < _parent.childCount; i++)
        {
            Transform child = _parent.GetChild(i);
            if (child.GetComponent<ButtonMenu>().ACTIVE)
            {
                if (child.name == transform.name)
                {
                    break;
                }

                AudioManager.INSTANCE.PlayUISelect();
                child.GetComponent<ButtonMenu>().ACTIVE = false;
                child.GetComponent<Image>().sprite = _backgrounds[0];
                if (!EventSystem.current.alreadySelecting)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                }
                break;
            }
        }

        ACTIVE = true;
        _activeImage.sprite = _backgrounds[1];
        if (!EventSystem.current.alreadySelecting)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }

    private void OnEnable()
    {
        _spacebarPressed = false;
    }

    public void OnSelectedLevelMenu()
    {
        for (int i = 0; i < _parent.childCount; i++)
        {
            Transform child = _parent.GetChild(i);
            if (child.GetComponent<ButtonMenu>().ACTIVE)
            {
                if (child.name == transform.name)
                {
                    break;
                }

                AudioManager.INSTANCE.PlayUISelect();
                child.GetComponent<ButtonMenu>().ACTIVE = false;
                child.GetChild(0).gameObject.SetActive(false);
                child.GetChild(1).gameObject.SetActive(false);
                child.GetComponentInChildren<TMP_Text>().transform.localScale = _originalScale;
                if (!EventSystem.current.alreadySelecting)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                }
                break;
            }
        }

        ACTIVE = true;
        _parent.GetComponent<TextSelectLevel>().ChangeText(transform.GetChild(1).GetComponent<TextMeshProUGUI>(), transform.name);
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        if (!EventSystem.current.alreadySelecting)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }

    private void Update()
    {
        if (!ACTIVE)
        {
            return;
        }

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            if (_lastSelected.gameObject.activeSelf && _lastSelected.GetComponent<Button>() != null)
            {
                EventSystem.current.SetSelectedGameObject(_lastSelected);
            }            
        }
        else
        {
            _lastSelected = EventSystem.current.currentSelectedGameObject;
        }

        if ((Input.GetButtonDown("Jump") || Input.GetButtonDown("Submit")) && !_spacebarPressed)
        {
            if (!_button.interactable)
            {
                return;
            }
            _spacebarPressed = true;
            _button.onClick.Invoke();
        }

        if (Input.GetButtonUp("Jump") || Input.GetButtonUp("Submit"))
        {
            _spacebarPressed = false;
        }
    }
}
