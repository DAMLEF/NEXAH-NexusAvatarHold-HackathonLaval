using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;
using System.Collections.Generic;


public class WristInformation : MonoBehaviour
{
    public TMP_Text moneyText;
    public Image cyanBar;
    public Image yellowBar;
    public Image magentaBar;
    public TMP_Text dmgText;
    public TMP_Text bounceText;
    public TMP_Text cooldownText;

    void Update()
    {
        if (GameObject.Find("GameManager") != null)
        {
            moneyText.text = GameObject.Find("GameManager").GetComponent<GameManager>().getMoney().ToString();

            List<GameObject> lanes = GameObject.Find("GameManager").GetComponent<GameManager>().getLanes();

            yellowBar.fillAmount = (float)lanes[0].GetComponent<Lane>().getHealth() / (float)lanes[0].GetComponent<Lane>().getMaxHealth();
            cyanBar.fillAmount = (float)lanes[1].GetComponent<Lane>().getHealth() / (float)lanes[1].GetComponent<Lane>().getMaxHealth();
            magentaBar.fillAmount = (float)lanes[2].GetComponent<Lane>().getHealth() / (float)lanes[2].GetComponent<Lane>().getMaxHealth();


            if (GameObject.Find("Gun") != null)
            {
                //dmgText.text = GameObject.Find("Gun").GetComponent<Gun>().hitDamage.toString();
            }

        }

        
    
    }
}
