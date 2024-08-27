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

    [SerializeField]
    private float _default_Size = 1.3f;

    [SerializeField]
    private float _interpolation_Speed = 0.5f;
    
    [SerializeField]
    private float _target_Size;
    
    private void Awake()
    {
        Instance = this;
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        _target_Size = _default_Size;
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
    public void LookLeft()
    {
        gameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 0.95f;
    }
    public void LookLeft2()
    {
        gameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = 1.25f;
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
            _target_Size = Shape.OrtoSize;
        }
        //Debug.Log(_target_Size);
    }
    private void FixedUpdate()
    {
        if (_cinemachineVirtualCamera.m_Lens.OrthographicSize > _target_Size || _cinemachineVirtualCamera.m_Lens.OrthographicSize < _target_Size)
        {
            _cinemachineVirtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(_cinemachineVirtualCamera.m_Lens.OrthographicSize,_target_Size,_interpolation_Speed * Time.deltaTime);
        }
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

    public void TooMuchShake()
    {
        Instance.ScreenShake(.5f, .8f);
    }

    public void DeStupidFuction()
    {
        GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_LookaheadTime = 0.5f;
        GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_LookaheadSmoothing = 8;
        GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_XDamping = 1;
        GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping = 1.5f;
        GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_ZDamping = 1;
        GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_SoftZoneHeight = 0.8f;
        GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_SoftZoneWidth = 0.8f;
    }

    public void StupidFuction()
    {
        GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_LookaheadTime = 0;
        GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_LookaheadSmoothing = 0;
        GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_XDamping = 0;
        GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_YDamping = 0;
        GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_ZDamping = 0;
        GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_SoftZoneHeight = 0;
        GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>().m_SoftZoneWidth = 0;
    }

}
