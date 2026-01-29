using UnityEngine;

public class Avatar : MonoBehaviour
{
    public bool axis;
    public bool direction;

    public int damage;
    public float attackSpeed;
    private float lastAttack;

    // TODO PUBLIC
    public GameObject defendedLane;

    public GameObject projectile;
    public GameObject avatarAttackStorage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastAttack >= attackSpeed)
        {
            // Si il n'a pas déjà attaqué recemmenton lance un projectile

            Vector3 colliderSize;

            GameObject attack = Instantiate(projectile, avatarAttackStorage.transform);


            if (axis)
            {
                colliderSize = new Vector3(1f, 5f, 30f);
            }
            else
            {
                colliderSize = new Vector3(30f, 5f, 1f);
            }

            attack.GetComponent<AvatarProjectile>().setupProjectile(transform.position.x, transform.position.y, transform.position.z, axis, direction, defendedLane.GetComponent<Lane>().getLaneLength(), damage);
            attack.GetComponent<BoxCollider>().size = colliderSize;


            lastAttack = Time.time;
        }



    }
}
