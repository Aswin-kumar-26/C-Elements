using UnityEngine;

public class SetBeakerColorYellow : MonoBehaviour
{
    public Material yellowMaterial; // Drag YellowLiquid here in Inspector

    void Start()
    {
        Invoke("ApplyMaterial", 0.1f); // Delay helps prevent transient errors
    }

    void ApplyMaterial()
    {
        Transform cylinder = transform.Find("LiquidHolder/Cylinder");
        if (cylinder != null && yellowMaterial != null)
        {
            Renderer renderer = cylinder.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = yellowMaterial;
                Debug.Log("Applied yellow material to beaker cylinder.");
            }
            else
            {
                Debug.LogError("Renderer not found on Cylinder.");
            }
        }
        else
        {
            Debug.LogError("Cylinder not found or yellow material is missing.");
        }
    }
}
