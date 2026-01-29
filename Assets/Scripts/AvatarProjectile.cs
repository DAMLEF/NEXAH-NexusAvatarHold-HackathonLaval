using UnityEngine;

public class AvatarProjectile : MonoBehaviour
{

    private int damage;

    public float speed;

    private bool axis;
    private bool direction;

    public bool distanceAvatar;

    private float maxDistance;
    private float travelDistance = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // On déplace le projectile seulement si il n'est pas au bout de la lane
        if (travelDistance < maxDistance)
        {

            // TODO : generic
            int directionCoefficient = getDirection() * (-1);
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
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().removeHealth(damage);
            Destroy(gameObject);
        }
    }

    int getDirection()
    {
        return direction ? 1 : -1;
    }

    public void setupProjectile(float x, float y, float z, bool axis, bool direction, float maxDistance, int damage)
    {
        transform.position = new Vector3(x, y, z);

        this.damage = damage;
        this.axis = axis;
        this.direction = direction;

        if (distanceAvatar) { 
            this.maxDistance = maxDistance; 
        }
        else
        {
            this.maxDistance = 3;

            if (axis)
            {
                gameObject.GetComponent<BoxCollider>().size = new Vector3(1f, 30f, 9f);
            }
            else
            {
                gameObject.GetComponent<BoxCollider>().size = new Vector3(9f, 30f, 1f);
            }
            
        }

        
    }
}
