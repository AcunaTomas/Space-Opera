using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//hola
//hola :D
//hi 0_0
public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    /*[HideInInspector]*/ public int currentHealth;
    private GameObject player;
    private int attackDamage;

    [SerializeField]
    private bool _Inmortal = false;
    private bool isDead, isFromWave;

    public bool _MakeFunctionCallOnDie;
    
    [SerializeField]
    private UnityEvent _callWhat;
    private UpdateBars _ub;
    private UpdateBars _ub2;
    public bool _coolingHit = false;
    public bool _coolingHit2 = false;

    private SpriteRenderer _spriteRenderer;

    public EnemyType _enemyType;
    public enum EnemyType
    {
        melee,
        gun,
        simple,
        shoot,
        boss,
        shield,
        gunTower,
        clone,
        squirrel,

        newBoss
    }

    [SerializeField]
    private GameObject _objectToDisable;

    [Header("KnockBack")]
    [SerializeField] private float knockbackRecibido = 0f; //.0.2f va
    [SerializeField] private float knockbackDado = 0f; //.1f va


    void Start()
    {

        switch (_enemyType)
        {
            default:

                break;
        }


        currentHealth = maxHealth;
        GameObject player = GameManager.INSTANCE.PLAYER;
        
        _ub =  UIManager.INSTANCE.LifeBar;
        _ub2 = UIManager.INSTANCE.LifeBar2;


        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage, Vector2 sourcePosition)
    {
        DoKnockback(sourcePosition);
        if (!_coolingHit && !isDead)
        {
            currentHealth -= damage;
            attackDamage = damage;
            animator.SetTrigger("Hurt");
            
            //CameraController.Instance.ScreenShake(.3f, .1f);

            if (_enemyType == EnemyType.melee)
            {
                GetComponent<EnemyBehaviour2>().enabled = false;
            }

            if (_enemyType == EnemyType.gun)
            {
                GetComponent<EnemyShooting2>().enabled = false;
            }

            if (_enemyType == EnemyType.gunTower)
            {
                GetComponent<EnemyShootingDown>().enabled = false;
            }
            if (_enemyType == EnemyType.clone)
            {
                GetComponent<EnemyCloneBehaviour>().enabled = false;
            }

            if (_enemyType == EnemyType.squirrel)
            {
                if(currentHealth < 200) GetComponent<SquireBehaviour>().enabled = false;
            }


            if (_enemyType == EnemyType.simple)
            {
                GetComponent<EnemyPatrol>()._waiting = false;
                GetComponent<EnemyPatrol>().ChangeSpeed();
                animator.SetTrigger("Hurt");
            }

            //AudioManager.INSTANCE.PlayEnemyHit();

            if (currentHealth <= 0 && !_Inmortal) 
            {   
                Die();
            }
           
            StartCoroutine(StartCooldownCH1());
            /*
            if (_spriteRenderer.flipX == true)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0), ForceMode2D.Impulse);
            }
            else
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0), ForceMode2D.Impulse);
            }
            */
        }
    }

    public void TakeDamage2(int damage, Vector2 sourcePosition)
    {
        DoKnockback(sourcePosition);
        if (!_coolingHit2)
        {
            currentHealth -= damage;
            attackDamage = damage;
            animator.SetTrigger("Hurt");

            if (_enemyType == EnemyType.melee)
            {
                GetComponent<EnemyBehaviour2>().enabled = false;
            }

            if (_enemyType == EnemyType.gun)
            {
                GetComponent<EnemyShooting2>().enabled = false;
            }

            if (_enemyType == EnemyType.gunTower)
            {
                GetComponent<EnemyShootingDown>().enabled = false;
            }
            if (_enemyType == EnemyType.clone)
            {
                GetComponent<EnemyCloneBehaviour>().enabled = false;
            }
            if (_enemyType == EnemyType.squirrel)
            {
                GetComponent<SquireBehaviour>().enabled = false;
            }


            //AudioManager.INSTANCE.PlayEnemyHit();

            if (currentHealth <= 0 && !_Inmortal) 
            {   
                Die();
            }
           
            StartCoroutine(StartCooldownCH2());
            
        }
    }

    private IEnumerator StartCooldownCH1()
    {
        _coolingHit = true;
        yield return new WaitForSeconds(0.3f);
        _coolingHit = false;
    }
    private IEnumerator StartCooldownCH2()
    {
        _coolingHit2 = true;
        yield return new WaitForSeconds(0.6f);
        _coolingHit2 = false;
    }
    public void changeCallback(UnityEvent a)
    {
        _callWhat = a;
    }

    public void Die()
    {
        if (GameManager.INSTANCE.LEVEL == 1)
        {
            //AchievementsManager.INSTANCE.Achievement01();
        }

        if (GameManager.INSTANCE.LEVEL == 3)
        {
            //AchievementsManager.INSTANCE.YouFuckingKilledAnimals();
        }
        isDead = true;
        if(isFromWave) //Logica oleada
        {
            WaveLogic wl = FindAnyObjectByType<WaveLogic>();
            wl.OnEnemyKilled();
        }
        
        if (_enemyType == EnemyType.boss)
        {
            animator.SetTrigger("Die");
            animator.SetBool("Died", true);
            GetComponent<BossController>().enabled = false;
            GetComponentInChildren<HurtBoxFly>().enabled = false;
            AudioController.instance.PlayOneShot(FMODEvents.instance.EnemiesDead, this.transform.position);
        }

        if (_enemyType == EnemyType.shoot)
        {
            animator.SetTrigger("Die");
            GetComponent<EnemyFireBehaviour>().enabled = false;
            AudioController.instance.PlayOneShot(FMODEvents.instance.EnemiesDead, this.transform.position);

        }

        if (_enemyType == EnemyType.gunTower)
        {
            animator.SetTrigger("Die");
            GetComponent<EnemyShootingDown>().enabled = false;
            AudioController.instance.PlayOneShot(FMODEvents.instance.EnemiesDead, this.transform.position);
        }

        if (_enemyType == EnemyType.simple)
        {
            animator.SetTrigger("Die");
            animator.SetBool("Dead", true);
            GetComponent<EnemyPatrol>().enabled = false;
            GetComponentInChildren<HurtBox>().enabled = false;
            AudioController.instance.PlayOneShot(FMODEvents.instance.EnemiesDead, this.transform.position);

            _objectToDisable.SetActive(false);


        }
        
        if (_enemyType == EnemyType.shield)
        {
            animator.SetTrigger("Die");
            GetComponent<EnemyShieldBehaviour>().enabled = false;
            AudioController.instance.PlayOneShot(FMODEvents.instance.EnemiesDead, this.transform.position);

            _objectToDisable.SetActive(false);
        }

        if (_enemyType == EnemyType.clone)
        {
            GetComponent<EnemyCloneBehaviour>().enabled = false;
            GetComponent<Animator>().SetBool("CantTransition", true);
        }
        if (_enemyType == EnemyType.squirrel)
        {
            GetComponent<SquireBehaviour>().enabled = false;
        }

        animator.SetTrigger("Die");
        FindObjectOfType<EnemySoundsController>().UpdateParameter("EnemyMode", 2);
        
        try
        {
            _ub.EnergyPlusOne();
            _ub2.EnergyPlusOne();    
        }
        catch (System.Exception)
        {
            
        }

        Destroy(GetComponent<Rigidbody2D>());
        GetComponent<BoxCollider2D>().enabled = false;

        if (_enemyType == EnemyType.melee)
        {
            GetComponent<EnemyBehaviour2>().enabled = false;
            //GetComponentInChildren<HotZoneCheck>().enabled = false;
            //GetComponentInChildren<TriggerAreaCheck>().enabled = false;

            StartCoroutine(AfterDie());

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            //GetComponentInChildren<HotZoneCheck>().gameObject.SetActive(false);
            //GetComponentInChildren<TriggerAreaCheck>().gameObject.SetActive(false);
            GetComponent<Animator>().SetBool("CantTransition", true);

            gameObject.layer = LayerMask.NameToLayer("Default");
        }

        if (_enemyType == EnemyType.gun)
        {
            GetComponent<EnemyShooting2>().enabled = false;
            animator.SetBool("isDead", true);
        }

        try
        {
            GetComponentInChildren<HotZoneCheckShooting>().enabled = false;
        }
        catch(System.Exception)
        {
        }

        if (_MakeFunctionCallOnDie)
        {
            _callWhat.Invoke();
        }

        this.enabled = false;
    }

    public void ActivateBehaviour()
    {
        if(isDead == false)
        {
            if (_enemyType == EnemyType.melee)
            {
                GetComponent<EnemyBehaviour2>().enabled = true;
            }

            if (_enemyType == EnemyType.gun)
            {
                GetComponent<EnemyShooting2>().enabled = true;
            }

            if (_enemyType == EnemyType.gunTower)
            {
                GetComponent<EnemyShootingDown>().enabled = true;
            }
            if (_enemyType == EnemyType.clone)
            {
                GetComponent<EnemyCloneBehaviour>().enabled = true;
            }
            if (_enemyType == EnemyType.squirrel)
            {
                GetComponent<SquireBehaviour>().enabled = true;
            }
        }   
    }

    void DoKnockback(Vector2 sourcePosition)
    {
        if(knockbackRecibido > 0)
        {
            
            Vector2 dir = (Vector2)transform.position - sourcePosition;
            dir = dir.normalized;
            //Debug.Log("KnockbackEnem" + dir.x * knockbackRecibido);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(dir.x * knockbackRecibido, 0), ForceMode2D.Impulse);
        }
    }
    public float GetKnockback()
    {
        return knockbackDado;
    }
    
    public EnemyType GetEnemyType()
    {
        return _enemyType;
    }


    public void NoMoreAnimator()
    {
        //I hate my life
        GetComponent<Animator>().enabled = false;
    }

    IEnumerator AfterDie()
    {
        yield return new WaitForSeconds(0.1f);

        if(GetComponentInChildren<HotZoneCheck>())
        {
            GetComponentInChildren<HotZoneCheck>().gameObject.SetActive(false);
            GetComponentInChildren<HotZoneCheck>().enabled = false;
        }
        
        if(GetComponentInChildren<TriggerAreaCheck>())
        {
            GetComponentInChildren<TriggerAreaCheck>().gameObject.SetActive(false);
            GetComponentInChildren<TriggerAreaCheck>().enabled = false;
        }
    }

    public void IsFromWave()
    {
        isFromWave = true;
    }
}
