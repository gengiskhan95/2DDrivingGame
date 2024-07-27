using UnityEngine;

public class ResponsiveRectTransform : MonoBehaviour
{
    public RectTransform rectTransform;

    public float initialWidth = 220f;
    public float initialHeight = 50f;

    public float minWidth = 110f;

    public float horizontalMargin = 50f;

    void Start()
    {
        AdjustSizeAndPosition();
    }

    void Update()
    {
        AdjustSizeAndPosition();
    }

    void AdjustSizeAndPosition()
    {
        float screenWidth = Screen.width;

        float newWidth = Mathf.Clamp(screenWidth - 2 * horizontalMargin, minWidth, initialWidth);

        rectTransform.sizeDelta = new Vector2(newWidth, initialHeight);

        rectTransform.anchoredPosition = new Vector2(0, rectTransform.anchoredPosition.y);
    }
}
