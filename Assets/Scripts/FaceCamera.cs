using TMPro;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    void Update()
    {
        Vector3 dir = Camera.main.transform.position - transform.position;

        if (dir.sqrMagnitude > 0.0001f)
            transform.rotation = Quaternion.LookRotation(dir);
    }
}
