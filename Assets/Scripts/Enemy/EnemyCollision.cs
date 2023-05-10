using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private Collider2D otherCollider;
    private Bounds bounds;

    void OnCollisionStay2D(Collision2D collision)
    {
        otherCollider = collision.collider;
        bounds = GetComponent<BoxCollider2D>().bounds;
        
        if (otherCollider.OverlapPoint(new Vector2(bounds.min.x, bounds.center.y)))
        {
            gameObject.GetComponent<EnemyMovement>()._leftMovement = false;
            transform.localScale = new Vector3(3.5f, 3.5f, 0);
        }

        if (otherCollider.OverlapPoint(new Vector2(bounds.max.x, bounds.center.y)))
        {
            gameObject.GetComponent<EnemyMovement>()._leftMovement = true;
            transform.localScale = new Vector3(-3.5f, 3.5f, 0);
        }
    }
}
