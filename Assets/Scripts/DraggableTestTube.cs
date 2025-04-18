using UnityEngine;
using UnityEngine.InputSystem;

public class UniversalTestTubeDragger : MonoBehaviour
{
    public LayerMask dragLayer;
    private Camera arCamera;
    private bool isDragging = false;
    private Vector3 offset;
    private float dragDistance;

    void Start()
    {
        arCamera = Camera.main;
    }

    void Update()
    {
        if (Pointer.current == null) return;

        Vector2 pointerPos = Pointer.current.position.ReadValue();

        if (Pointer.current.press.isPressed)
        {
            if (!isDragging)
            {
                Ray ray = arCamera.ScreenPointToRay(pointerPos);
                if (Physics.Raycast(ray, out RaycastHit hit, 1.5f, dragLayer))
                {
                    if (hit.transform == transform)
                    {
                        isDragging = true;
                        dragDistance = Vector3.Distance(arCamera.transform.position, hit.point);
                        offset = transform.position - hit.point;
                    }
                }
            }
            else
            {
                Vector3 newPos = arCamera.ScreenToWorldPoint(new Vector3(pointerPos.x, pointerPos.y, dragDistance));
                transform.position = newPos + offset;
            }
        }
        else
        {
            isDragging = false;
        }
    }
}
