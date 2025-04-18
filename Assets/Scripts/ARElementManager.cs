using UnityEngine;
using UnityEngine.SceneManagement;

public class ARElementManager : MonoBehaviour
{
    public GameObject[] elementPrefabs;
    public GameObject panel; // Reference to the UI panel

    public static int SelectedElementIndex { get; private set; } = -1;
    private ElementDescriptionManager descriptionManager;

    void Start()
    {
        SelectedElementIndex = PlayerPrefs.GetInt("SelectedElementIndex", -1);
        descriptionManager = FindFirstObjectByType<ElementDescriptionManager>();

        if (SelectedElementIndex != -1 && descriptionManager != null)
        {
            descriptionManager.UpdateDescription(SelectedElementIndex);
        }
    }

    public void SelectElement(int index)
    {
        if (index >= 1 && index <= elementPrefabs.Length)
        {
            SelectedElementIndex = index - 1; // Convert to 0-based
            PlayerPrefs.SetInt("SelectedElementIndex", SelectedElementIndex);
            PlayerPrefs.Save();

            if (SceneManager.GetSceneByName("ARScene") != null)
            {
                if (panel != null)
                {
                    panel.SetActive(false);
                }
                SceneManager.LoadScene("ARScene");
            }
            else
            {
                Debug.LogError("ARScene not found!");
            }
        }
        else
        {
            Debug.LogError("Invalid element index: " + index);
        }
    }

    public void ForceRespawn()
    {
        GameObject currentModel = GameObject.FindGameObjectWithTag("SpawnedElement");

        if (currentModel != null)
        {
            Destroy(currentModel);
            Debug.Log("Old model destroyed.");
        }
        else
        {
            Debug.LogWarning("No model found to destroy.");
        }

        GameObject elementPrefab = GetSelectedElementPrefab();
        if (elementPrefab != null)
        {
            Camera arCamera = Camera.main;
            Vector3 spawnPosition = arCamera.transform.position + arCamera.transform.forward * 0.5f;

            GameObject newElement = Instantiate(elementPrefab, spawnPosition, Quaternion.identity);
            newElement.tag = "SpawnedElement";

            Debug.Log("New model spawned: " + newElement.name);
        }
        else
        {
            Debug.LogError("No valid element prefab to spawn.");
        }
    }

    public GameObject GetSelectedElementPrefab()
    {
        if (SelectedElementIndex >= 0 && SelectedElementIndex < elementPrefabs.Length)
        {
            return elementPrefabs[SelectedElementIndex];
        }
        return null;
    }
}
