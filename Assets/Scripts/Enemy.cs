using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int health;

    private int damage;

    public bool axis;
    public bool direction;

    public float speed;
    public float maxDistance;
    private float travelDistance = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // On déplace l'adversaire seulement si il n'est pas au bout de la lane
        if(travelDistance < maxDistance)
        {

            int directionCoefficient = getDirection();
            float deltaDistance = speed * Time.deltaTime;

            Vector3 newPos = transform.position;

            if (axis)
            {
                // On considère l'axe x pour le déplacement
                newPos.x += directionCoefficient * deltaDistance;

            }
            else
            {
                // On considère l'axe z pour le déplacement
                newPos.z += directionCoefficient * deltaDistance;
            }

            travelDistance += deltaDistance;

            transform.position = newPos;

            if (travelDistance > maxDistance) { 
                travelDistance = maxDistance;
            }
        }


    }

    int getDirection()
    {
        return direction ? 1 : -1;
    }

    void setupSpawn(float x, float z)
    {

    }

}
