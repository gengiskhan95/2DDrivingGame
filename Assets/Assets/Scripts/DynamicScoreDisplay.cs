using UnityEngine;
using UnityEngine.UI;

public class DynamicScoreDisplay : MonoBehaviour
{
    public Text scoreText;
    public Image backgroundImage;

    public float baseWidth = 100f;
    public float baseHeight = 50f;
    public float widthPerCharacter = 10f;

    void Update()
    {
        int textLength = scoreText.text.Length;

        float newWidth = baseWidth + (textLength * widthPerCharacter);
        float newHeight = baseHeight;

        backgroundImage.rectTransform.sizeDelta = new Vector2(newWidth, newHeight);
    }
}
