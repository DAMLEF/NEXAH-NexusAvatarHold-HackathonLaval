using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public int action;
    public int price;

    private GameObject gm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Awake()
    {
        gm = GameObject.Find("GameObject");
    }

    // Update is called once per frame
    void Update()
    {
     
        

    }

    public bool canBeBuy()
    {

        if (gm != null && gm.GetComponent<GameManager>().getMoney() >= price) {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void purchase()
    {
        if (canBeBuy())
        {
            gm.GetComponent<GameManager>().addMoney(price);
            gm.GetComponent<GameManager>().makeShopAction(action);
        }

    }
}
