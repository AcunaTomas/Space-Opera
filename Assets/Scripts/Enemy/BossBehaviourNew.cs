using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;


public class BossBehaviourNew : MonoBehaviour
{
    public Transform _playerPos;
    public float _speed;
    public float _distanciaFrenado;
    public float _distanciaRetroceso;

    public GameObject _rumba;

    public GameObject _rumbaRota;
    private float _tiempo;

    private SpriteRenderer _spriteRenderer;


    public float attackDistance;
    public float moveSpeed;
    public float timer;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange;
    public GameObject hotZone;
    public GameObject triggerArea;
    public Transform _bulletSource;

    private Animator animator;
    private float distance;
    private bool attackMode;
    [HideInInspector] public bool cooling;
    private float intTimer;
    private GameObject player;
    [HideInInspector] public float timerAttack;
    public float attackDelay = 1.5f;
        //EnemyState
    [Header("Estados")]
    public BossIdleState idleState;
    public BossChaseState chaseState;
    public BossAttackState attackState;
    [SerializeField] State state;


    //NewShit


    [Header("Checkpoints")]
    public GameObject bossPosition1;
    public GameObject bossPosition2;
    public GameObject bossPosition3;

    [Header("Electricity")]

    public GameObject electricidadIZQ;
    public GameObject electricidadDER;
    public GameObject electricidadPhase2;

    private bool _first = false;
    private bool _second = false;
    private bool _third = false;

    private bool _attacking = false;

    private int _randomNum;
    private int _lastRandomNum = 0;

    public float meleeDistance = 0;

    private bool _meleeCooling = false;

    public GameObject meleeHurtBox;

    private int _randomAttackNum;

    public bool _phase2 = false;
    private int _numMisilesGenerator;

    public GameObject _hackInteractor;

    public bool _hacked = false;

    public GameObject _weakSpot;

    private bool _weakSpotActive;

    public GameObject _platform1;
    public GameObject _platform2;


    public Material _normalMaterial;
    public Material _hitMaterial;

    public bool _flipping = false;

    [Header("Missile Source")]

    public Transform _m1Source;
    public Transform _m2Source;
    public Transform _m3Source;
    public Transform _m4Source;


    public GameObject _missile;

    public GameObject _missileEffect;

    private List<GameObject> killAll;


