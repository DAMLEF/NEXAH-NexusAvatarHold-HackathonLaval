using UnityEngine;

public class Avatar : MonoBehaviour
{
    public bool axis;
    public bool direction;

    public int damage;
    public float attackSpeed;
    private float lastAttack;

    // TODO PUBLIC
    private GameObject defendedLane;

    public GameObject projectile;
    private GameObject avatarAttackStorage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Awake()
    {
        if (avatarAttackStorage == null)
            avatarAttackStorage = GameObject.Find("AvatarAttackStorage");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastAttack >= attackSpeed)
        {
            // Si il n'a pas déjà attaqué recemmenton lance un projectile

            Vector3 colliderSize;

            Debug.Log(avatarAttackStorage);
            GameObject attack = Instantiate(projectile, avatarAttackStorage.transform);


            if (axis)
            {
                colliderSize = new Vector3(30f, 150f, 70f);
            }
            else
            {
                colliderSize = new Vector3(70f, 150f, 30f);
            }

            Debug.Log("LANE : " + defendedLane);
            attack.GetComponent<BoxCollider>().size = colliderSize;
            attack.GetComponent<AvatarProjectile>().setupProjectile(transform.position.x, transform.position.y, transform.position.z, axis, direction, defendedLane.GetComponent<Lane>().getLaneLength(), damage);



            lastAttack = Time.time;
        }



    }

    public void setupAvatar(GameObject lane)
    {
        defendedLane = lane;
    }
}
