using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasuraLogic : MonoBehaviour
{
    [SerializeField]
    private GenericObjPool pool;

    [SerializeField]
    private bool activate = false;

    [SerializeField]
    private float activationTimer = 1f;

    [SerializeField]
    private Vector2 spawnPos;

    private float actualtime = 0f;

    void Awake()
    {
        pool = GetComponent<GenericObjPool>();
        if (spawnPos == null)
        {
            spawnPos = new Vector2(0,0);
        }
        actualtime = activationTimer;
    }

    public void ToggleActive()
    {
        activate = !activate;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag != "NotClimbable")
        {
            return;
        }
        other.gameObject.SetActive(false);
    }

    void Update()
    {
        if (activate)
        {
            actualtime += Time.deltaTime;
            if (actualtime >= activationTimer)
            {
                var a = pool.GetObj();
                print(a);
                a.transform.localPosition = new Vector2(spawnPos.x, a.transform.localPosition.y);
                a.SetActive(true);

                actualtime = 0f;
            }
        }
    }
}
