using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onMouseOverBalloon : MonoBehaviour
{



    public bool mouseOver = false;



    public void mouseOverButton()
    {
        mouseOver = true;
        if (mouseOver == true)
        {
            GameObject troopContainer = GameObject.Find("Store");
            Store storeScript = troopContainer.GetComponent<Store>();
            storeScript.balloonLevelText.text = "Level: " + storeScript.gameData.balloonLevel + " +  " + storeScript.balloonLevelIncrease;
            storeScript.balloonHealthText.text = "Health: " + storeScript.gameData.balloonHealth + " + " + storeScript.balloonHealthIncrease;
            storeScript.balloonDamageText.text = "Damage: " + storeScript.gameData.balloonDamage + " + " + storeScript.balloonDamageIncrease;
        }
    }

    public void mouseNotOver()
    {
        mouseOver = false;
        if (mouseOver == false)
        {
            GameObject troopContainer = GameObject.Find("Store");
            Store storeScript = troopContainer.GetComponent<Store>();
            storeScript.balloonLevelText.text = "Level: " + storeScript.gameData.balloonLevel;
            storeScript.balloonHealthText.text = "Health: " + storeScript.gameData.balloonHealth;
            storeScript.balloonDamageText.text = "Damage: " + storeScript.gameData.balloonDamage;
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
