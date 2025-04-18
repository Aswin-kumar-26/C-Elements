using UnityEngine;

public class ChemicalSpawner : MonoBehaviour
{
    public GameObject testTubePrefab;
    public GameObject beakerPrefab;
    public Material[] chemicalMaterials;
    public Camera arCamera;

    private GameObject firstTube;
    private GameObject secondTube;
    private GameObject beaker;

    private Vector3 spawnBasePos => arCamera.transform.position + arCamera.transform.forward * 0.5f + Vector3.down * 0.1f;

    public void SpawnFirstChemical(int chemicalIndex)
    {
        if (firstTube != null) Destroy(firstTube);

        Vector3 leftPos = spawnBasePos + arCamera.transform.right * -0.2f;
        firstTube = Instantiate(testTubePrefab, leftPos, Quaternion.Euler(270, 0, 0));
        firstTube.transform.localScale = Vector3.one * 100f;

        ApplyChemicalMaterial(firstTube, chemicalIndex);
    }

    public void SpawnSecondChemical(int chemicalIndex)
    {
        if (secondTube != null) Destroy(secondTube);

        Vector3 rightPos = spawnBasePos + arCamera.transform.right * 0.2f;
        secondTube = Instantiate(testTubePrefab, rightPos, Quaternion.Euler(270, 0, 0));
        secondTube.transform.localScale = Vector3.one * 100f;

        ApplyChemicalMaterial(secondTube, chemicalIndex);
    }

    public void SpawnBeaker()
    {
        if (beaker != null) Destroy(beaker);

        Vector3 centerPos = spawnBasePos;
        beaker = Instantiate(beakerPrefab, centerPos + Vector3.down * 0.05f, Quaternion.Euler(270, 0, 0));
        beaker.transform.localScale = Vector3.one * 100f;

        // Automatically set the beaker reference in SimpleReaction using new method
        SimpleReaction reactionScript = FindFirstObjectByType<SimpleReaction>();
        if (reactionScript != null)
        {
            reactionScript.SetBeaker(beaker);
        }
    }

    private void ApplyChemicalMaterial(GameObject tube, int chemicalIndex)
    {
        if (chemicalIndex >= chemicalMaterials.Length) return;

        Transform liquid = tube.transform.Find("TubeLiquid/Cylinder");
        if (liquid != null)
        {
            Renderer rend = liquid.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.material = chemicalMaterials[chemicalIndex];
            }
        }
    }

    public void ResetScene()
    {
        if (firstTube) Destroy(firstTube);
        if (secondTube) Destroy(secondTube);
        if (beaker) Destroy(beaker);
    }
}
