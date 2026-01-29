using UnityEngine;

public class SnapObject : MonoBehaviour
{
    public Transform objectSnapPosition;

    private void Start()
    {
        if (!objectSnapPosition) Debug.LogError("No object snap position set in the SnapArea");
    }
    private void OnTriggerStay(Collider other)
    {
        GameObject item = other.gameObject;
        if (item.CompareTag("Grabbable") && (item.transform.parent == null || !item.transform.parent.CompareTag("GameController")) )
        {
            // Make item fixed
            Rigidbody rb = item.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }
            
            // Snap
            item.transform.parent = transform;
            if (objectSnapPosition) item.transform.localPosition = objectSnapPosition.localPosition;
        }
    }
}
