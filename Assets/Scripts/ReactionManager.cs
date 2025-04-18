using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ReactionManager : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI reactionTitle;

    private string firstChemical = null;
    private string secondChemical = null;

    private Dictionary<string, string> reactionLookup = new Dictionary<string, string>();

    void Start()
    {
        if (panel != null)
            panel.SetActive(false);

        reactionLookup.Add("Sodium+Chlorine", "Sodium Chloride");
        reactionLookup.Add("Hydrogen+Oxygen", "Water");
        reactionLookup.Add("Sodium+Sulfate", "Sodium Sulfate");
        reactionLookup.Add("Hydrochloric acid+Sodium Hydroxide", "Salt and Water");
        reactionLookup.Add("Calcium+Sulfate", "Calcium Sulfate");

        if (reactionTitle != null)
            reactionTitle.text = "";
    }

    public void RegisterChemical(string chemicalName)
    {
        if (firstChemical == null)
        {
            firstChemical = chemicalName;
            Debug.Log($"First chemical registered: {chemicalName}");
        }
        else if (secondChemical == null)
        {
            secondChemical = chemicalName;
            Debug.Log($"Second chemical registered: {chemicalName}");
            DisplayReaction();
        }
    }

    private void DisplayReaction()
    {
        string key1 = $"{firstChemical}+{secondChemical}";
        string key2 = $"{secondChemical}+{firstChemical}";

        if (reactionLookup.ContainsKey(key1))
        {
            reactionTitle.text = reactionLookup[key1];
            Debug.Log($"Reaction found: {reactionLookup[key1]}");
        }
        else if (reactionLookup.ContainsKey(key2))
        {
            reactionTitle.text = reactionLookup[key2];
            Debug.Log($"Reaction found: {reactionLookup[key2]}");
        }
        else
        {
            reactionTitle.text = "No known reaction";
            Debug.Log("No valid reaction for the combination.");
        }

        if (panel != null)
            panel.SetActive(true);

        firstChemical = null;
        secondChemical = null;
    }

    public void ResetReaction()
    {
        firstChemical = null;
        secondChemical = null;

        if (reactionTitle != null)
            reactionTitle.text = "";

        if (panel != null)
            panel.SetActive(false);
    }
}