    [Header("Sounds")]
    public EnemySoundsController soundsController;
    private BossSoundsController playSounds;


void Awake()
{
    intTimer = timer;
    animator = GetComponent<Animator>();
    player = GameObject.FindWithTag("Player");
    soundsController = GetComponent<EnemySoundsController>();
    playSounds = GetComponent<BossSoundsController>();
    _playerPos = player.transform;
    //idleState.Setup(player, gameObject, animator);
    //chaseState.Setup(player, gameObject, animator);
    //attackState.Setup(player, gameObject, animator);
    //if(!state) state = idleState;
    PersistentGameInfo.skipTutorial = true;
    killAll = new List <GameObject>();
    GetRandomNum();
}

/// <summary>
/// This function is called when the object becomes enabled and active.
/// </summary>
private void OnEnable()
{
    soundsController.UpdateParameter("EnemyMode", 0);
}

void FixedUpdate()
{

    //state.Do();
    Debug.Log("1" + _first + "\n" + "3" + _third);
    
    timerAttack += Time.deltaTime;

    if (_first)
    {
        if (GetComponent<SpriteRenderer>().flipX == false && !_attacking)
        {
            if (!_flipping)
            {
                Flip(true);
            }
        }

        if (!_flipping)
        {
            transform.position = Vector2.MoveTowards(transform.position, bossPosition1.transform.position, _speed * Time.deltaTime);
            animator.SetBool("Run", true);
            if (transform.position.x == bossPosition1.transform.position.x)
            {
                animator.SetBool("Run", false);
                if(!_attacking)
                {
                    StartCoroutine(Ataque1());
                }
            }

        }
        
    }
    if (_second)
    {
        transform.position = Vector2.MoveTowards(transform.position, bossPosition2.transform.position, _speed * Time.deltaTime);
        animator.SetBool("Run", true);
        if (transform.position.x == bossPosition2.transform.position.x)
        {
            animator.SetBool("Run", false);
            if (!_attacking)
            {
                StartCoroutine(Ataque2());
            }
        }
    }
    if (_third)
    {
        if (GetComponent<SpriteRenderer>().flipX == true && !_attacking)
        {
            if (!_flipping)
            {
                Flip(false);
            }
        }
        if (!_flipping)
        {
            transform.position = Vector2.MoveTowards(transform.position, bossPosition3.transform.position, _speed * Time.deltaTime);
            animator.SetBool("Run", true);
            if (transform.position.x == bossPosition3.transform.position.x)
            {
                animator.SetBool("Run", false);
                if(!_attacking)
                {
                    StartCoroutine(Ataque3());
                }  
            }
        }
        
    }

    if (!_attacking && Vector2.Distance(transform.position, player.transform.position) < meleeDistance)
    {
        if (!_meleeCooling)
        _meleeCooling = true;
        StartCoroutine(MeleeAttack());
    }


    GetMissileNumbers();

}

public void GetHit()
{
    StartCoroutine(GotHit());
}

IEnumerator GotHit()
{
    gameObject.GetComponent<SpriteRenderer>().material = _hitMaterial;

    yield return new WaitForSeconds(0.3f);

    gameObject.GetComponent<SpriteRenderer>().material = _normalMaterial;
}


void GetRandomNum()
{

    _randomNum = Random.Range(1,4);

    if (_randomNum == 1 && _lastRandomNum != 1)
    {
        _first = true;
        _second = false;
        _third = false;
        target = bossPosition1.transform;
    }
    else if (_randomNum == 2 && _lastRandomNum != 2)
    {
        _first = false;
        _second = true;
        _third = false;
        target = bossPosition2.transform;
    }
    else if (_randomNum == 3 && _lastRandomNum != 3)
    {
        _first = false;
        _second = false;
        _third = true;
        target = bossPosition3.transform;
    }

    if (_randomNum == _lastRandomNum)
    {
        GetRandomNum();
    }

    // if (_lastRandomNum == 1)
    // {
    //     _randomNum = Random.Range(2,4);
    // }
    // else if (_lastRandomNum == 2)
    // {
    //     _randomNum = Random.Range(1,4);
    // }
    // else if (_lastRandomNum == 3)
    // {
    //     _randomNum = Random.Range(1,3);
    // }

    
}


