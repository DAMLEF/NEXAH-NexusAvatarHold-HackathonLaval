using UnityEngine;

public class RotateItemShop : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.up, 20 * Time.deltaTime, Space.World);
    }
}
