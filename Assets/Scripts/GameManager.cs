using UnityEngine;
using System.Collections.Generic;
using Unity.XR.CoreUtils;


public class GameManager : MonoBehaviour
{

    public int money = 0;

    public List<GameObject> lanes;
    private int avatarsCount = 0;

    public GameObject avatarGrabSpawn;

    public GameObject avatarGunFigurine;
    public GameObject avatarLSFigurine;
    public GameObject avatarGrenadeFigurine;

    public int avatarPerLane = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void OnValidate()
    {
        makeShopAction(0);
    }

    // Update is called once per frame
    void Update()
    {
        

        foreach (GameObject lane in lanes)
        {
            if (lane.GetComponent<Lane>().getHealth() <= 0)
            {
                Debug.Log("FIN DU JEU : Barricade détruite");
            }
        }

    }

    public void addMoney(int m)
    {
        money += m;
    }

    public void removeMoney(int m)
    {
        money -= m;
    }

    public int getMoney() {  return money; }

    public bool spawnAvatarFigurine(GameObject figurine)
    {
        if (avatarsCount < lanes.Count * 3)
        {
            GameObject newFigurine = Instantiate(figurine);

            figurine.transform.position = avatarGrabSpawn.transform.position;

            avatarsCount++;
            return true;

        }
        else
        {
            Debug.Log("Il y a trop de figurines dans le jeu");
            return false;
        }
    }

    public bool makeShopAction(int action)
    {
        if(action == 0)
        {
            return spawnAvatarFigurine(avatarGunFigurine);
        }
        else if(action == 1)
        {
            return spawnAvatarFigurine(avatarLSFigurine);
        }
        else if(action == 2)
        {
            return spawnAvatarFigurine(avatarGrenadeFigurine);
        }
        else
        {
            return false;
        }


    }

    public List<GameObject> getLanes()
    {
        return lanes;
    }

}
