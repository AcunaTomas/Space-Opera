using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreSystemLogic : MonoBehaviour
{
    private float _totalTime, _progressTime;
    private bool _showing;
    private GameObject _scoreContainer;
    private Animator _scoreAnimator;
    private int _enemyCount, _enemyKill, _collectableFound, _deathCount;
    private bool _noHit;
    private bool _showKills, _showCollectables, _showDeaths, _showTime, _showNoHit;
    [SerializeField] int _collectableCount;


    void Start()
    {
        _showing = false;
        _noHit = true;
        _scoreContainer = transform.GetChild(0).gameObject;
        _scoreAnimator = GetComponent<Animator>();
        _scoreContainer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!_showing) _totalTime += Time.deltaTime;
        if(_showKills)
        {
            if(_progressTime<1f) _scoreContainer.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text = Mathf.Lerp(0f, _enemyKill, _progressTime) + "/" + _enemyCount;
            else _scoreContainer.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text = _enemyKill + "/" + _enemyCount;
        }
        if(_showCollectables)
        {
            if(_progressTime<2f) _scoreContainer.transform.GetChild(9).GetComponent<TextMeshProUGUI>().text = Mathf.Lerp(0f, _collectableFound, _progressTime/2) + "/" + _collectableCount;
            else _scoreContainer.transform.GetChild(9).GetComponent<TextMeshProUGUI>().text = _collectableFound + "/" + _collectableCount;
        }
        if(_showDeaths)
        {
           if(_progressTime<3f) _scoreContainer.transform.GetChild(10).GetComponent<TextMeshProUGUI>().text = Mathf.Lerp(0f, _deathCount, _progressTime/3).ToString();
           else _scoreContainer.transform.GetChild(10).GetComponent<TextMeshProUGUI>().text = _deathCount.ToString();
        }
        if(_showTime)
        {
            if(_progressTime<4f) _scoreContainer.transform.GetChild(11).GetComponent<TextMeshProUGUI>().text = Mathf.Lerp(0f, Mathf.Round(_totalTime), _progressTime/4).ToString() + "''";
            else _scoreContainer.transform.GetChild(11).GetComponent<TextMeshProUGUI>().text = Mathf.Round(_totalTime).ToString() + "''";
        }
        if(_showNoHit) _scoreContainer.transform.GetChild(12).GetComponent<TextMeshProUGUI>().text = _noHit.ToString();
        _progressTime += Time.deltaTime *2f;
        if(Input.GetButton("Submit") && _showKills) ShowAll();
    }

    public void ShowScore()
    {
        _showing = true;
        _scoreContainer.SetActive(true);
        _scoreAnimator.enabled = true;
    }


    //Counters
    public void EnemySpawn() {_enemyCount++;}
    public void EnemyKilled() {_enemyKill++;}
    public void CollectableFound() {_collectableFound++;}
    public void PlayerDead() {_deathCount++;}
    public void Hit() {_noHit = false;}

    //Bools
    public void ShowKills() {Reset();_showKills = true;}
    public void ShowCollectables() {_showCollectables = true;}
    public void ShowDeaths() {_showDeaths = true;}
    public void ShowTime() {_showTime = true;}
    public void ShowNoHit() {_showNoHit = true;}

    public void ShowAll()
    {
        _showKills = true;
        ShowCollectables();
        ShowDeaths();
        ShowTime();
        ShowNoHit();
        _scoreAnimator.Play("ScoreScreen_loop");
    }

    void Reset()
    {
        _progressTime = 0f;
    }

}
