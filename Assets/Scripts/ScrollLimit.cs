using UnityEngine;
using UnityEngine.UI;

public class ScrollLimit : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform content;

    private float minX; // Left boundary (when fully scrolled)
    private float maxX; // Right boundary (initial position)

    void Start()
    {
        // Set scroll boundaries
        maxX = 3104; // The initial X position of Content
        minX = 909;  // The X position where scrolling should stop

        // Force the content to start at the leftmost position
        content.anchoredPosition = new Vector2(minX, content.anchoredPosition.y);
    }

    void Update()
    {
        // Get the current content position
        Vector2 pos = content.anchoredPosition;

        // Clamp the X position between minX and maxX
        pos.x = Mathf.Clamp(pos.x, minX, maxX);

        // Apply the new position to prevent scrolling beyond the limits
        content.anchoredPosition = pos;
    }
}