 private IEnumerator MeleeAttack()
 {
     _attacking = true;
     animator.SetTrigger("melee");

     yield return new WaitForSeconds(0.6f);

     meleeHurtBox.SetActive(true);

     yield return new WaitForSeconds(0.3f);

     _attacking = false;
     meleeHurtBox.SetActive(false);

     yield return new WaitForSeconds(5f);
     
    _meleeCooling = false;
 }

private IEnumerator Ataque1()
{
    soundsController.UpdateParameter("EnemyMode", 1);
    _bulletSource.localPosition = new Vector2(0.1f,0f);
    _lastRandomNum = 1;

    Flip(false);
    _attacking = true;
    yield return new WaitUntil(() => !_flipping);

    
    
    Debug.Log("Flip ended");

    yield return new WaitForSeconds(0.3f);

    if (_phase2)
    {
        _randomAttackNum = Random.Range(1,3);
        if (_randomAttackNum == 1)
        {
            animator.SetTrigger("missile");
            StartCoroutine(AtaqueMisiles(1.0f));
        }
        else
        {
            animator.SetBool("Roomba", true);
            StartCoroutine(AtaqueRumba(1.0f, 3.0f, 60, false));
        }
    }
    else
    {
        animator.SetTrigger("missile");
        StartCoroutine(AtaqueMisiles(1.0f));
    }

    
    yield return new WaitForSeconds(2.5f);
    animator.SetBool("missileIdle", false);
    animator.SetBool("Roomba", false);

    yield return new WaitForSeconds(0.5f);
    GetRandomNum();
    _attacking = false;
    _flipping = false;
}
private IEnumerator Ataque2()
{
    _attacking = true;
    Debug.Log("Ataque2");
    _lastRandomNum = 2;
    _hackInteractor.SetActive(true);
    _hackInteractor.GetComponent<Animator>().SetTrigger("show");

    yield return new WaitForSeconds(1f);
    StartCoroutine(AtaqueElectricidad());


    yield return new WaitForSeconds(2f);
    
    if (!_hacked)
    {
        animator.SetBool("Electricity", false);
        GetRandomNum();
        _attacking = false;
    }
    else
    {
        yield return new WaitForSeconds(4);

        animator.SetTrigger("restore");
        playSounds.PlayRechargeSound();
        animator.SetBool("Electricity", false);

        yield return new WaitForSeconds(1);
        GetRandomNum();
        _attacking = false;
        _hacked = false;
    }

    _flipping = false;
    
}
private IEnumerator Ataque3()
{
    _bulletSource.localPosition = new Vector2(-0.1f,0f);
    _lastRandomNum = 3;

    Flip(true);

    _attacking = true;

    yield return new WaitUntil(() => !_flipping);

    Debug.Log("Flip ended");

    yield return new WaitForSeconds(0.3f);
    
    if (_phase2)
    {
        _randomAttackNum = Random.Range(1,3);
        if (_randomAttackNum == 1)
        {
            animator.SetTrigger("missile");
            StartCoroutine(AtaqueMisiles(1.0f));
        }
        else
        {
            animator.SetBool("Roomba", true);
            StartCoroutine(AtaqueRumba(-3.0f, 3.0f, -60, true));
        }
    }
    else
    {
        animator.SetTrigger("missile");
        StartCoroutine(AtaqueMisiles(1.0f));
    }

    yield return new WaitForSeconds(2.5f);
    animator.SetBool("missileIdle", false);
    animator.SetBool("Roomba", false);

    yield return new WaitForSeconds(0.5f);
    GetRandomNum();
    _attacking = false;
    _flipping = false;
}


IEnumerator AtaqueRumba(float num1, float num2, int speed1, bool flip)
{
    yield return new WaitForSeconds(1.7f);
    var a = Instantiate(_rumbaRota, _bulletSource.position, transform.rotation);
    a.GetComponent<EnemyPatrol>()._speed = speed1;
    a.GetComponent<EnemyPatrol>().Impulse(num1, num2);
    a.GetComponent<SpriteRenderer>().flipX = flip;


    var b = Instantiate(_rumbaRota, _bulletSource.position, transform.rotation);
    b.GetComponent<EnemyPatrol>()._speed = speed1;
    b.GetComponent<EnemyPatrol>().Impulse(num1, num2);
    b.GetComponent<SpriteRenderer>().flipX = flip;


    var c = Instantiate(_rumba, _bulletSource.position, transform.rotation);
    c.GetComponent<EnemyPatrol>()._speed = 60;
    c.GetComponent<EnemyPatrol>().Impulse(num1, num2);
    c.GetComponent<SpriteRenderer>().flipX = false;
    killAll.Add(c);

    yield return new WaitForSeconds(0.7f);

    c.GetComponent<EnemyPatrol>()._bossFight = false;
}

public void MissileIdleBool()
{
    animator.SetBool("missileIdle", true);
}
IEnumerator AtaqueMisiles(float _missileSpeed)
{
    
    int _lastNumMisiles = _numMisilesGenerator;

    if(_lastNumMisiles == 1)
    {
        animator.SetTrigger("m"+ _lastNumMisiles);
        yield return new WaitForSeconds(1.2f);
        var a = Instantiate(_missile, _m1Source);
        var b = Instantiate(_missileEffect, _m1Source);
        if (GetComponent<SpriteRenderer>().flipX == false)
        {
            a.GetComponent<MissileController2>()._flip = false;
            b.GetComponent<SpriteRenderer>().flipX = false;
            b.GetComponent<Transform>().localPosition = new Vector2(-0.2f, 0);
        }
        else
        {
            a.GetComponent<MissileController2>()._flip = true;
            b.GetComponent<SpriteRenderer>().flipX = true;
            b.GetComponent<Transform>().localPosition = new Vector2(0.2f, 0);
        }

        
    }
    else if(_lastNumMisiles == 2)
    {
        animator.SetTrigger("m" + _lastNumMisiles);
        yield return new WaitForSeconds(1.2f);
        var a = Instantiate(_missile, _m2Source);
        var b = Instantiate(_missileEffect, _m2Source);
        if (GetComponent<SpriteRenderer>().flipX == false)
        {
            a.GetComponent<MissileController2>()._flip = false;
            b.GetComponent<SpriteRenderer>().flipX = false;
            b.GetComponent<Transform>().localPosition = new Vector2(-0.1f, 0);
        }
        else
        {
            a.GetComponent<MissileController2>()._flip = true;
            b.GetComponent<SpriteRenderer>().flipX = true;
            b.GetComponent<Transform>().localPosition = new Vector2(0.1f, 0);
        }
    }
    else if(_lastNumMisiles == 3)
    {
        animator.SetTrigger("m" + _lastNumMisiles);
        yield return new WaitForSeconds(1.2f);
        var a = Instantiate(_missile, _m3Source);
        var b = Instantiate(_missileEffect, _m3Source);
        if (GetComponent<SpriteRenderer>().flipX == false)
        {
            a.GetComponent<MissileController2>()._flip = false;
            b.GetComponent<SpriteRenderer>().flipX = false;
            b.GetComponent<Transform>().localPosition = new Vector2(0.066f, 0);
        }
        else
        {
            a.GetComponent<MissileController2>()._flip = true;
            b.GetComponent<SpriteRenderer>().flipX = true;
            b.GetComponent<Transform>().localPosition = new Vector2(-0.066f, 0);
        }
    }
    else if(_lastNumMisiles == 4)
    {
        animator.SetTrigger("m" + _lastNumMisiles);
        yield return new WaitForSeconds(1.2f);
        var a = Instantiate(_missile, _m4Source);
        var b = Instantiate(_missileEffect, _m4Source);
        if (GetComponent<SpriteRenderer>().flipX == false)
        {
            a.GetComponent<MissileController2>()._flip = false;
            b.GetComponent<SpriteRenderer>().flipX = false;
            b.GetComponent<Transform>().localPosition = new Vector2(0.085f, -0.02f);
        }
        else
        {
            a.GetComponent<MissileController2>()._flip = true;
            b.GetComponent<SpriteRenderer>().flipX = true;
            b.GetComponent<Transform>().localPosition = new Vector2(-0.085f, -0.02f);
        }
    }
    yield return new WaitForSeconds(0.5f);


    yield return new WaitUntil (() => _numMisilesGenerator !=_lastNumMisiles);

    _lastNumMisiles = _numMisilesGenerator;
    if(_lastNumMisiles == 1)
    {
        animator.SetTrigger("m" + _lastNumMisiles);
        yield return new WaitForSeconds(0.8f);
        var a = Instantiate(_missile, _m1Source);
        var b = Instantiate(_missileEffect, _m1Source);
        if (GetComponent<SpriteRenderer>().flipX == false)
        {
            a.GetComponent<MissileController2>()._flip = false;
            b.GetComponent<SpriteRenderer>().flipX = false;
            b.GetComponent<Transform>().localPosition = new Vector2(-0.2f, 0);
        }
        else
        {
            a.GetComponent<MissileController2>()._flip = true;
            b.GetComponent<SpriteRenderer>().flipX = true;
            b.GetComponent<Transform>().localPosition = new Vector2(0.2f, 0);
        }
    }
    else if(_lastNumMisiles == 2)
    {
        animator.SetTrigger("m" + _lastNumMisiles);  
        yield return new WaitForSeconds(0.8f);
        var a = Instantiate(_missile, _m2Source);
        var b = Instantiate(_missileEffect, _m2Source);
        if (GetComponent<SpriteRenderer>().flipX == false)
        {
            a.GetComponent<MissileController2>()._flip = false;
            b.GetComponent<SpriteRenderer>().flipX = false;
            b.GetComponent<Transform>().localPosition = new Vector2(-0.1f, 0);
        }
        else
        {
            a.GetComponent<MissileController2>()._flip = true;
            b.GetComponent<SpriteRenderer>().flipX = true;
            b.GetComponent<Transform>().localPosition = new Vector2(0.1f, 0);
        }
    }
    else if(_lastNumMisiles == 3)
    {  
        animator.SetTrigger("m" + _lastNumMisiles);
        yield return new WaitForSeconds(0.8f);
        var a = Instantiate(_missile, _m3Source);
        var b = Instantiate(_missileEffect, _m3Source);
        if (GetComponent<SpriteRenderer>().flipX == false)
        {
            a.GetComponent<MissileController2>()._flip = false;
            b.GetComponent<SpriteRenderer>().flipX = false;
            b.GetComponent<Transform>().localPosition = new Vector2(0.066f, 0);
        }
        else
        {
            a.GetComponent<MissileController2>()._flip = true;
            b.GetComponent<SpriteRenderer>().flipX = true;
            b.GetComponent<Transform>().localPosition = new Vector2(-0.066f, 0);
        }
    }
    else if(_lastNumMisiles == 4)
    {   
        animator.SetTrigger("m" + _lastNumMisiles);
        yield return new WaitForSeconds(0.8f);
        var a = Instantiate(_missile, _m4Source);
        var b = Instantiate(_missileEffect, _m4Source);
        if (GetComponent<SpriteRenderer>().flipX == false)
        {
            a.GetComponent<MissileController2>()._flip = false;
            b.GetComponent<SpriteRenderer>().flipX = false;
            b.GetComponent<Transform>().localPosition = new Vector2(0.085f, -0.02f);
        }
        else
        {
            a.GetComponent<MissileController2>()._flip = true;
            b.GetComponent<SpriteRenderer>().flipX = true;
            b.GetComponent<Transform>().localPosition = new Vector2(-0.085f, -0.02f);
        }
    }

}

void GetMissileNumbers()
{
    _numMisilesGenerator = Random.Range(1,5);
}

IEnumerator AtaqueElectricidad()
{
    animator.SetBool("Electricity", true);
    yield return new WaitForSeconds(1.4f);
    if (!_hacked)
    {
        int newERandomNum = Random.Range(1,3);

        if (_phase2)
        {
            electricidadPhase2.SetActive(true);
            _hackInteractor.GetComponent<Animator>().SetTrigger("hide");
            yield return new WaitForSeconds(0.1f);
            _hackInteractor.SetActive(false);
        }
        else if (newERandomNum == 1)
        {
            electricidadDER.SetActive(true);
            _hackInteractor.GetComponent<Animator>().SetTrigger("hide");
            yield return new WaitForSeconds(0.1f);
            _hackInteractor.SetActive(false);
        }
        else
        {
            electricidadIZQ.SetActive(true);
            _hackInteractor.GetComponent<Animator>().SetTrigger("hide");
            yield return new WaitForSeconds(0.1f);
            _hackInteractor.SetActive(false);
        }


        yield return new WaitForSeconds(0.5f);

        electricidadPhase2.SetActive(false);
        electricidadIZQ.SetActive(false);
        electricidadDER.SetActive(false);
    }
    else
    {
        _hackInteractor.SetActive(false);
        Debug.Log("THE BOSS WAS STUNNED");
        yield return new WaitForSeconds(1f);
    }
    
}


public void HackBoss()
{
    _hacked = true;
    animator.SetTrigger("hacked");
    playSounds.PlayHackingSound();
}


public void GenerateWeakSpot()
{
    if (!_weakSpotActive)
    {
        float[] _valoresX = {-0.38f, -0.18f, 0.05f, 0.2f, 0.34f, 0.47f};
        float x = _valoresX[Random.Range(0, _valoresX.Length)];

        float[] _valoresY = {-0.44f, -0.28f, -0.16f, -0.02f, 0.1f};
        float y = _valoresY[Random.Range(0, _valoresY.Length)];

        Vector3 WeakSpotPosition = new Vector3 (x, y, 0);
        var a = Instantiate(_weakSpot, gameObject.transform.position, transform.rotation);
        a.transform.parent = gameObject.transform;
        a.transform.localPosition = WeakSpotPosition;
        a.transform.localScale = new Vector3 (1f, 1f, 0);

        _weakSpotActive = true;

        StartCoroutine(DestroyWeakSpot(a));
    }
}

private IEnumerator DestroyWeakSpot(GameObject a)
{
    yield return new WaitForSeconds(4);
    Destroy(a);
    _weakSpotActive = false;
}


public void TriggerCooling()
{
    cooling = true;
}

public void Flip(bool left)
{
    if (!_attacking)
    {
        if (!left)
        {
            _flipping = true;
            animator.SetTrigger("flip");
        }
        else
        {
            _flipping = true;
            animator.SetTrigger("flip");
        }
    }
    
}
public void Flip2LaSecuela()
{
    Vector3 rotation = transform.eulerAngles;
    if (GetComponent<SpriteRenderer>().flipX == false)
    {
        GetComponent<SpriteRenderer>().flipX = true;
        _platform1.GetComponent<Transform>().localPosition = new Vector2(0.73f,0.175f);
        _platform2.GetComponent<Transform>().localPosition = new Vector2(0.027f,0.175f);
        _hackInteractor.GetComponent<Transform>().localPosition = new Vector2(-0.11f, 0.41f);
        _hackInteractor.GetComponent<SpriteRenderer>().flipX = true;

        _m1Source.localPosition = new Vector2(0.3f,0.165f);
        _m2Source.localPosition = new Vector2(0.2f,-0.78f);
        _m3Source.localPosition = new Vector2(-1.22f,-0.06f);
        _m4Source.localPosition = new Vector2(-1.21f,-0.47f);

        // idleState.gameObject.GetComponent<BoxCollider2D>().offset = -idleState.gameObject.GetComponent<BoxCollider2D>().offset;
        // chaseState.gameObject.GetComponent<BoxCollider2D>().offset = -chaseState.gameObject.GetComponent<BoxCollider2D>().offset;
    }
    else
    {
        GetComponent<SpriteRenderer>().flipX = false;
        _platform1.GetComponent<Transform>().localPosition = new Vector2(0f,0.175f);
        _platform2.GetComponent<Transform>().localPosition = new Vector2(0.39f,0.175f);
        _hackInteractor.GetComponent<Transform>().localPosition = new Vector2(0.11f, 0.41f);
        _hackInteractor.GetComponent<SpriteRenderer>().flipX = false;

        _m1Source.localPosition = new Vector2(-0.3f,0.165f);
        _m2Source.localPosition = new Vector2(-0.2f,-0.78f);
        _m3Source.localPosition = new Vector2(1.22f,-0.06f);
        _m4Source.localPosition = new Vector2(1.21f,-0.47f);

        // idleState.gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(Math.Abs(idleState.gameObject.GetComponent<BoxCollider2D>().offset.x),
        //                                                                                 idleState.gameObject.GetComponent<BoxCollider2D>().offset.y);
        // chaseState.gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(Math.Abs(chaseState.gameObject.GetComponent<BoxCollider2D>().offset.x),
        //                                                                                 chaseState.gameObject.GetComponent<BoxCollider2D>().offset.y);
    }
    

    transform.eulerAngles = rotation;
}

public void FlipFalse()
{
    _flipping = false;
}

public void BossDeath()
{
    foreach(var romba in killAll)
    {
        romba.SetActive(false);
    }
    PersistentGameInfo.skipTutorial = false;
    soundsController.UpdateParameter("EnemyMode", 2);
}
//public void ChangeState(State newState)
//{
//    state = newState;
//}

// public State GetState()
// {
//     return state;
// }

}
