using UnityEngine;

public class Figurine : MonoBehaviour
{
    private bool alreadyPlace = false;
    public GameObject avatar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject getAvatar()
    {
        return avatar;
    }

    public void figurinePlaced()
    {
        alreadyPlace = true;
    }

    public bool getPlacementStatus()
    {
        return alreadyPlace;
    }

}
