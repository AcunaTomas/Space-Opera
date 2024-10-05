using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;

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

    public GameObject meleeHurtBox;

    private int _randomAttackNum;

    public bool _phase2 = false;

    private bool _flipeable = true;
    private int _numMisilesGenerator;

    public GameObject _hackInteractor;

    public bool _hacked = false;

    public GameObject _weakSpot;

    private bool _weakSpotActive;



void Awake()
{
    intTimer = timer;
    animator = GetComponent<Animator>();
    player = GameObject.FindWithTag("Player");
    _playerPos = player.transform;
    //idleState.Setup(player, gameObject, animator);
    //chaseState.Setup(player, gameObject, animator);
    //attackState.Setup(player, gameObject, animator);
    //if(!state) state = idleState;
    GetRandomNum();
}

void FixedUpdate()
{
    if(_flipeable)Flip();

    //state.Do();
    
    timerAttack += Time.deltaTime;

    if (_first)
    {
        transform.position = Vector2.MoveTowards(transform.position, bossPosition1.transform.position, _speed * Time.deltaTime);
        
        if (transform.position.x == bossPosition1.transform.position.x)
        {
            if(!_attacking)
            {
                StartCoroutine(Ataque1());
            }
        }
    }
    if (_second)
    {
        transform.position = Vector2.MoveTowards(transform.position, bossPosition2.transform.position, _speed * Time.deltaTime);
        if (transform.position.x == bossPosition2.transform.position.x)
        {
            if (!_attacking)
            {
                StartCoroutine(Ataque2());
            }
        }
    }
    if (_third)
    {
        transform.position = Vector2.MoveTowards(transform.position, bossPosition3.transform.position, _speed * Time.deltaTime);
        if (transform.position.x == bossPosition3.transform.position.x)
        {
            if(!_attacking)
            {
                StartCoroutine(Ataque3());
            }  
        }
    }

    if (!_attacking && Vector2.Distance(transform.position, player.transform.position) < meleeDistance)
    {
        StartCoroutine(MeleeAttack());
    }


    GetMissileNumbers();

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
     meleeHurtBox.SetActive(true);
     Debug.Log("Melee");
     yield return new WaitForSeconds(1);
     _attacking = false;
     meleeHurtBox.SetActive(false);
     _speed = 0.5f;
 }

private IEnumerator Ataque1()
{
    _attacking = true;
    _bulletSource.localPosition = new Vector2(0.1f,0f);
    _lastRandomNum = 1;
    
    yield return new WaitForSeconds(0.3f);

    if (_phase2)
    {
        _randomAttackNum = Random.Range(1,3);
        if (_randomAttackNum == 1)
        {
            StartCoroutine(AtaqueMisiles(1.0f));
        }
        else
        {
            StartCoroutine(AtaqueRumba(1.0f, 3.0f, 60, false));
        }
    }
    else
    {
        StartCoroutine(AtaqueMisiles(1.0f));
    }

    
    yield return new WaitForSeconds(2);
    GetRandomNum();
    _attacking = false;
}
private IEnumerator Ataque2()
{
    _attacking = true;
    Debug.Log("Ataque2");
    _lastRandomNum = 2;
    _hackInteractor.SetActive(true);

    yield return new WaitForSeconds(1f);
    StartCoroutine(AtaqueElectricidad());


    yield return new WaitForSeconds(2);
    if (!_hacked)
    {
        GetRandomNum();
        _attacking = false;
    }
    else
    {
        yield return new WaitForSeconds(5);
        GetRandomNum();
        _attacking = false;
        _hacked = false;
    }
    
}
private IEnumerator Ataque3()
{
    _flipeable = false;
    GetComponent<SpriteRenderer>().flipX = true;
    _bulletSource.localPosition = new Vector2(-0.1f,0f);
    _attacking = true;
    _lastRandomNum = 3;

    yield return new WaitForSeconds(0.3f);
    
    if (_phase2)
    {
        _randomAttackNum = Random.Range(1,3);
        if (_randomAttackNum == 1)
        {
            StartCoroutine(AtaqueMisiles(1.0f));
        }
        else
        {
            StartCoroutine(AtaqueRumba(-3.0f, -1.0f, -60, true));
        }
    }
    else
    {
        StartCoroutine(AtaqueMisiles(1.0f));
    }

    yield return new WaitForSeconds(2);
    GetRandomNum();
    _attacking = false;
    _flipeable = true;
}


