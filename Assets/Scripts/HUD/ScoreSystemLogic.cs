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
    private bool _showKills, _showCollectables, _showDeaths, _showTime, _showNoHit, _readytoclose;
    [SerializeField] int _collectableCount;


    void Start()
    {
        _showing = false;
        _readytoclose = false;
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
            GameManager.INSTANCE.PAUSABLE = false;
            _scoreContainer.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text = _enemyKill + "/" + (_enemyCount - 1);
        }
        if(_showCollectables)
        {
            _scoreContainer.transform.GetChild(9).GetComponent<TextMeshProUGUI>().text = _collectableFound + "/" + _collectableCount;
        }
        if(_showDeaths)
        {
            _scoreContainer.transform.GetChild(10).GetComponent<TextMeshProUGUI>().text = _deathCount.ToString();
        }
        if(_showTime)
        {
            _scoreContainer.transform.GetChild(11).GetComponent<TextMeshProUGUI>().text = Mathf.Round(_totalTime).ToString() + "''";
        }
        if(_showNoHit) _scoreContainer.transform.GetChild(12).GetComponent<TextMeshProUGUI>().text = _noHit.ToString();
        if(_readytoclose && InputManager.INSTANCE.SubmitInput) StartCoroutine(Credits());
        if(InputManager.INSTANCE.SubmitHeld && _showKills) ShowAll();
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
        _readytoclose = true;
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

    IEnumerator Credits()
    {
        _readytoclose = false;
        yield return new WaitForSeconds(5f);
        ScenesManager.Instance.LoadNextScene("Credits");
    }

    public void PersistValues()
    {
        PersistentGameInfo._collectableFound = _collectableFound;
        PersistentGameInfo._deathCount = _deathCount;
        PersistentGameInfo._enemyKill = _enemyKill;
        PersistentGameInfo._enemyCount = _enemyCount;
        PersistentGameInfo._noHit = _noHit;
        PersistentGameInfo._progressTime = _progressTime;
        PersistentGameInfo._totalTime = _totalTime;
    }
}
