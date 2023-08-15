using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public float bulletSpeed;
    public GameObject bulletPrefab;
    public float moveSpeed;
    public float moveDistance;
    public float stopTime;

    private Vector2 initialPosition;
    private Vector2 targetPosition;
    private bool stopped = false;
    private float stopTimer = 0f;

    void Start()
    {
        initialPosition = transform.position - Vector3.up * moveDistance;
        targetPosition = transform.position;
    }

    void Update()
    {
        if (Mathf.Approximately(transform.position.y, targetPosition.y))
        {
            if (targetPosition == initialPosition)
            {
                targetPosition = transform.position + Vector3.up * moveDistance;
            }
            else
            {
                if (!stopped)
                {
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    bullet.SetActive(true);
                    bullet.transform.localScale = new Vector3(0.1f, 0.05f, 0f);
                    bullet.AddComponent<BulletController>();
                    bullet.GetComponent<Rigidbody2D>().velocity = Vector2.left * bulletSpeed;
                    stopped = true;
                }
                else
                {
                    stopTimer += Time.deltaTime;
                    if (stopTimer >= stopTime)
                    {
                        targetPosition = initialPosition;
                        stopped = false;
                        stopTimer = 0f;
                    }
                }
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
