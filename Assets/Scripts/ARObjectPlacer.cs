using UnityEngine;

public class ARObjectPlacer : MonoBehaviour
{
    private GameObject spawnedElement;
    private ARPlaneDetector planeDetector;
    private ARElementManager elementManager;

    void Start()
    {
        planeDetector = FindFirstObjectByType<ARPlaneDetector>();
        elementManager = FindFirstObjectByType<ARElementManager>();
        TrySpawnAtCenter();
    }

    void TrySpawnAtCenter()
    {
        if (planeDetector.TryGetPlacementPosition(out Pose placementPose))
        {
            GameObject elementPrefab = elementManager.GetSelectedElementPrefab();
            if (elementPrefab != null)
            {
                spawnedElement = Instantiate(elementPrefab, placementPose.position, placementPose.rotation);
            }
        }
        else
        {
            Debug.LogWarning("No suitable surface found at screen center!");
        }
    }

    public void MoveElement(Pose newPose)
    {
        if (spawnedElement != null)
        {
            spawnedElement.transform.position = newPose.position;
        }
    }

    public GameObject GetSpawnedObject() // ✅ This method fixes the error
    {
        return spawnedElement;
    }
}
