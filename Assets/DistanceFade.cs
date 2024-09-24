using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceFade : MonoBehaviour
{
    GameObject _player;

    public CanvasGroup _fade;
    public float maxDistance = 10f;
    public float minValue = 0f;
    public float maxValue = 1f;

    void FixedUpdate()
    {
        _player = GameManager.INSTANCE.GetActivePlayer().gameObject;

        float distance = Vector2.Distance(_player.transform.position, transform.position);

        float normalizedDistance = Mathf.Clamp01(distance / maxDistance);

        float value = Mathf.Lerp(maxValue, minValue, normalizedDistance);

        _fade.alpha = value; 
    }
}
