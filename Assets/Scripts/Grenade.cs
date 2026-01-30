using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float explosionRadius = 3f;
    public AudioSource explosionSource;

    public void Throw()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null )
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }
    public void Explode()
    {
        if (explosionSource) explosionSource.Play();
        // Create a big red sphere as explosion
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = Vector3.one * explosionRadius;
        sphere.transform.position = transform.position;
        Renderer renderer = sphere.GetComponent<Renderer>();
        renderer.material.color = Color.red;
        sphere.tag = "Grenade";
        Destroy(sphere, 0.5f);
        Destroy(gameObject);

    }
    void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (!obj.CompareTag("GameController"))
        {
            Explode();
        }
    }
}