IEnumerator AtaqueRumba(float num1, float num2, int speed1, bool flip)
{
    var a = Instantiate(_rumbaRota, _bulletSource.position, transform.rotation);
    a.GetComponent<EnemyPatrol>()._speed = speed1;
    a.GetComponent<EnemyPatrol>().Impulse(num1, num2);
    a.GetComponent<SpriteRenderer>().flipX = flip;

    yield return new WaitForSeconds(0.3f);
    
    var b = Instantiate(_rumbaRota, _bulletSource.position, transform.rotation);
    b.GetComponent<EnemyPatrol>()._speed = speed1;
    b.GetComponent<EnemyPatrol>().Impulse(num1, num2);
    b.GetComponent<SpriteRenderer>().flipX = flip;

    yield return new WaitForSeconds(0.3f);

    var c = Instantiate(_rumba, _bulletSource.position, transform.rotation);
    c.GetComponent<EnemyPatrol>()._speed = 60;
    c.GetComponent<EnemyPatrol>().Impulse(num1, num2);
    c.GetComponent<SpriteRenderer>().flipX = false;

    yield return new WaitForSeconds(0.2f);

    c.GetComponent<EnemyPatrol>()._bossFight = false;
}

IEnumerator AtaqueMisiles(float _missileSpeed)
{
    
    int _lastNumMisiles = _numMisilesGenerator;

    if(_lastNumMisiles == 1)
    {
        Debug.Log("misiles1");
    }
    else if(_lastNumMisiles == 2)
    {
        Debug.Log("misiles2");
    }
    else if(_lastNumMisiles == 3)
    {
        Debug.Log("misiles3");
    }
    else if(_lastNumMisiles == 4)
    {
        Debug.Log("misiles4");
    }
    yield return new WaitForSeconds(0.5f);


    yield return new WaitUntil (() => _numMisilesGenerator !=_lastNumMisiles);

    _lastNumMisiles = _numMisilesGenerator;
    if(_lastNumMisiles == 1)
    {
        Debug.Log("misiles1");
    }
    else if(_lastNumMisiles == 2)
    {
        Debug.Log("misiles2");   
    }
    else if(_lastNumMisiles == 3)
    {
        Debug.Log("misiles3");    
    }
    else if(_lastNumMisiles == 4)
    {
        Debug.Log("misiles4");   
    }
}

void GetMissileNumbers()
{
    _numMisilesGenerator = Random.Range(1,5);
}

IEnumerator AtaqueElectricidad()
{
    if (!_hacked)
    {
        int newERandomNum = Random.Range(1,3);

        if (_phase2)
        {
            electricidadPhase2.SetActive(true);
            _hackInteractor.SetActive(false);
        }
        else if (newERandomNum == 1)
        {
            electricidadDER.SetActive(true);
            _hackInteractor.SetActive(false);
        }
        else
        {
            electricidadIZQ.SetActive(true);
            _hackInteractor.SetActive(false);
        }


        yield return new WaitForSeconds(1f);

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
}


public void GenerateWeakSpot()
{
    if (!_weakSpotActive)
    {
        float[] _valoresX = {-0.04f, -0.03f, -0.02f, -0.01f, 0f, 0.01f, 0.02f, 0.03f, 0.04f, 0.05f};
        float x = _valoresX[Random.Range(0, _valoresX.Length)];
        Debug.Log(x);
    
        float[] _valoresY = {-0.12f, -0.11f, -0.10f, -0.09f, -0.08f, -0.07f, -0.06f, -0.05f, -0.04f, -0.03f};
        float y = _valoresY[Random.Range(0, _valoresY.Length)];
        Debug.Log(y);

        Vector3 WeakSpotPosition = new Vector3 (x, y, 0);
        var a = Instantiate(_weakSpot, gameObject.transform.position, transform.rotation);
        a.transform.parent = gameObject.transform;
        a.transform.localPosition = WeakSpotPosition;
        a.transform.localScale = new Vector3 (0.14f, 0.14f, 0);

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

public void Flip()
{
    Vector3 rotation = transform.eulerAngles;
    if (transform.position.x > target.position.x) 
    {
        GetComponent<SpriteRenderer>().flipX = true;
        _bulletSource.localPosition = new Vector2(-0.1f,0f);
        // idleState.gameObject.GetComponent<BoxCollider2D>().offset = -idleState.gameObject.GetComponent<BoxCollider2D>().offset;
        // chaseState.gameObject.GetComponent<BoxCollider2D>().offset = -chaseState.gameObject.GetComponent<BoxCollider2D>().offset;
    }
    else
    {
        GetComponent<SpriteRenderer>().flipX = false;
        _bulletSource.localPosition = new Vector2(0.1f,0f);
        // idleState.gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(Math.Abs(idleState.gameObject.GetComponent<BoxCollider2D>().offset.x),
        //                                                                                 idleState.gameObject.GetComponent<BoxCollider2D>().offset.y);
        // chaseState.gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(Math.Abs(chaseState.gameObject.GetComponent<BoxCollider2D>().offset.x),
        //                                                                                 chaseState.gameObject.GetComponent<BoxCollider2D>().offset.y);
    }
    

    transform.eulerAngles = rotation;
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
