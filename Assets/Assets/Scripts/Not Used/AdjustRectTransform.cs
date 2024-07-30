using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class AdjustRectTransform : MonoBehaviour
{
    [Header("Initial Size (at 1920x1080)")]
    [SerializeField] private float initialWidth = 720f;
    [SerializeField] private float initialHeight = 50f;

    [Header("Min Size")]
    [SerializeField] private float minWidth = 525f;
    [SerializeField] private float minHeight = 25f;

    [Header("Margins")]
    [SerializeField] private float horizontalMargin = 50f;

    [Header("Wide Screen (Width > Height) Settings")]
    [SerializeField] private Vector2 wideScreenPosition;
    [SerializeField] private Vector2 wideScreenAnchorMin;
    [SerializeField] private Vector2 wideScreenAnchorMax;
    [SerializeField] private Vector2 wideScreenPivot;

    [Header("Tall Screen (Height > Width) Settings")]
    [SerializeField] private Vector2 tallScreenPosition;
    [SerializeField] private Vector2 tallScreenAnchorMin;
    [SerializeField] private Vector2 tallScreenAnchorMax;
    [SerializeField] private Vector2 tallScreenPivot;

    [Header("Reference Resolution")]
    [SerializeField] private float referenceWidth = 1920f;
    [SerializeField] private float referenceHeight = 1080f;

    private RectTransform rectTransform;
    private Vector2 lastScreenSize;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        AdjustSizeAndPosition();
        lastScreenSize = new Vector2(Screen.width, Screen.height);
    }

    private void Update()
    {
        Vector2 currentScreenSize = new Vector2(Screen.width, Screen.height);
        if (currentScreenSize != lastScreenSize)
        {
            AdjustSizeAndPosition();
            lastScreenSize = currentScreenSize;
        }
    }

    private void AdjustSizeAndPosition()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float scaleFactor = Mathf.Min(screenWidth / referenceWidth, screenHeight / referenceHeight);

        float newWidth = Mathf.Max(initialWidth * scaleFactor, minWidth);
        float newHeight = Mathf.Max(initialHeight * scaleFactor, minHeight);
        rectTransform.sizeDelta = new Vector2(newWidth, newHeight);

        if (screenWidth > screenHeight)
        {
            ApplyTransformSettings(wideScreenPosition, wideScreenAnchorMin, wideScreenAnchorMax, wideScreenPivot);
        }
        else
        {
            ApplyTransformSettings(tallScreenPosition, tallScreenAnchorMin, tallScreenAnchorMax, tallScreenPivot);
        }

        rectTransform.anchoredPosition = new Vector2(horizontalMargin, rectTransform.anchoredPosition.y);
    }

    private void ApplyTransformSettings(Vector2 position, Vector2 anchorMin, Vector2 anchorMax, Vector2 pivot)
    {
        rectTransform.anchoredPosition = position;
        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
        rectTransform.pivot = pivot;
    }
}
