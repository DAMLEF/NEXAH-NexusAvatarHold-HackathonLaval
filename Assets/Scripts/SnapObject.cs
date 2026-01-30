using UnityEngine;

public class SnapObject : MonoBehaviour
{
    public Transform objectSnapPosition;
    public GameObject allyZone;

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
            if (!item.GetComponent<Figurine>().getPlacementStatus())
            {
                GameObject avatar = item.GetComponent<Figurine>().getAvatar();
                allyZone.GetComponent<AllyZone>().addAvatar(avatar);

                item.GetComponent<Figurine>().figurinePlaced();

                
            }


            item.transform.parent = transform;
            //if (objectSnapPosition) { 
            //   item.transform.localPosition = objectSnapPosition.localPosition;
            //    item.transform.localRotation = objectSnapPosition.localRotation;
            //}
            Destroy(item);

        }

    }
}
