using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementsManager : MonoBehaviour, AchievementPersistance
{
    public static AchievementsManager INSTANCE;

    public bool ACHIEVEMENT01 = false;
    public int ENEMIES_LVL01 = 11;
    public int ENEMIES_KILLED = 0;

    public bool ACHIEVEMENT02 = false;
    public int ITEMS_TOTAL = 1;
    public int ITEMS_COLLECTED = 0;

    public bool ACHIEVEMENT03 = false;
    public int RADAR_COUNT = 0;

    public bool ACHIEVEMENT04 = false;
    public int BOMB_COUNT = 0;

    public bool ACHIEVEMENT05 = false;

    public bool ACHIEVEMENT06 = false;
    private int _animals_killed = 0;

    private bool _achievementEarned = false;
    private bool _dontDoIt = false;
    private bool _fold = false;
    private float _movementY;
    private float _time = 0f;

    public Image[] _actualImages;
    public Sprite[] _achievementsImageEarned;
    public Sprite[] _achievementsImageNotEarned;

    private Image _panel;
    private TextMeshProUGUI _title;
    private TextMeshProUGUI _description;

    private void Awake()
    {
        INSTANCE = this;
    }

    private void Start()
    {
        _movementY = GetComponent<RectTransform>().sizeDelta.y;
        _panel = transform.GetChild(0).GetComponent<Image>();
        _title = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        _description = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    public void ChangeSpritesMenu()
    {
        bool[] _bl = new bool[6] { ACHIEVEMENT01, ACHIEVEMENT02, ACHIEVEMENT03, ACHIEVEMENT04, ACHIEVEMENT05, ACHIEVEMENT06 };

        for (int i = 0; i < _bl.Length; i++)
        {
            if (_bl[i])
            {
                _actualImages[i].sprite = _achievementsImageEarned[i];
            }
            else
            {
                _actualImages[i].sprite = _achievementsImageNotEarned[i];
            }
        }
    }

    public IEnumerator AchievementEarned(bool var)
    {
        _fold = !_fold;
        float _time = 0f;
        float _initialY;
        float _finalY;

        if (var)
        {
            _initialY = transform.localPosition.y;
            _finalY = _initialY - _movementY;
        }
        else
        {
            _initialY = transform.localPosition.y;
            _finalY = _initialY + _movementY;
        }

        while (_time < 0.2f)
        {
            float _percentage = _time / 0.2f;
            float _newY = Mathf.Lerp(_initialY, _finalY, _percentage);
            transform.localPosition = new Vector2(transform.localPosition.x, _newY);

            _time += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = new Vector2(transform.localPosition.x, _finalY);

        yield return new WaitForSeconds(0.1f);

        
    }

    private void UpdateAchievementPanel(int n)
    {
        n--;
        _title.text = "�Logro desbloqueado!";
        _panel.sprite = _achievementsImageEarned[n];
    }

    public void Achievement01()
    {
        if (ACHIEVEMENT01)
        {
            return;
        }

        ENEMIES_KILLED++;
        if (ENEMIES_KILLED == ENEMIES_LVL01)
        {
            ACHIEVEMENT01 = true;
            _achievementEarned = true;
            UpdateAchievementPanel(1);
            _description.text = "M�talos a todos";
        }
        DataPersistentManager.INSTANCE.SaveAchievements();
    }

    public void Achievement02()
    {
        if (ACHIEVEMENT02)
        {
            return;
        }

        ITEMS_COLLECTED++;
        if (ITEMS_COLLECTED == ITEMS_TOTAL)
        {
            ACHIEVEMENT02 = true;
            _achievementEarned = true;
            UpdateAchievementPanel(2);
            _description.text = "Para la colecci�n";
        }
        DataPersistentManager.INSTANCE.SaveAchievements();
    }

    public void Achievement03()
    {
        if (ACHIEVEMENT03)
        {
            return;
        }

        RADAR_COUNT++;
        if (RADAR_COUNT == 5)
        {
            ACHIEVEMENT03 = true;
            _achievementEarned = true;
            UpdateAchievementPanel(3);
            _description.text = "Maestro rastreador";
        }
        DataPersistentManager.INSTANCE.SaveAchievements();
    }

    public void Achievement04()
    {
        if (ACHIEVEMENT04)
        {
            return;
        }

        BOMB_COUNT++;
        if (BOMB_COUNT == 5)
        {
            ACHIEVEMENT04 = true;
            _achievementEarned = true;
            UpdateAchievementPanel(4);
            _description.text = "Hombre bombardero";
        }
        DataPersistentManager.INSTANCE.SaveAchievements();
    }

    public void Achievement05()
    {
        if (ACHIEVEMENT05)
        {
            return;
        }

        ACHIEVEMENT05 = true;
        _achievementEarned = true;
        UpdateAchievementPanel(5);
        _description.text = "Nos vemos";
        DataPersistentManager.INSTANCE.SaveAchievements();
    }

    public void YouFuckingKilledAnimals()
    {
        _animals_killed++;
    }

    public void Achievement06()
    {
        if (ACHIEVEMENT06 || _animals_killed > 0)
        {
            return;
        }

        Debug.Log("oa");
        ACHIEVEMENT06 = true;
        _achievementEarned = true;
        UpdateAchievementPanel(6);
        _description.text = "Amante de la naturaleza";
        DataPersistentManager.INSTANCE.SaveAchievements();
    }

    void AchievementPersistance.LoadAchievements(AchievementsData data)
    {
        ACHIEVEMENT01 = data.ACHIEVEMENT01;
        ACHIEVEMENT02 = data.ACHIEVEMENT02;
        ACHIEVEMENT03 = data.ACHIEVEMENT03;
        ACHIEVEMENT04 = data.ACHIEVEMENT04;
        ACHIEVEMENT05 = data.ACHIEVEMENT05;
        ACHIEVEMENT06 = data.ACHIEVEMENT06;

        ITEMS_COLLECTED = data.ITEMS_COLLECTED;
        RADAR_COUNT = data.RADAR_COUNT;
        BOMB_COUNT = data.BOMB_COUNT;
    }

    void AchievementPersistance.SaveAchievements(AchievementsData data)
    {
        data.ACHIEVEMENT01 = ACHIEVEMENT01;
        data.ACHIEVEMENT02 = ACHIEVEMENT02;
        data.ACHIEVEMENT03 = ACHIEVEMENT03;
        data.ACHIEVEMENT04 = ACHIEVEMENT04;
        data.ACHIEVEMENT05 = ACHIEVEMENT05;
        data.ACHIEVEMENT06 = ACHIEVEMENT06;

        data.ITEMS_COLLECTED = ITEMS_COLLECTED;
        data.RADAR_COUNT = RADAR_COUNT;
        data.BOMB_COUNT = BOMB_COUNT;
    }

    private void Update()
    {
        if (!_achievementEarned)
        {
            return;
        }
        
        if (!_dontDoIt)
        {
            StartCoroutine(AchievementEarned(_fold));
            _dontDoIt = true;
        }

        if (_dontDoIt)
        {
            _time += Time.deltaTime;
            if (_time > 3f)
            {
                StartCoroutine(AchievementEarned(_fold));
                _dontDoIt = false;
                _achievementEarned = false;
                _time = 0f;
                return;
            }
        }
    }
}