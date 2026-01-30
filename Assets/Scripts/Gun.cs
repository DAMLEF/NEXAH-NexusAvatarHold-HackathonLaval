using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;

public class Gun : MonoBehaviour
{
    public static float cooldownBase = 30;
    [HideInInspector] public static float cooldown;
    public GameObject laserPrefab;
    public Transform laserStorage; // Hierachy management
    public static int gunDamage = 1;
    private Transform shootingPoint;
    private InputDevice hand;
    void Start()
    {
        cooldown = cooldownBase;
        hand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        shootingPoint = Generic.FindChildWithTag(gameObject, "Marker")?.transform;
        if (shootingPoint == null) Debug.LogError("No child of Gun with tag 'Marker' to shoot from");
    }

    public static void Upgrade()
    {
        cooldown = 0.98f * cooldown;
        gunDamage += 1;
        LaserBounce.allowedBounces += 1;
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
            if (laserStorage) laser.transform.SetParent(laserStorage);
            LaserBounce laserScript = laser.GetComponent<LaserBounce>();
            if (laserScript != null) { laserScript.hitDamage = gunDamage; }
            laser.transform.position = shootingPoint.position;
            laser.transform.localRotation = shootingPoint.rotation;
        }
    }
}
