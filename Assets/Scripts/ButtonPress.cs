using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

// Assign to the Left and Right controller (OpenXR objects with HapticImpulsePlayer attributes)
public class ButtonPress : MonoBehaviour
{
    private HapticImpulsePlayer haptics;

    private void Start()
    {
        haptics = GetComponent<HapticImpulsePlayer>();
    }

    private bool activatedOnce = false; // Allow to know if chaining activations or just coming back later
    private void OnCollisionEnter(Collision collision)
    {
        activatedOnce = false;
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
                pressFrames++;
                // Button being pressed
                // TODO Progress Bar
                if (activatedOnce) haptics.SendHapticImpulse(1f, 0.1f); // Keep max haptics
                else haptics.SendHapticImpulse(((float)pressFrames) / ((float)button.pressFramesToActivate), 0.1f); // Progressive strength
                if (pressFrames == button.pressFramesToActivate)
                {
                    // Button activates (pressed long enough)
                    button.OnActivate();
                    activatedOnce = true;
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
