using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ResponsiveRectTransformInspector : MonoBehaviour
{
    public Vector2 wideScreenAnchorMin;
    public Vector2 wideScreenAnchorMax;
    public Vector2 wideScreenOffsetMin;
    public Vector2 wideScreenOffsetMax;
    public Vector2 wideScreenPivot;

    public Vector2 tallScreenAnchorMin;
    public Vector2 tallScreenAnchorMax;
    public Vector2 tallScreenOffsetMin;
    public Vector2 tallScreenOffsetMax;
    public Vector2 tallScreenPivot;

    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        AdjustRectTransform();
    }

    void Update()
    {
        AdjustRectTransform();
    }

    void AdjustRectTransform()
    {
        float screenAspectRatio = (float)Screen.width / Screen.height;

        if (screenAspectRatio >= 1)
        {
            rectTransform.anchorMin = wideScreenAnchorMin;
            rectTransform.anchorMax = wideScreenAnchorMax;
            rectTransform.offsetMin = wideScreenOffsetMin;
            rectTransform.offsetMax = wideScreenOffsetMax;
            rectTransform.pivot = wideScreenPivot;
        }
        else
        {
            rectTransform.anchorMin = tallScreenAnchorMin;
            rectTransform.anchorMax = tallScreenAnchorMax;
            rectTransform.offsetMin = tallScreenOffsetMin;
            rectTransform.offsetMax = tallScreenOffsetMax;
            rectTransform.pivot = tallScreenPivot;
        }
    }
}
