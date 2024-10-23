using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSceneChimi : MonoBehaviour
{
    // Start is called before the first frame update
    Animator _animRef;
    [SerializeField] Transform bossRef;
    bool _pissing;
    void Start()
    {
        _pissing = false;
        InputManager.INSTANCE.UIInputs();
        GameManager.INSTANCE.PAUSABLE = false;
        _animRef = GetComponent<Animator>();
        _animRef.Play("Chimi_run");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(bossRef.position.x, transform.position.y), 1*Time.deltaTime);
        if(Vector2.Distance(transform.position, new Vector2(bossRef.position.x, transform.position.y)) < .1f && !_pissing)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            _pissing = true;
            _animRef.Play("Chimi_piss");
        }
    }
}
