using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewSystem : MonoBehaviour
{
    private ScrollRect _scrollRect;
    [SerializeField]
    private float _scrollSpeed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        _scrollRect = GetComponent<ScrollRect>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            if (_scrollRect.verticalNormalizedPosition <= 1f)
            {
                _scrollRect.verticalNormalizedPosition += _scrollSpeed;
            }
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            if (_scrollRect.verticalNormalizedPosition >= 0f)
            {
                _scrollRect.verticalNormalizedPosition -= _scrollSpeed;
            }
        }
    }
}
