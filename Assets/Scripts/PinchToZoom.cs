using UnityEngine;

public class PinchToZoom : MonoBehaviour
{
    private float zoomSpeed = 0.005f;
    private float rotationSpeed = 5f;
    private float minScale = 0.1f;
    private float maxScale = 3f;

    void Update()
    {
        HandlePinchZoom();
        HandleMouseZoom();
        HandleRotation();
    }

    void HandlePinchZoom()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            float prevDistance = (touchZero.position - touchZero.deltaPosition).magnitude -
                                 (touchOne.position - touchOne.deltaPosition).magnitude;
            float currentDistance = (touchZero.position - touchOne.position).magnitude;
            float scaleChange = (currentDistance - prevDistance) * zoomSpeed;

            Vector3 newScale = transform.localScale + Vector3.one * scaleChange;
            transform.localScale = Vector3.ClampMagnitude(newScale, maxScale);
            transform.localScale = new Vector3(
                Mathf.Clamp(transform.localScale.x, minScale, maxScale),
                Mathf.Clamp(transform.localScale.y, minScale, maxScale),
                Mathf.Clamp(transform.localScale.z, minScale, maxScale)
            );
        }
    }

    void HandleMouseZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            Vector3 newScale = transform.localScale + Vector3.one * scroll;
            transform.localScale = new Vector3(
                Mathf.Clamp(newScale.x, minScale, maxScale),
                Mathf.Clamp(newScale.y, minScale, maxScale),
                Mathf.Clamp(newScale.z, minScale, maxScale)
            );
        }
    }

    void HandleRotation()
    {
        if (Input.GetMouseButton(1)) // Right-click to rotate
        {
            float rotation = Input.GetAxis("Mouse X") * rotationSpeed;
            transform.Rotate(Vector3.up, -rotation, Space.World);
        }
    }
}
