using UnityEngine;

public class AvatarGrabSpawn : MonoBehaviour
{
    public float radius = 0.3f;
    public Color color = new Color(0.07f, 0.37f, 1f);

    void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
