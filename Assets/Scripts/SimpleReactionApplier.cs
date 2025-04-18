using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SimpleReaction : MonoBehaviour
{
    public Material[] reactionMaterials; // Assign 9 materials in Inspector (3x3 combinations)

    public string[] reactionEquations = new string[9] {
        "H₂ + H₂ → H₄",        // 0
        "H₂ + O → H₂O",        // 1
        "H₂ + Fe → ???",       // 2
        "Na + H₂ → ???",       // 3
        "Na + O → Na₂O",       // 4
        "Na + Fe → ???",       // 5
        "Cl + H₂ → ???",       // 6
        "Cl + O → ???",        // 7
        "Cl + Fe → FeCl₃"      // 8
    };

    public string[] reactionDescriptions = new string[9] {
        "Hypothetical reaction with hydrogen.",
        "This is a simple water formation reaction.",
        "Unknown result with hydrogen and iron.",
        "Sodium reacts with hydrogen.",
        "Forms sodium oxide.",
        "Reaction with sodium and iron.",
        "Chlorine meets hydrogen.",
        "Chlorine reacts with oxygen.",
        "This creates iron(III) chloride."
    };

    public GameObject reactionPanel; // Assign UI Panel
    public TextMeshProUGUI reactionText;
    public TextMeshProUGUI descriptionText;

    private GameObject currentBeaker;
    private int firstChemicalIndex = -1;
    private int secondChemicalIndex = -1;

    void Start()
    {
        if (reactionPanel != null)
            reactionPanel.SetActive(false);
    }

    public void SetBeaker(GameObject beaker)
    {
        currentBeaker = beaker;
        Debug.Log("Beaker reference set.");
    }

    public void SelectFirstChemical(int index)
    {
        firstChemicalIndex = index;
        Debug.Log("First chemical selected: " + index);
    }

    public void SelectSecondChemical(int index)
    {
        secondChemicalIndex = index;
        Debug.Log("Second chemical selected: " + index);
    }

    public void ApplyReaction()
    {
        if (currentBeaker == null)
        {
            Debug.LogWarning("No beaker assigned!");
            return;
        }

        if (firstChemicalIndex < 0 || secondChemicalIndex < 0)
        {
            Debug.LogWarning("Both chemicals must be selected before reacting.");
            return;
        }

        int reactionIndex = firstChemicalIndex * 3 + secondChemicalIndex;

        if (reactionIndex >= reactionMaterials.Length)
        {
            Debug.LogError("Invalid reaction index.");
            return;
        }

        Transform cylinder = currentBeaker.transform.Find("LiquidHolder/Cylinder");
        if (cylinder != null)
        {
            Renderer rend = cylinder.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.material = reactionMaterials[reactionIndex];
                Debug.Log("Material applied for reaction index: " + reactionIndex);
            }
        }

        StartCoroutine(ShowReactionInfo(reactionIndex));
    }

    private IEnumerator ShowReactionInfo(int reactionIndex)
    {
        yield return new WaitForSeconds(1f);

        if (reactionPanel != null && reactionText != null && descriptionText != null)
        {
            reactionText.text = reactionEquations[reactionIndex];
            descriptionText.text = reactionDescriptions[reactionIndex];
            reactionPanel.SetActive(true);
        }
    }
}
