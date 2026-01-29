using System.Runtime.InteropServices;
using UnityEngine;

public class Lane : MonoBehaviour
{

    private int health;

    private bool axis;
    private bool direction;

    private float length;
    private float width;


    private bool spawnPointSet = false;
    private float spawnHeight = 1.0f;
    private float spawnX;
    private float spawnZ;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!spawnPointSet)
        {
            foreach (Transform child in transform)
            {
                if (child.CompareTag("SpawnPoint"))
                {

                    spawnX = child.position.x;
                    spawnHeight = child.position.y;
                    spawnZ = child.position.z;

                    spawnPointSet = true;
                }
            }
        }

    }
}
