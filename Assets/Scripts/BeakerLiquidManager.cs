using UnityEngine;

public class BeakerTrigger : MonoBehaviour
{
    private Renderer beakerLiquidRenderer;
    private Color firstColor;
    private bool firstPoured = false;

    void Start()
    {
        Transform cylinder = transform.Find("LiquidHolder/Cylinder");
        if (cylinder != null)
        {
            beakerLiquidRenderer = cylinder.GetComponent<Renderer>();
            if (beakerLiquidRenderer != null)
            {
                // Make sure we’re working with a fresh material instance
                beakerLiquidRenderer.material = new Material(beakerLiquidRenderer.sharedMaterial);
                Debug.Log("Beaker material prepared.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Chemical")) return;

        Renderer chemRenderer = other.GetComponentInChildren<Renderer>();
        if (chemRenderer == null)
        {
            Debug.LogWarning("Chemical object has no Renderer.");
            return;
        }

        Color chemicalColor = chemRenderer.material.color;
        if (!firstPoured)
        {
            firstColor = chemicalColor;
            beakerLiquidRenderer.material.color = firstColor;
            firstPoured = true;
            Debug.Log("First chemical poured.");
        }
        else
        {
            Color mixedColor = Color.Lerp(firstColor, chemicalColor, 0.5f);
            beakerLiquidRenderer.material.color = mixedColor;
            Debug.Log("Second chemical mixed.");
        }

        Destroy(other.gameObject);
    }
}
