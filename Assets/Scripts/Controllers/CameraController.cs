using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance {get; private set;}
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
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
        GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.3f;
    }
    public void LookRight2()
    {
        GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.1f;
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
        GetComponent<CinemachineVirtualCamera>().Follow = target;
    }
    public void LookAtPlayer()
    {
        GetComponent<CinemachineVirtualCamera>().Follow = GameObject.FindWithTag("Player").transform;
    }

    //ScreenShake

    public void ScreenShake(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    private void FixedUpdate()
    {
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
    





}
