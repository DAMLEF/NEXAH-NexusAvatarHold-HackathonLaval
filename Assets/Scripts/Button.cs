using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

// Assign on the moving button part
public class Button : MonoBehaviour
{
    public float minY = 0.015f;
    public float maxY = 0.079f;
    [HideInInspector] public float thresholdY;
    public int pressFramesToActivate = 200;
    private Rigidbody rb;

    void Start()
    {
        thresholdY = minY + 0.6f * (maxY - minY);
        
        rb = GetComponent<Rigidbody>();
    }

    public void OnActivate()
    {
        // TODO
        Debug.LogWarning("Button active : NOT IMPLEMENTED");
    }
    
    void Update()
    {
        // Clamp position
        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Clamp(transform.localPosition.y, minY, maxY), transform.localPosition.z);
    }

}
