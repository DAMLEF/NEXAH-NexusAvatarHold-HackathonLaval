using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics.HapticsUtility;

public class GrenadeManager : MonoBehaviour
{
    public GameObject grenadePrefab;
    public Transform grenadeSpawnTransform;

    [HideInInspector] public GameObject currentGrenade;
    private InputDevice hand;


    private void Start()
    {
        hand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    public GameObject GenerateGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab);
        grenade.transform.parent = transform;
        grenade.transform.localPosition = grenadeSpawnTransform.localPosition;
        grenade.transform.localRotation = grenadeSpawnTransform.localRotation;
        grenade.transform.localScale = grenadeSpawnTransform.localScale;
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
        return grenade;
    }

    private bool gripPressedLastFrame;
    private Vector3 posLastFrame = Vector3.zero;
    public void Update()
    {
        // Get "Grip" press to switch tool/mode
        bool gripPressed = false;
        bool success = hand.TryGetFeatureValue(CommonUsages.gripButton, out gripPressed);

        // Initialise grenade
        if (!currentGrenade && success && gripPressed && !gripPressedLastFrame)
        {
            currentGrenade = GenerateGrenade();

        }
        // Throw grenade
        else if (currentGrenade && success && !gripPressed && gripPressedLastFrame)
        {
            currentGrenade.transform.parent = null;
            Rigidbody rb = currentGrenade.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
                // Apply velocity
                rb.AddForce((currentGrenade.transform.position - posLastFrame) / Time.deltaTime, ForceMode.VelocityChange);
            }
            currentGrenade = null;
        }

        // update last grenade position to know speed
        if (currentGrenade) posLastFrame = currentGrenade.transform.position;
        gripPressedLastFrame = gripPressed;
    }
}
