using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance {get; private set;}
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    public CinemachineConfiner _confiner;
    private float shakeTimer;
    
    private void Awake()
    {
        Instance = this;
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void LookDown()
    {
        GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = 0.35f;
    }
    public void LookRight()
    {
        gameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.3f;
    }
    public void LookRight2()
    {
        gameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.1f;
    }
    public void BackToNormal()
    {
        GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.5f;
    }
    public void BackToNormalY()
    {
        GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = 0.5f;
    }
    public void LookAtTarget(Transform target)
    {
        gameObject.GetComponent<CinemachineVirtualCamera>().Follow = target;
    }
    public void LookAtPlayer()
    {
        GetComponent<CinemachineVirtualCamera>().Follow = GameManager.INSTANCE.GetActivePlayer().gameObject.transform;
    }

    //ScreenShake

    public void ScreenShake(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }
    private void SwitchConfiners(ConfinerController Shape)
    {
        if (Shape.gameObject.name != _confiner.m_BoundingShape2D.gameObject.name)
        {
            _confiner.m_Damping = 0.5f;
            _confiner.m_BoundingShape2D = Shape.BoundingShape;
            _confiner.InvalidatePathCache();
        }
        
    }
    private void FixedUpdate()
    {
        if (_confiner.m_Damping > 0)
        {
            _confiner.m_Damping -= 0.01f;
        }

        if (shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime; 

            if (shakeTimer <= 0f)
            {
            //over
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }
    
    private void ToggleOff()
    {
        gameObject.SetActive(false);
    }

    public Collider2D GetCurrentConfiner()
    {
        return _confiner.m_BoundingShape2D;
    }


}
