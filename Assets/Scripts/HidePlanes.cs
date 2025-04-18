using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections;

public class HidePlanes : MonoBehaviour
{
    private ARPlaneManager planeManager;

    void Start()
    {
        planeManager = GetComponent<ARPlaneManager>();

        if (planeManager == null)
        {
            Debug.LogError("ARPlaneManager not found!");
            return;
        }

        StartCoroutine(HidePlanesRoutine());
    }

    IEnumerator HidePlanesRoutine()
    {
        yield return new WaitForSeconds(1f); // Wait for planes to be detected

        foreach (var plane in planeManager.trackables)
        {
            HidePlaneVisuals(plane);
        }

        planeManager.enabled = false; // Optional: stop tracking new planes
    }

    void HidePlaneVisuals(ARPlane plane)
    {
        // Disable mesh renderer
        var meshRenderer = plane.GetComponent<MeshRenderer>();
        if (meshRenderer) meshRenderer.enabled = false;

        // Disable all line renderers
        foreach (var line in plane.GetComponentsInChildren<LineRenderer>())
        {
            line.enabled = false;
        }

        // Disable all child objects
        foreach (Transform child in plane.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
