using UnityEngine;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{

    private int money = 0;

    public List<GameObject> lanes;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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

    public List<GameObject> getLanes()
    {
        return lanes;
    }
}
