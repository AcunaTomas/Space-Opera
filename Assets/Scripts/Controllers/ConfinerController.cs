using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfinerController : MonoBehaviour
{
    public PolygonCollider2D BoundingShape;
    public ConfinerController self;
    void Awake()
    {
        BoundingShape = GetComponent<PolygonCollider2D>();
        self = GetComponent<ConfinerController>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            GameManager.INSTANCE.ChangeCameras(self);
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Player" && GameManager.INSTANCE.CameraController.GetCurrentConfiner() != BoundingShape)
        {
            GameManager.INSTANCE.ChangeCameras(self);
        }
    }

}
