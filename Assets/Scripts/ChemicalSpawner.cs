using UnityEngine;

public class ChemicalSpawnerV2 : MonoBehaviour
{
    public GameObject chemicalPrefab;
    public Material[] chemicalMaterials;
    public string[] chemicalNames;
    public Transform spawnPoint;
    public ReactionManager reactionManager;

    public void SpawnChemical(int index)
    {
        GameObject chem = Instantiate(chemicalPrefab, spawnPoint.position, Quaternion.identity);

        // Assign chemical name via TubeInfo script
        TubeInfo tubeInfo = chem.GetComponent<TubeInfo>();
        if (tubeInfo != null && index < chemicalNames.Length)
        {
            tubeInfo.chemicalName = chemicalNames[index];
        }

        // Assign fresh material instance to avoid (Instance)(Instance)
        Transform liquid = chem.transform.Find("TubeLiquid");
        if (liquid != null)
        {
            Renderer rend = liquid.GetComponent<Renderer>();
            if (rend != null && index < chemicalMaterials.Length)
            {
                Material newMat = new Material(chemicalMaterials[index]);
                rend.sharedMaterial = newMat;
                Debug.Log("Applied material (fresh instance): " + newMat.name);
            }
            else
            {
                Debug.LogWarning("Renderer missing or index out of range!");
            }
        }
        else
        {
            Debug.LogWarning("TubeLiquid not found!");
        }

        // Register chemical
        if (reactionManager != null && index < chemicalNames.Length)
        {
            reactionManager.RegisterChemical(chemicalNames[index]);
        }
    }
}
