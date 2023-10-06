using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onMouseOverSorc : MonoBehaviour
{


    public bool mouseOver = false;



    public void mouseOverButton()
    {
        mouseOver = true;
        if (mouseOver == true)
        {
            GameObject troopContainer = GameObject.Find("Store");
            Store storeScript = troopContainer.GetComponent<Store>();
            storeScript.sorcLevelText.text = "Level: " + storeScript.gameData.sorcLevel + " +  " + storeScript.sorcLevelIncrease;
            storeScript.sorcHealthText.text = "Health: " + storeScript.gameData.sorcHealth + " + " + storeScript.sorcHealthIncrease;
            storeScript.sorcDamageText.text = "Damage: " + storeScript.gameData.sorcDamage + " + " + storeScript.sorcDamageIncrease;
        }
    }

    public void mouseNotOver()
    {
        mouseOver = false;
        if (mouseOver == false)
        {
            GameObject troopContainer = GameObject.Find("Store");
            Store storeScript = troopContainer.GetComponent<Store>();
            storeScript.sorcLevelText.text = "Level: " + storeScript.gameData.sorcLevel;
            storeScript.sorcHealthText.text = "Health: " + storeScript.gameData.sorcHealth;
            storeScript.sorcDamageText.text = "Damage: " + storeScript.gameData.sorcDamage;
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
