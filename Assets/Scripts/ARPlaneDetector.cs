using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class ARPlaneDetector : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private ARPlaneManager planeManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Start()
    {
        raycastManager = FindFirstObjectByType<ARRaycastManager>();
        planeManager = FindFirstObjectByType<ARPlaneManager>();

        if (raycastManager == null)
        {
            Debug.LogError("ARRaycastManager not found!");
        }

        if (planeManager != null)
        {
            planeManager.enabled = true; // Ensure plane detection is active
        }
        else
        {
            Debug.LogWarning("ARPlaneManager not found! Plane detection may not work.");
        }
    }

    public bool TryGetPlacementPosition(out Pose placementPose)
    {
        placementPose = new Pose();
        if (raycastManager == null) return false;

        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        if (raycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
        {
            placementPose = hits[0].pose;
            return true;
        }

        return false;
    }
}
