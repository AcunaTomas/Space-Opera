using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 previousCameraPosition;
    [SerializeField] float moving = .1f;
    void Start()
    {
        cameraTransform =  CameraController.Instance.transform;
        //Cinemachine.CinemachineVirtualCamera _activeCamera
        previousCameraPosition = cameraTransform.position;
    }

    void LateUpdate()
    {
        float deltaX = (cameraTransform.position.x - previousCameraPosition.x) * moving;
        transform.Translate(new Vector3(deltaX * -1, 0, 0));
        previousCameraPosition = cameraTransform.position;
    }
}
