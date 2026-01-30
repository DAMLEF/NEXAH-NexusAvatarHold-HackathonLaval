using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public int action;
    public int price;

    public AudioSource successAudio;
    public AudioSource errorAudio;

    private GameObject gm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Awake()
    {
        gm = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
     
        

    }

    public bool canBeBuy()
    {

        if (gm != null && gm.GetComponent<GameManager>().getMoney() >= price) {
            if (successAudio) { successAudio.Play(); }
            return true;
        }
        else
        {
            if (errorAudio) { errorAudio.Play(); }
            return false;
        }
    }

    public void purchase()
    {
        Debug.Log("Phase d'achat");
        if (canBeBuy())
        {
            Debug.Log("Achat confirmé - lancement de l'action");

            if (gm.GetComponent<GameManager>().makeShopAction(action))
            {
                Debug.Log("Action validé");
                gm.GetComponent<GameManager>().removeMoney(price);
            }


        }

    }
}
