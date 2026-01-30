using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class AllyZone : MonoBehaviour
{
    // Paramètres Gizmo
    public float radius = 0.3f;
    public Color color = new Color(0.57f, 1f, 0.54f);

    public GameObject avatarGO;

    // Paramètres zone des alliés
    private GameObject parentLane;
    public float lateralOffset = 0.7f;

    public AudioSource zoneAudio;

    private List<GameObject> avatars = new List<GameObject>();


    void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parentLane = transform.parent.gameObject;
    }



    // Update is called once per frame
    void Update()
    {

        // TODO : Debug spawn avatar
        if (avatars.Count < 1 && false)
        {
            Debug.Log("LANE :" + parentLane.GetComponent<Lane>().getAxis());
            GameObject avatar = Instantiate(avatarGO);
            addAvatar(avatar);
        }




    }

    public void addAvatar(GameObject avatar)
    {
        if(avatars.Count < 3)
        {
            if (zoneAudio) { zoneAudio.Play(); }

            GameObject avatarInstance = Instantiate(avatar);
            
            avatars.Add(avatarInstance);

            Vector3 pos = transform.position;

            bool laneAxis = parentLane.GetComponent<Lane>().getAxis();

            if (avatars.Count == 1)
            {
                if (laneAxis)
                {
                    pos.z -= lateralOffset;
                }
                else
                {
                    pos.x -= lateralOffset;
                }
            }
            else if (avatars.Count == 2) {
                if (laneAxis)
                {
                    pos.z += lateralOffset;
                }
                else
                {
                    pos.x += lateralOffset;
                }
            }

            avatarInstance.transform.position = pos;
            avatarInstance.GetComponent<Avatar>().setupAvatar(parentLane);

        }
        else
        {
            Debug.Log("Impossible d'ajouter un avatar à la Lane car elle est remplie");
        }
    }
}
