using System.Runtime.InteropServices;
using UnityEngine;

public class Lane : MonoBehaviour
{

    private int health = 100;
    private int maxHealth;

    public bool axis;
    public bool direction;

    public float length;
    public float width;

    public AudioSource attackedSound;


    private bool spawnPointSet = false;
    private float spawnHeight = 1.0f;
    private float spawnX;
    private float spawnZ;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("HP BARRICADE : " + health);

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

    public Vector3 getSpawnPosition()
    {
        Vector3 result = new Vector3(spawnX, spawnHeight, spawnZ);

        // On applique le facteur random sur la ligne de spawn
        float randomOffset = Random.Range(- width / 2f, width / 2f);

        // On inverse l'axe de spawn car il est perpendiculaire à la direction
        if (axis)
        {
            // Ligne de spawn selon z
            result.z += randomOffset;
        }
        else {
            // Ligne de spawn selon x
            result.x += randomOffset;
        }

        return result;

    }

    public void removeHealth(int h)
    {   
        if (attackedSound) attackedSound.Play();
        health -= h;

        // Si la barricade est détruite alors le GameManager le détecte et met fin au jeu
    }


    public int getMaxHealth() {  return maxHealth; }

    public bool getAxis() { return axis; }
    public bool getDirection() { return direction; }
    public float getLaneLength() { return length;  }
    public float getHealth()
    {
        return health;
    }


}
