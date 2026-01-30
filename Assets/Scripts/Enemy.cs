using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 10;

    public int damage;
    public float attackRange;
    public float attackSpeed;
    private float lastAttack;

    private bool axis;
    private bool direction;

    public float speed;
    private float maxDistance;
    private float travelDistance = 0.0f;

    private GameObject targetLane;

    private bool alreadyGivenMoney = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // On déplace l'adversaire seulement si il n'est pas au bout de la lane
        if (travelDistance < maxDistance)
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

        // Phase d'attaque
        if(maxDistance - travelDistance <= attackRange)
        {
            // Si l'adversaire est assez proche d'une barrière
            if (Time.time - lastAttack >= attackSpeed)
            {
                // Si l'adversaire n'a pas déjà attaqué recemment
                targetLane.GetComponent<Lane>().removeHealth(damage);
                lastAttack = Time.time;


            }
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLISION");
        if (collision.gameObject.CompareTag("Laser"))
        {
            Debug.Log("IMPACT");
            removeHealth(collision.gameObject.GetComponent<LaserBounce>().hitDamage);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Grenade"))
        {
            removeHealth(collision.gameObject.GetComponent<Grenade>().hitDamage);
        }
    }

    int getDirection()
    {
        return direction ? 1 : -1;
    }

    public void removeHealth(int h)
    {
        health -= h;
        if (health <= 0) {

            if (!alreadyGivenMoney)
            {
                // On ajotue de l'argent à chaque kill
                GameObject.Find("GameManager").GetComponent<GameManager>().addMoney(1);
                alreadyGivenMoney = true;
            }


            Destroy(gameObject);
        }
    }

    public void prepareEnemy(GameObject lane, float difficultyCoefficient)
    {

        setTargetLane(lane);

        axis = targetLane.GetComponent<Lane>().getAxis();
        direction = targetLane.GetComponent<Lane>().getDirection();

        Vector3 spawnPos = targetLane.GetComponent<Lane>().getSpawnPosition();
        setupSpawn(spawnPos.x, spawnPos.y, spawnPos.z);

        health = (int)(difficultyCoefficient * health);

        maxDistance = targetLane.GetComponent<Lane>().getLaneLength() - 1;


        int xRotation = -90;
        int yRotation = 90;
        int zRotation = 0;

        if (axis)
        {
            if (direction)
            {
                yRotation += 180;
            }
            else
            {

            }
        }
        else
        {
            if (direction)
            {
                yRotation -= 90;
            }
            else
            {
                yRotation += 90;
            }

        }

        transform.Rotate(new Vector3(xRotation, yRotation, zRotation));

        lastAttack = Time.time;
    }

    private void setupSpawn(float x, float y, float z)
    {
        Vector3 pos;

        pos.x = x;
        pos.y = y;
        pos.z = z;

        transform.position = pos;
    }

    private void setTargetLane(GameObject lane)
    {
        targetLane = lane;
    }


}
