using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class ARInteractionHandler : MonoBehaviour
{
    private ARObjectPlacer objectPlacer;
    private float initialDistance;
    private Vector3 initialScale;

    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += HandleTouchInput;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerMove += HandlePinchToZoom;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerMove += HandleRotation;
    }

    void OnDisable()
    {
        EnhancedTouchSupport.Disable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= HandleTouchInput;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerMove -= HandlePinchToZoom;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerMove -= HandleRotation;
    }

    void Start()
    {
        objectPlacer = FindFirstObjectByType<ARObjectPlacer>();
    }

    void HandleTouchInput(Finger finger)
    {
        if (objectPlacer == null) return;

        Pose newPose;
        ARPlaneDetector planeDetector = FindFirstObjectByType<ARPlaneDetector>();
        if (planeDetector != null && planeDetector.TryGetPlacementPosition(out newPose))
        {
            objectPlacer.MoveElement(newPose);
        }
    }

    void HandlePinchToZoom(Finger finger)
    {
        if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count < 2 || objectPlacer == null)
        {
            initialDistance = 0; // Reset distance when no pinch gesture is detected
            return;
        }

        var touch0 = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[0];
        var touch1 = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[1];

        float currentDistance = Vector2.Distance(touch0.screenPosition, touch1.screenPosition);

        if (initialDistance == 0)
        {
            initialDistance = currentDistance;
            initialScale = objectPlacer.GetSpawnedObject().transform.localScale;
        }
        else
        {
            float scaleFactor = currentDistance / initialDistance;
            objectPlacer.GetSpawnedObject().transform.localScale = initialScale * scaleFactor;
        }
    }

    void HandleRotation(Finger finger)
    {
        if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count < 2 || objectPlacer == null) return;

        var touch0 = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[0];
        var touch1 = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[1];

        Vector2 prevTouch0 = touch0.screenPosition - touch0.delta;
        Vector2 prevTouch1 = touch1.screenPosition - touch1.delta;

        float prevAngle = Mathf.Atan2(prevTouch1.y - prevTouch0.y, prevTouch1.x - prevTouch0.x) * Mathf.Rad2Deg;
        float currentAngle = Mathf.Atan2(touch1.screenPosition.y - touch0.screenPosition.y, touch1.screenPosition.x - touch0.screenPosition.x) * Mathf.Rad2Deg;

        float angleDifference = currentAngle - prevAngle;
        objectPlacer.GetSpawnedObject().transform.Rotate(Vector3.up, -angleDifference);
    }
}
