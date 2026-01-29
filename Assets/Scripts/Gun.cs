using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;

public class Gun : MonoBehaviour
{
    public float cooldown = 30;
    public GameObject laserPrefab;

    private Transform shootingPoint;
    private InputDevice hand;
    void Start()
    {
        hand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        shootingPoint = Generic.FindChildWithTag(gameObject, "Marker")?.transform;
        if (shootingPoint == null) Debug.LogError("No child of Gun with tag 'Marker' to shoot from");
    }

    void OnEnable()
    {
        // Avoid odd cooldown after swtiching while not allowing a strong quick swtiching reload exploit
        ticking = cooldown / 3; 
    }

    private float ticking = 0;
    void Update()
    {
        ticking--;
        // If on cooldown nothing to do
        if (ticking > 0) { return; }

        // Get "Grip" press to shoot gun
        bool triggerPressed = false;
        hand.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed);
        if (triggerPressed)
        {
            ticking = cooldown + 1;
            GameObject laser = Instantiate(laserPrefab);
            laser.transform.position = shootingPoint.position;
            laser.transform.localRotation = shootingPoint.rotation;
        }
    }
}
