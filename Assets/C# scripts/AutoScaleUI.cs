using UnityEngine;

public class AutoScaleUI : MonoBehaviour
{
    public float scaleSpeed = 1.0f; // Speed of scaling
    public float maxScale = 1.2f;   // Maximum scale factor
    public float minScale = 0.8f;   // Minimum scale factor

    private RectTransform rectTransform;
    private bool scalingUp = true; // Direction of scaling

    void Start()
    {
        // Get the RectTransform component of the UI element
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("AutoScaleUI requires a RectTransform component.");
        }
    }

    void Update()
    {
        if (rectTransform == null) return;

        // Determine the current scale
        Vector3 currentScale = rectTransform.localScale;

        // Adjust the scale based on the direction
        if (scalingUp)
        {
            currentScale += Vector3.one * scaleSpeed * Time.deltaTime;
            if (currentScale.x >= maxScale)
            {
                currentScale = Vector3.one * maxScale;
                scalingUp = false;
            }
        }
        else
        {
            currentScale -= Vector3.one * scaleSpeed * Time.deltaTime;
            if (currentScale.x <= minScale)
            {
                currentScale = Vector3.one * minScale;
                scalingUp = true;
            }
        }

        // Apply the new scale
        rectTransform.localScale = currentScale;
    }
}
