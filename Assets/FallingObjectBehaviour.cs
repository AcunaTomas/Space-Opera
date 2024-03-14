using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectBehaviour : MonoBehaviour
{
    private bool _fall;
    [SerializeField] private LayerMask _layerRef;
    [SerializeField] private float _speed = 2.5f; //me gusta en 2.5(?) Movelo a tú gusto si es un objeto más pesado:D
    void Awake()
    {
        _fall = false;
    }
    void FixedUpdate()
    {
        if(_fall)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.GetComponent<Collider2D>().bounds.min.y), Vector2.down, _layerRef);
            //Debug.DrawRay(transform.GetComponent<Collider2D>().bounds.min, Vector2.down, Color.red);
            if(hit.collider.gameObject.tag == "Untagged")
            {
                FallingFor(hit.distance);
            }
            else if(hit.collider.gameObject.tag == "Player") //Por si algún loquit@ se para abajo
            {
                Transform _playerTransform = hit.collider.gameObject.transform;
                RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(_playerTransform.position.x, _playerTransform.GetComponent<BoxCollider2D>().bounds.max.y), Vector2.down, _layerRef);
                FallingFor(hit.distance + hit2.distance);
            }
        }
        else
        {
            return;
        }
    }

    public void SetFall(bool booleano)
    {
        _fall = booleano;
    }
    private void FallingFor(float distanceRef)
    {
        if(distanceRef>_speed * Time.fixedDeltaTime)
        {
            //Debug.DrawRay(new Vector2(transform.position.x, transform.GetComponent<Collider2D>().bounds.min.y), Vector2.down, Color.red);
            //Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - transform.GetComponent<Collider2D>().bounds.extents.y), Vector2.down, Color.red);
            transform.position -= new Vector3(0, _speed * Time.fixedDeltaTime, 0);
        }
        else
        {
            transform.position -= new Vector3(0,distanceRef,0);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            SetFall(false);
        }
    }
    
}
