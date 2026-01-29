using UnityEngine;

public class AllyZone : MonoBehaviour
{

    public float radius = 0.3f;
    public Color color = new Color(0.57f, 1f, 0.54f);

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
