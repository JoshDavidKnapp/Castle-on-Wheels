using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onMouseOverCastle : MonoBehaviour
{


    public bool mouseOver = false;



    public void mouseOverButton()
    {
        mouseOver = true;
        if (mouseOver == true)
        {
            GameObject troopContainer = GameObject.Find("Store");
            Store storeScript = troopContainer.GetComponent<Store>();
            storeScript.castleLevelText.text = "Level: " + storeScript.gameData.castleLevel + " +  " + storeScript.castleLevelIncrease;
            storeScript.castleHealthText.text = "Health: " + storeScript.gameData.castleHealth + " + " + storeScript.castleHealthIncrease;
            storeScript.castleDamageText.text = "Damage: " + storeScript.gameData.castleDamage + " + " + storeScript.castleDamageIncrease;
            storeScript.castleSpeedText.text = "Speed: " + storeScript.gameData.castleSpeed + " + " + storeScript.castleSpeedIncrease;

        }
    }

    public void mouseNotOver()
    {
        mouseOver = false;
        if (mouseOver == false)
        {
            GameObject troopContainer = GameObject.Find("Store");
            Store storeScript = troopContainer.GetComponent<Store>();
            storeScript.castleLevelText.text = "Level: " + storeScript.gameData.castleLevel;
            storeScript.castleHealthText.text = "Health: " + storeScript.gameData.castleHealth;
            storeScript.castleDamageText.text = "Damage: " + storeScript.gameData.castleDamage;
            storeScript.castleSpeedText.text = "Speed: " + storeScript.gameData.castleSpeed;

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
