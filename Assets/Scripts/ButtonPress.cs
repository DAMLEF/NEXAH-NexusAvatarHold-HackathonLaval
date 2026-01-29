using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

// Assign to the Left and Right controller (OpenXR objects with HapticImpulsePlayer attributes)
public class ButtonPress : MonoBehaviour
{
    public GameObject chargeBar;
    private Image bar;
    
    private HapticImpulsePlayer haptics;
    


    private void Start()
    {
        haptics = GetComponent<HapticImpulsePlayer>();
        if (!chargeBar) Debug.LogError("Charge bar canva not found by Controller " + gameObject);
        else bar = chargeBar.GetComponentInChildren<Image>();
    }


    private int consecutiveActivations = 0; // Allow to know if chaining activations or just coming back later (for haptics intensity)
    private void OnCollisionExit(Collision collision)
    {
        consecutiveActivations = 0;
        bar.fillAmount = 0;
        chargeBar.SetActive(false);
    }

    // Stay pressing on button
    private int pressFrames = 0;
    private void OnCollisionStay(Collision collision)
    {
        // Get button data
        if (collision.gameObject.CompareTag("Button"))
        {
            Button button = collision.gameObject.GetComponent<Button>();
            if (button.transform.localPosition.y <= button.thresholdY)
            {
                // Button being pressed
                chargeBar.SetActive(true);
                pressFrames++;
                float progress = Mathf.Max(1f, (float)(pressFrames * (consecutiveActivations + 1))) / (float)button.pressFramesToActivate;
                // Progress Bar
                bar.fillAmount = progress;
                // Haptics
                if (consecutiveActivations >= 1) haptics.SendHapticImpulse(1f, 0.1f); // Keep max haptics
                else haptics.SendHapticImpulse(progress, 0.1f); // Progressive strength
                if (pressFrames == button.pressFramesToActivate)
                {
                    // Button activates (pressed long enough)
                    button.OnActivate();
                    consecutiveActivations++;
                    pressFrames = 0;
                }
            }
            else
            {
                pressFrames = 0;
            }
        }
    }
}
