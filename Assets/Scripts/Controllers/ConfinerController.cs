using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfinerController : MonoBehaviour
{
    public Collider2D BoundingShape;
    public ConfinerController self;


    public float OrtoSize = 1.3f;
    void Awake()
    {
        if (OrtoSize == null || OrtoSize == 0)
        {
            OrtoSize = 1.3f;
        }
        BoundingShape = GetComponent<Collider2D>();
        self = GetComponent<ConfinerController>();
    }

    void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.tag == "Player")
        {
            GameManager.INSTANCE.ChangeCameras(self);
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player" && GameManager.INSTANCE.CameraController.GetCurrentConfiner() != BoundingShape)
        {
            GameManager.INSTANCE.ChangeCameras(self);
        }
    }

}
