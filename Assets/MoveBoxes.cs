using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoxes : MonoBehaviour
{
    private float _playerPosRef;
    private float _boxLimitRef;
    private float _holdLimit;
    [SerializeField] private float _posOffset; //.16 funciona
    [SerializeField] private float _posOffLimit; //.025 goat
    [SerializeField] private float _speedDrag = .6f;
    private bool _isDragging;
    [SerializeField] private LayerMask _layerRef;
    private Player _playerRef;
    private void OnEnable()
    {
        _isDragging = false;    
    }
    private void Start()
    {
        _playerRef = GetComponent<Player>();
    }
    private void FixedUpdate()
    {
        if(_boxLimitRef != 0f)
        {
            Debug.Log("LimitRef");
            if(Input.GetButton("Submit"))
            {
                //Debug.Log("KEEP HOLDING" + _holdLimit);
                _holdLimit += Time.deltaTime;
            }
        }
    }
    void OnTriggerStay2D(Collider2D box) //Cuando te acercas a la caja
    {
        if(box.gameObject.CompareTag("Movible") && Input.GetButton("Submit"))
        {
            Physics2D.queriesHitTriggers = false; //No detectes triggers >:(
            Vector2 _refDER = new Vector2 (box.gameObject.transform.GetComponent<BoxCollider2D>().bounds.max.x, box.gameObject.transform.GetComponent<Collider2D>().bounds.max.y);
            Vector2 _refIZQ = new Vector2 (box.gameObject.transform.GetComponent<BoxCollider2D>().bounds.min.x, box.gameObject.transform.GetComponent<Collider2D>().bounds.max.y);
            RaycastHit2D hitIZQ = Physics2D.Raycast(_refIZQ, Vector2.left, _layerRef);
            RaycastHit2D hitDER = Physics2D.Raycast(_refDER, Vector2.right, _layerRef);
            _playerRef.SetSpeed(_speedDrag);
            if(!_isDragging)
            {
                _holdLimit = 0f;
                _boxLimitRef = 0f;
                _playerPosRef = gameObject.transform.position.x;
                _isDragging = true;
            }
            /*Debuggers de los hits :D
            Debug.Log("IZQUIERDA: " + hitIZQ.distance + hitIZQ.collider.name);
            Debug.Log("DERECHA: " + hitDER.distance + hitDER.collider.name);
            */
            if(box.gameObject.transform.position.x < gameObject.transform.position.x) //Si el jugador está a la derecha
            {
                MoveBoxDirection(box.gameObject.transform, hitIZQ, -_posOffset, _posOffLimit); //Offset negativo, la caja está a la izquierda y el limite positivo(Está del otro lado)
            }
            else if(box.gameObject.transform.position.x > gameObject.transform.position.x) //Si el jugador está a la izquierda
            {
                MoveBoxDirection(box.gameObject.transform, hitDER, _posOffset, -_posOffLimit); //Offset positivo, la caja está a la derecha y el limite negativo(Está del otro lado)
            }
        }
    }

    void MoveBoxDirection(Transform BoxRef, RaycastHit2D HitDirection, float PushForce, float PushLimit)
    {
        if(HitBuilding(HitDirection) && HitDirection.distance > PushLimit)
        {
            BoxRef.position = new Vector2(gameObject.transform.position.x + PushForce, BoxRef.gameObject.transform.position.y);
        }
        else if(HitBuilding(HitDirection))
        {
            _boxLimitRef = BoxRef.gameObject.transform.position.x;
            if(_holdLimit>=.4f)
            {
                Debug.Log("GoBack");
                gameObject.transform.position = new Vector2(gameObject.transform.position.x + PushLimit, gameObject.transform.position.y);
                BoxRef.gameObject.transform.position = new Vector2(gameObject.transform.position.x + PushForce, BoxRef.gameObject.transform.position.y);
                _holdLimit = 0f;
                _boxLimitRef = 0;
            }
        }
    }
    void OnTriggerExit2D(Collider2D box)  //Cuando te alejas a la caja
    {
        if(box.gameObject.CompareTag("Movible") || Input.GetButtonUp("Submit"))
        {
            Physics2D.queriesHitTriggers = true; //Detecta triggers >:)
            _playerRef.ResetSpeed();
            _isDragging = false;
            //box.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }

    bool HitBuilding(RaycastHit2D hit) //En caso de que haya un nuevo tag que funcione como pared agregar acá ♥ ⬇⬇
    {
        if(hit.collider.gameObject.tag == "NotClimbable" || hit.collider.gameObject.tag == "Untagged")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    bool HitPlayer(RaycastHit2D hit)
    {
        if(hit.collider.gameObject.tag == "Player")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
