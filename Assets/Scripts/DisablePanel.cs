using UnityEngine;

public class ARSceneManager : MonoBehaviour
{
    public GameObject panel; // Assign in Inspector

    void Start()
    {
        if (panel != null)
        {
            Debug.Log("Disabling panel in AR Scene.");
            panel.SetActive(false);
        }
        else
        {
            Debug.LogError("Panel is not assigned in ARSceneManager!");
        }
    }
}
