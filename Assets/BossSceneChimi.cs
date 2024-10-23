using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSceneChimi : MonoBehaviour
{
    // Start is called before the first frame update
    Animator _animRef;
    [SerializeField] Transform bossRef;
    [SerializeField] ScoreSystemLogic _scoreRef;
    [SerializeField] GameObject BRODY;
    [SerializeField] AnimatorControllerBrody Body;
    [SerializeField] GameObject BITO;
    [SerializeField] GameObject SPARKLE;
    bool _pissing;
    GameObject oficial, spawned;
    void Start()
    {
        _pissing = false;
        InputManager.INSTANCE.UIInputs();
        GameManager.INSTANCE.PAUSABLE = false;
        _animRef = GetComponent<Animator>();
        _animRef.Play("Chimi_run");
        TeleportSequence();
    }

    // Update is called once per frame
    void Update()
    {
        IDK();
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(bossRef.position.x, transform.position.y), 1*Time.deltaTime);
        if(Vector2.Distance(transform.position, new Vector2(bossRef.position.x, transform.position.y)) < .1f && !_pissing)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            _pissing = true;
            _animRef.Play("Chimi_piss");
            StartCoroutine(EndGame());
        }
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1f);
        Body.SetAnimTrigger("Laugh");
        BITO.GetComponent<Animator>().Play("Bito_laugh");
        yield return new WaitForSeconds(3f);
        _scoreRef.ShowScore();
    }

    void TeleportSequence()
    {
        SPARKLE.SetActive(true);
        if(BRODY.transform.position.x >= bossRef.position.x || BITO.transform.position.x >= bossRef.position.x)
        {
            Debug.Log("A la IZQUIERDA");
            if(BRODY.activeSelf)
            {
                oficial = BRODY;
                BITO.SetActive(true);
                BITO.transform.position = new Vector2(BRODY.transform.position.x+.2f, BRODY.transform.position.y);
                spawned = BITO;
            }
            else if(BITO.activeSelf)
            {
                oficial = BITO;
                BRODY.SetActive(true);
                BRODY.transform.position = new Vector2(BITO.transform.position.x+.2f, BITO.transform.position.y);
                spawned = BRODY;
            }
            BITO.GetComponent<BoxCollider2D>().enabled = false;
            BITO.GetComponent<Rigidbody2D>().isKinematic = true;
            BITO.GetComponent<SpriteRenderer>().flipX = true;
            Body.AnimatorLeft();
        }
        else if(BRODY.transform.position.x < bossRef.position.x || BITO.transform.position.x < bossRef.position.x)
        {
            Debug.Log("A la DERECHA");
            if(BRODY.activeSelf)
            {
                oficial = BRODY;
                BITO.SetActive(true);
                BITO.transform.position = new Vector2(BRODY.transform.position.x-.2f, BRODY.transform.position.y);
                spawned = BITO;
            }
            else if(BITO.activeSelf)
            {
                oficial = BITO;
                BRODY.SetActive(true);
                BRODY.transform.position = new Vector2(BITO.transform.position.x-.2f, BITO.transform.position.y);
                spawned = BRODY;
            }
            BRODY.GetComponent<BoxCollider2D>().enabled = false;
            BRODY.GetComponent<Rigidbody2D>().isKinematic = true;
            BITO.GetComponent<SpriteRenderer>().flipX = false;
            Body.AnimatorRight();
        }
        BRODY.GetComponent<Player>().enabled = false;
        BRODY.GetComponent<PlayerCombat>().enabled = false;
        BITO.GetComponent<Player>().enabled = false;
        BITO.GetComponent<PlayerCombat>().enabled = false;
    }

    void IDK()
    {
        SPARKLE.transform.position = spawned.transform.position;
        BITO.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,BITO.GetComponent<Rigidbody2D>().velocity.y);
        BRODY.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,BRODY.GetComponent<Rigidbody2D>().velocity.y);
        if(BRODY.transform.position.x >= bossRef.position.x || BITO.transform.position.x >= bossRef.position.x)
        {
            spawned.transform.position = new Vector2(oficial.transform.position.x+.2f, oficial.transform.position.y);
        }
        else if(BRODY.transform.position.x < bossRef.position.x || BITO.transform.position.x < bossRef.position.x)
        {
            spawned.transform.position = new Vector2(oficial.transform.position.x-.2f, oficial.transform.position.y);
        }
    }
}
