using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro namespace

public class CameraActivation : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;
    public Button cameraToggleButton;
    private TextMeshProUGUI buttonText; // Use TextMeshProUGUI instead of Text

    private bool isAerialView = false;

    void Start()
    {
        // Ensure Camera1 is active and Camera2 is inactive at the start
        camera1.gameObject.SetActive(true);
        camera2.gameObject.SetActive(false);

        // Set up the button text and add the listener
        buttonText = cameraToggleButton.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = "Aerial View";
        cameraToggleButton.onClick.AddListener(ToggleCamera);
    }

    public void ToggleCamera()
    {
        if (isAerialView)
        {
            // Switch to Camera1 (Front View)
            camera1.gameObject.SetActive(true);
            camera2.gameObject.SetActive(false);
            buttonText.text = "Aerial View";
            Debug.Log("Switching to Aerial view");
        }
        else
        {
            // Switch to Camera2 (Aerial View)
            camera1.gameObject.SetActive(false);
            camera2.gameObject.SetActive(true);
            buttonText.text = "Front View";
            Debug.Log("Switching to Front view");
        }

        isAerialView = !isAerialView;
    }
}