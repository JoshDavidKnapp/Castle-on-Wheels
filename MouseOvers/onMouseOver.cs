using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onMouseOver : MonoBehaviour
{
    public bool mouseOver = false;

  
     
    public void mouseOverButton()
    {
        mouseOver = true;
        if (mouseOver == true)
        {
            GameObject troopContainer = GameObject.Find("Store");
            Store storeScript = troopContainer.GetComponent<Store>();
            storeScript.militiaLevelText.text = "Level: " + storeScript.gameData.militiaLevel + " +  " + storeScript.militiaLevelIncrease;
            storeScript.militiaHealthText.text = "Health: " + storeScript.gameData.militiaHealth + " + " + storeScript.militiaHealthIncrease;
            storeScript.militiaDamageText.text = "Damage: " + storeScript.gameData.militiaDamage + " + " + storeScript.militiaDamageIncrease;
        }
    }

    public void mouseNotOver()
    {
        mouseOver = false;
        if(mouseOver == false)
        {
            GameObject troopContainer = GameObject.Find("Store");
            Store storeScript = troopContainer.GetComponent<Store>();
            storeScript.militiaLevelText.text = "Level: " + storeScript.gameData.militiaLevel;
            storeScript.militiaHealthText.text = "Health: " + storeScript.gameData.militiaHealth;
            storeScript.militiaDamageText.text = "Damage: " + storeScript.gameData.militiaDamage;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
