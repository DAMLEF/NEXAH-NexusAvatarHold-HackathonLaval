using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float explosionRadius = 3f;
    public int hitDamage = 10;

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
        // TODO Implement (growing collider)
        Debug.LogWarning("Explosion not implemented");

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
