using UnityEngine;
using Cinemachine;

public class MonitorMainCamera : MonoBehaviour
{
    private void OnEnable()
    {
        CinemachineCore.CameraUpdatedEvent.RemoveListener(OnCameraUpdated);
        CinemachineCore.CameraUpdatedEvent.AddListener(OnCameraUpdated);
    }

    private void OnDisable()
    {
        CinemachineCore.CameraUpdatedEvent.RemoveListener(OnCameraUpdated);
    }

    private void OnCameraUpdated(CinemachineBrain brain)
    {
        Camera camera = brain.OutputCamera;
        if (camera != null)
        {
            gameObject.GetComponent<Camera>().fieldOfView = camera.fieldOfView;
        }
    }


}
