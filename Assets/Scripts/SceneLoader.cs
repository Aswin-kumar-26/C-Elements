using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject panel;

    public void LoadElementsScene()
    {
        if (loadingScreen) loadingScreen.SetActive(true);
        SceneManager.LoadScene("ElementsScene");
    }

    public void LoadReactionScene()
    {
        if (loadingScreen) loadingScreen.SetActive(true);
        SceneManager.LoadScene("ReactionScene");
    }

    public void LoadMethaneScene()
    {
        if (loadingScreen) loadingScreen.SetActive(true);
        SceneManager.LoadScene("Methene");
    }

    public void LoadCarbondioxideScene()
    {
        if (loadingScreen) loadingScreen.SetActive(true);
        SceneManager.LoadScene("carbondixoide");
    }

    public void Load1H2Scene()
    {
        if (loadingScreen) loadingScreen.SetActive(true);
        SceneManager.LoadScene("1h2");
    }

    public void LoadHomeScene()
    {
        if (loadingScreen) loadingScreen.SetActive(true);
        SceneManager.LoadScene("HomeScene");
    }

    public void LoadARScene()
    {
        if (loadingScreen) loadingScreen.SetActive(true);
        SceneManager.LoadScene("ARScene");
    }

    public void ExitGame()
    {
        Debug.Log("Game Closed");
        Application.Quit();
    }

    public void SelectElement(int index)
    {
        int adjustedIndex = index - 1; // Convert 1-based button index to 0-based prefab index

        if (adjustedIndex >= 0 && adjustedIndex < 10) // 10 = number of element prefabs
        {
            PlayerPrefs.SetInt("SelectedElementIndex", adjustedIndex);
            PlayerPrefs.Save();

            if (panel != null)
            {
                Debug.Log("Disabling panel before scene transition.");
                panel.SetActive(false);
            }

            SceneManager.LoadScene("ARScene");
        }
        else
        {
            Debug.LogError("Invalid element index selected: " + index);
        }
    }
}
