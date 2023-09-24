using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    private float _x;

    private float _y;
    public void ValueX(float x)
    {
        _x = x;
    }
    public void ValueY(float y)
    {
        _y = y;
    }

    public void ChangePosition()
    {
        transform.position = new Vector2(_x, _y);
    }

}
