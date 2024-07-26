using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoxes : MonoBehaviour
{
    private float _playerPosRef;
    private float _holdLimit;
    [SerializeField] private float _posOffset; //.16 funciona para .5 de escala
    [SerializeField] private float _posOffLimit; //.025 goat
    [SerializeField] private float _speedDrag = .6f;
    private bool _isDragging;
    [SerializeField] private LayerMask _layerRef;
    private Player _playerRef;
    SpriteRenderer _spriteRenderer;
    private void OnEnable()
    {
        _isDragging = false;    
    }
    private void Start()
    {
        _playerRef = GetComponent<Player>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
    }
    void OnTriggerEnter2D(Collider2D box) //Cuando te acercas a la caja
    {
        if(box.gameObject.CompareTag("Movible"))
        {
            if(box.gameObject.transform.localScale.x >1)
            {
                box.gameObject.transform.GetComponent<CapsuleCollider2D>().size = new Vector2(.60f - box.gameObject.transform.localScale.x/100, box.gameObject.transform.GetComponent<CapsuleCollider2D>().size.y);
            }
            else
            {
                box.gameObject.transform.GetComponent<CapsuleCollider2D>().size = new Vector2(.63f  + Math.Abs(box.gameObject.transform.localScale.x/10 - .1f), box.gameObject.transform.GetComponent<CapsuleCollider2D>().size.y);
            }
            /* esto contemplado por si toca hacer algo de poco grosor y alto, thats the pun .565f
            else
            {
                box.gameObject.transform.GetComponent<CapsuleCollider2D>().size = new Vector2(.70f + Math.Abs(box.gameObject.transform.localScale.x/10 - .1f), box.gameObject.transform.GetComponent<CapsuleCollider2D>().size.y);
            }
            */
        }
    }
    void OnTriggerStay2D(Collider2D box) //Cuando te quedas en la caja
    {
        
        if(box.gameObject.CompareTag("Movible") && GetComponent<Playerererer>().grounded)
        {
            if(Input.GetButton("Submit") && !GetComponent<Animator>().GetBool("IsJumping"))
            {
                if(GetComponent<Animator>().GetBool("IsJumping")) return;
                Physics2D.queriesHitTriggers = false; //No detectes triggers >:(
                Vector2 _refDER = new Vector2 (box.gameObject.transform.GetComponent<BoxCollider2D>().bounds.max.x, box.gameObject.transform.GetComponent<Collider2D>().bounds.min.y + .1f * box.gameObject.transform.localScale.y);
                Vector2 _refIZQ = new Vector2 (box.gameObject.transform.GetComponent<BoxCollider2D>().bounds.min.x, box.gameObject.transform.GetComponent<Collider2D>().bounds.min.y + .1f * box.gameObject.transform.localScale.y);
                RaycastHit2D hitIZQ = Physics2D.Raycast(_refIZQ, Vector2.left, _layerRef);
                RaycastHit2D hitDER = Physics2D.Raycast(_refDER, Vector2.right, _layerRef);
                _playerRef.SetSpeed(_speedDrag);
                if(!_isDragging)
                {
                    box.GetComponent<Animator>().SetBool("inRange", true);
                    if((_spriteRenderer.flipX && transform.position.x> box.gameObject.transform.position.x)||(!_spriteRenderer.flipX && transform.position.x<box.gameObject.transform.position.x))GetComponent<Animator>().SetBool("Push", true);
                    if((!_spriteRenderer.flipX && transform.position.x> box.gameObject.transform.position.x)||(_spriteRenderer.flipX && transform.position.x<box.gameObject.transform.position.x))GetComponent<Animator>().SetBool("Drag", true);
                    _holdLimit = 0f;
                    _isDragging = true;
                }
                /*Debuggers de los hits :D
                Debug.Log("IZQUIERDA: " + hitIZQ.distance + hitIZQ.collider.name);
                Debug.Log("DERECHA: " + hitDER.distance + hitDER.collider.name);
                */
                Vector2 colliderDER = box.gameObject.transform.GetComponent<CapsuleCollider2D>().bounds.max;
                Vector2 colliderIZQ = box.gameObject.transform.GetComponent<CapsuleCollider2D>().bounds.min;
                Vector2 PlayerIZQ = box.gameObject.transform.GetComponent<BoxCollider2D>().bounds.max;
                Vector2 PlayerDER = box.gameObject.transform.GetComponent<BoxCollider2D>().bounds.min;
                if(box.gameObject.transform.position.x < gameObject.transform.position.x) //Si el jugador está a la derecha
                {
                    MoveBoxDirection(box.gameObject.transform, colliderDER, hitIZQ, -_posOffset, _posOffLimit); //Offset negativo, la caja está a la izquierda y el limite positivo(Está del otro lado)
                    if (Input.GetAxisRaw("Horizontal") > 0.1)
                    {
                        GetComponent<Animator>().SetBool("Drag", true);
                        GetComponent<Animator>().SetBool("Push", false);
                    }
                    else if (Input.GetAxisRaw("Horizontal") < -0.1)
                    {
                        GetComponent<Animator>().SetBool("Push", true);
                        GetComponent<Animator>().SetBool("Drag", false);
                    }
                    
                }
                else if(box.gameObject.transform.position.x > gameObject.transform.position.x) //Si el jugador está a la izquierda
                {
                    MoveBoxDirection(box.gameObject.transform, colliderIZQ, hitDER, _posOffset, -_posOffLimit); //Offset positivo, la caja está a la derecha y el limite negativo(Está del otro lado)
                    if (Input.GetAxisRaw("Horizontal") > 0.1)
                    {
                        GetComponent<Animator>().SetBool("Push", true);
                        GetComponent<Animator>().SetBool("Drag", false);
                    }
                    else if (Input.GetAxisRaw("Horizontal") < -0.1)
                    {
                        GetComponent<Animator>().SetBool("Drag", true);
                        GetComponent<Animator>().SetBool("Push", false);
                    }
                }
            }
            else
            {
                _isDragging = false;
                box.GetComponent<Animator>().SetBool("inRange", false);
                ResetAnimMove();
            }
            
            
        }
    }

    void MoveBoxDirection(Transform BoxRef, Vector2 Origin, RaycastHit2D HitDirection, float PushForce, float PushLimit)
    {
        //float distanceXD = Vector2.Distance(BoxRef.transform.position, Origin);
        float distanceXD = Mathf.Abs(Origin.x - BoxRef.position.x) * PushForce;
        if(HitBuilding(HitDirection) && HitDirection.distance > Mathf.Abs(PushLimit))
        {
            //Debug.Log("new offset xd: " + distanceXD);
            BoxRef.position = new Vector2(transform.position.x + distanceXD, BoxRef.position.y);

        }
        else if(HitBuilding(HitDirection))
        {
            if(((Input.GetAxisRaw("Horizontal")>0.2) && BoxRef.position.x < transform.position.x) || ((Input.GetAxisRaw("Horizontal")<-0.2) && BoxRef.position.x > transform.position.x))
            {
                //Debug.Log("GoBack "+ Input.GetAxisRaw("Horizontal"));
                transform.position = new Vector2(transform.position.x + PushLimit, transform.position.y);
                BoxRef.position = new Vector2(BoxRef.position.x + PushLimit, BoxRef.position.y);
                _holdLimit = 0f;
            }
        }
    }
    void OnTriggerExit2D(Collider2D box)  //Cuando te alejas a la caja
    {
        if(box.gameObject.CompareTag("Movible"))
        {
            Physics2D.queriesHitTriggers = true; //Detecta triggers >:)
            _playerRef.ResetSpeed();
            _isDragging = false;
            box.GetComponent<Animator>().SetBool("inRange", false);
            ResetAnimMove();
            if(GetComponent<Playerererer>().grounded) GetComponent<Animator>().SetTrigger("idle");
            //box.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }

    bool HitBuilding(RaycastHit2D hit) //En caso de que haya un nuevo tag que funcione como pared agregar acá ♥ ⬇⬇
    {
        if(hit.collider.gameObject.CompareTag("NotClimbable") || hit.collider.gameObject.CompareTag("Untagged") || hit.collider.gameObject.CompareTag("Movible") || hit.collider.gameObject.CompareTag("Enemy"))
        {
            return true;
        }
        else
        {
            Debug.Log("no pega");
            return false;
        }
    }
    bool HitPlayer(RaycastHit2D hit)
    {
        if(hit.collider.gameObject.CompareTag("Player"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void ResetAnimMove()
    {
        GetComponent<Animator>().SetBool("Push", false);
        GetComponent<Animator>().SetBool("Drag", false);
    }
}