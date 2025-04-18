using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ElementDescriptionManager : MonoBehaviour
{
    public TextMeshProUGUI descriptionText;

    private Dictionary<int, string> elementDescriptions = new Dictionary<int, string>()
    {
        {0, "Hydrogen (H)\nAtomic Number: 1\npH: Neutral\nValence Electrons: 1\nElectronic Configuration: 1s¹\nNon-Metal\nLightest element, used in fuel cells."},
        {1, "Helium (He)\nAtomic Number: 2\npH: Neutral\nValence Electrons: 2\nElectronic Configuration: 1s²\nNoble Gas\nUsed in balloons and cooling systems."},
        {2, "Lithium (Li)\nAtomic Number: 3\npH: Basic\nValence Electrons: 1\nElectronic Configuration: [He] 2s¹\nMetal\nUsed in batteries."},
        {3, "Beryllium (Be)\nAtomic Number: 4\npH: Neutral\nValence Electrons: 2\nElectronic Configuration: [He] 2s²\nMetal\nUsed in aerospace applications."},
        {4, "Boron (B)\nAtomic Number: 5\npH: Neutral\nValence Electrons: 3\nElectronic Configuration: [He] 2s² 2p¹\nMetalloid\nUsed in detergents and semiconductors."},
        {5, "Carbon (C)\nAtomic Number: 6\npH: Neutral\nValence Electrons: 4\nElectronic Configuration: [He] 2s² 2p²\nNon-Metal\nBasis of organic life."},
        {6, "Nitrogen (N)\nAtomic Number: 7\npH: Neutral\nValence Electrons: 5\nElectronic Configuration: [He] 2s² 2p³\nNon-Metal\nMajor part of Earth's atmosphere."},
        {7, "Oxygen (O)\nAtomic Number: 8\npH: Neutral\nValence Electrons: 6\nElectronic Configuration: [He] 2s² 2p⁴\nNon-Metal\nEssential for respiration."},
        {8, "Fluorine (F)\nAtomic Number: 9\npH: Acidic\nValence Electrons: 7\nElectronic Configuration: [He] 2s² 2p⁵\nNon-Metal\nHighly reactive, used in toothpaste."},
        {9, "Neon (Ne)\nAtomic Number: 10\npH: Neutral\nValence Electrons: 8\nElectronic Configuration: [He] 2s² 2p⁶\nNoble Gas\nUsed in neon signs."}
    };

    public void UpdateDescription(int elementIndex)
    {
        if (descriptionText == null)
        {
            Debug.LogError("Description Text UI not assigned!");
            return;
        }

        if (elementDescriptions.TryGetValue(elementIndex, out string description))
        {
            descriptionText.text = description;
        }
        else
        {
            descriptionText.text = "No description available.";
            Debug.LogWarning($"Invalid element index: {elementIndex}");
        }
    }
}
