using UnityEngine;
using System.Collections;

public class HiResScreenShots : MonoBehaviour
{
    public Camera Camera;

    public KeyCode screenShotButton;
    void Update()
    {
        if (Input.GetKeyDown(screenShotButton))
        {
            ScreenCapture.CaptureScreenshot("screenshot.png");
            Debug.Log("A screenshot was taken!");
        }
    }
}