using UnityEngine;
using UnityEngine.Android;

public class CameraPermissionHandler : MonoBehaviour
{
    void Start()
    {
        CheckAndRequestCameraPermission();
    }
    void CheckAndRequestCameraPermission()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);

            if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
            {
                Debug.Log("Camera permission denied! Please enable it in settings.");
            }
        }
    }

}
