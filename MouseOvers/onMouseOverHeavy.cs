using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onMouseOverHeavy : MonoBehaviour
{

    public bool mouseOver = false;



    public void mouseOverButton()
    {
        mouseOver = true;
        if (mouseOver == true)
        {
            GameObject troopContainer = GameObject.Find("Store");
            Store storeScript = troopContainer.GetComponent<Store>();
            storeScript.heavyLevelText.text = "Level: " + storeScript.gameData.heavyLevel + " +  " + storeScript.heavyLevelIncrease;
            storeScript.heavyHealthText.text = "Health: " + storeScript.gameData.heavyHealth + " + " + storeScript.heavyHealthIncrease;
            storeScript.heavyDamageText.text = "Damage: " + storeScript.gameData.heavyDamage + " + " + storeScript.heavyDamageIncrease;
        }
    }

    public void mouseNotOver()
    {
        mouseOver = false;
        if (mouseOver == false)
        {
            GameObject troopContainer = GameObject.Find("Store");
            Store storeScript = troopContainer.GetComponent<Store>();
            storeScript.heavyLevelText.text = "Level: " + storeScript.gameData.heavyLevel;
            storeScript.heavyHealthText.text = "Health: " + storeScript.gameData.heavyHealth;
            storeScript.heavyDamageText.text = "Damage: " + storeScript.gameData.heavyDamage;
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
