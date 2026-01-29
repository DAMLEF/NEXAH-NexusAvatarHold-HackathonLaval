using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // On affiche les points de spawn à l'aide d'une sphère rouge

    public float radius = 0.3f;
    public Color color = new Color(1f, 0.5f, 0.5f);

    void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
