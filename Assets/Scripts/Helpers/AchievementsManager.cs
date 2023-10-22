using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsManager : MonoBehaviour, AchievementPersistance
{
    public static AchievementsManager INSTANCE;

    public bool ACHIEVEMENT01 = false;
    public int ENEMIES_LVL01 = 10;
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

    private bool _achievementEarned = false;
    private bool _dontDoIt = false;
    private bool _fold = false;
    private float _movementY;
    private float _time = 0f;

    private void Awake()
    {
        INSTANCE = this;
    }

    private void Start()
    {
        _movementY = GetComponent<RectTransform>().sizeDelta.y;
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
        DataPersistentManager.INSTANCE.SaveAchievements();
    }

    public void Achievement06()
    {
        if (ACHIEVEMENT06)
        {
            return;
        }

        ACHIEVEMENT06 = true;
        _achievementEarned = true;
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

        ENEMIES_KILLED = data.ENEMIES_KILLED;
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

        data.ENEMIES_KILLED = ENEMIES_KILLED;
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
            if (_time > 2f)
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