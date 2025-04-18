using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

public class ARMoleculePlacer : MonoBehaviour
{
    public GameObject[] elementPrefabs;
    private int selectedElementIndex = -1;
    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private GameObject spawnedElement = null;

    private float initialDistance = 0f;
    private Vector3 initialScale;

    void OnEnable()
    {
        EnhancedTouch.EnhancedTouchSupport.Enable();
        EnhancedTouch.Touch.onFingerDown += HandleTouchInput;
        EnhancedTouch.Touch.onFingerMove += HandlePinchToZoom;
        EnhancedTouch.Touch.onFingerMove += HandleRotation;
    }

    void OnDisable()
    {
        EnhancedTouch.EnhancedTouchSupport.Disable();
        EnhancedTouch.Touch.onFingerDown -= HandleTouchInput;
        EnhancedTouch.Touch.onFingerMove -= HandlePinchToZoom;
        EnhancedTouch.Touch.onFingerMove -= HandleRotation;
    }

    void Start()
    {
        raycastManager = FindFirstObjectByType<ARRaycastManager>();
        planeManager = FindFirstObjectByType<ARPlaneManager>();
        planeManager.enabled = true;

        selectedElementIndex = PlayerPrefs.GetInt("SelectedElementIndex", -1);
        if (selectedElementIndex < 0 || selectedElementIndex >= elementPrefabs.Length)
        {
            Debug.LogWarning("No element selected or invalid index!");
            return;
        }

        Camera arCamera = Camera.main;
        if (arCamera == null)
        {
            Debug.LogError("No main camera found!");
            return;
        }

        Vector3 spawnPosition = arCamera.transform.position + arCamera.transform.forward * 2f + Vector3.down * 0.3f;
        spawnedElement = Instantiate(elementPrefabs[selectedElementIndex], spawnPosition, Quaternion.identity);
        spawnedElement.tag = "SpawnedElement";
        Debug.Log("Spawned at: " + spawnPosition);

        // Update description
        ElementDescriptionManager descriptionManager = FindFirstObjectByType<ElementDescriptionManager>();
        if (descriptionManager != null)
        {
            descriptionManager.UpdateDescription(selectedElementIndex);
        }
        else
        {
            Debug.LogWarning("ElementDescriptionManager not found in scene.");
        }
    }

    void HandleTouchInput(EnhancedTouch.Finger finger)
    {
        if (spawnedElement == null || selectedElementIndex < 0) return;

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(finger.index))
            return;

        if (raycastManager != null && raycastManager.Raycast(finger.screenPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            spawnedElement.transform.position = hitPose.position + Vector3.down * 0.1f;
            Debug.Log("Moved element to new position: " + hitPose.position);
        }
    }

    void HandlePinchToZoom(EnhancedTouch.Finger finger)
    {
        if (EnhancedTouch.Touch.activeTouches.Count < 2 || spawnedElement == null) return;

        var touch0 = EnhancedTouch.Touch.activeTouches[0];
        var touch1 = EnhancedTouch.Touch.activeTouches[1];

        float currentDistance = Vector2.Distance(touch0.screenPosition, touch1.screenPosition);

        if (initialDistance == 0f)
        {
            initialDistance = currentDistance;
            initialScale = spawnedElement.transform.localScale;
            return;
        }

        float scaleFactor = currentDistance / initialDistance;
        spawnedElement.transform.localScale = initialScale * scaleFactor;
    }

    void HandleRotation(EnhancedTouch.Finger finger)
    {
        if (EnhancedTouch.Touch.activeTouches.Count < 2 || spawnedElement == null) return;

        var touch0 = EnhancedTouch.Touch.activeTouches[0];
        var touch1 = EnhancedTouch.Touch.activeTouches[1];

        Vector2 prevTouch0 = touch0.screenPosition - touch0.delta;
        Vector2 prevTouch1 = touch1.screenPosition - touch1.delta;

        float prevAngle = Mathf.Atan2(prevTouch1.y - prevTouch0.y, prevTouch1.x - prevTouch0.x) * Mathf.Rad2Deg;
        float currentAngle = Mathf.Atan2(touch1.screenPosition.y - touch0.screenPosition.y, touch1.screenPosition.x - touch0.screenPosition.x) * Mathf.Rad2Deg;

        float angleDifference = currentAngle - prevAngle;
        spawnedElement.transform.Rotate(Vector3.up, -angleDifference);
    }

    public void SelectElement(int index)
    {
        if (index >= 0 && index < elementPrefabs.Length)
        {
            PlayerPrefs.SetInt("SelectedElementIndex", index);
            PlayerPrefs.Save();
            Debug.Log("Element Selected: " + elementPrefabs[index].name);
        }
        else
        {
            Debug.LogError("Invalid element index: " + index);
        }
    }
}
