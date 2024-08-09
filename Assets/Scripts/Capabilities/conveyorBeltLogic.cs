using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conveyorBeltLogic : MonoBehaviour
{
    [Header("Status")]
    [SerializeField]
    bool active = true;
    [Header("Push Strength Settings")]
    [SerializeField]
    float speed = -0.6f;
    [SerializeField]
    float playerSpeed = -14;

    void OnCollisionStay2D(Collision2D other)
    {
        if (!active)
        {
            return;
        }
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.SendMessage("AddSpeed", new Vector2(playerSpeed, 0));
            return;
        }

        other.rigidbody.velocity += new Vector2(speed, 0);
        
    }

}
