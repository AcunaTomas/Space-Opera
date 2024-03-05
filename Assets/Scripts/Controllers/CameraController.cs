using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    


    public void LookLeft()
    {
        gameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.8f;
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
        gameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.5f;
    }
    public void LookAtTarget(Transform target)
    {
        gameObject.GetComponent<CinemachineVirtualCamera>().Follow = target;
    }
    public void LookAtPlayer()
    {
        gameObject.GetComponent<CinemachineVirtualCamera>().Follow = GameManager.INSTANCE.PLAYER.transform;
    }

}
