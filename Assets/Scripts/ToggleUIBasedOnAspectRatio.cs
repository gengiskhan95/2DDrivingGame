using UnityEngine;

public class ToggleUIBasedOnAspectRatio : MonoBehaviour
{
    public GameObject[] WideScreenObjects;
    public GameObject[] TallScreenObjects;

    void Start()
    {
        ToggleUIElements();
    }

    void ToggleUIElements()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        bool isWideScreen = screenWidth > screenHeight;

        foreach (GameObject obj in WideScreenObjects)
        {
            obj.SetActive(isWideScreen);
        }

        foreach (GameObject obj in TallScreenObjects)
        {
            obj.SetActive(!isWideScreen);
        }
    }

    void Update()
    {
        ToggleUIElements();
    }
}
