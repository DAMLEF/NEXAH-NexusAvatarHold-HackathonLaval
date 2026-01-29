using UnityEngine;
using UnityEngine.UIElements;

public class LaserBounce : MonoBehaviour
{
    public static int allowedBounces;
    [SerializeField] private int allowedBouncesEditor = 1;

    public static float laserSpeed;
    [SerializeField] private float laserSpeedEditor = 5;

    public static int maxLifeTime;
    [SerializeField] private int maxLifeTimeEditor = 500;



    private int bouncesLeft;
    private int timeLeft;
    private Rigidbody rb;

    private void OnValidate()
    {
        allowedBounces = allowedBouncesEditor;
        laserSpeed = laserSpeedEditor;
        maxLifeTime = maxLifeTimeEditor;
    }

    void ApplyVelocity()
    {
        rb.linearVelocity = transform.forward * laserSpeed;
    }

    void Start()
    {
        OnValidate();
        bouncesLeft = allowedBounces;
        timeLeft = maxLifeTime;
        
        rb = GetComponent<Rigidbody>();
        if (!rb) Debug.LogError("Laser projectile doesn't have a rigidbody");

        // Set initial movement
        ApplyVelocity();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (bouncesLeft > 0)
        {
            bouncesLeft--;

            // Bounce in the correct direction (vector reflection)
            Vector3 normal = collision.GetContact(0).normal;
            transform.forward = transform.forward - 2f * Vector3.Dot(transform.forward, normal) * normal;

            ApplyVelocity();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (timeLeft < 0) { Destroy(gameObject); }
        timeLeft--;
    }
}
