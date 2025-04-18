using UnityEngine;

public class ChemicalIdentifier : MonoBehaviour
{
    public string chemicalName;
    public Material chemicalMaterial;

    public Material GetMaterial()
    {
        return chemicalMaterial;
    }
}
