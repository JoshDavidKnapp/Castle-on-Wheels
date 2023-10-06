using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onMouseOverAssassin : MonoBehaviour
{

    public bool mouseOver = false;



    public void mouseOverButton()
    {
        mouseOver = true;
        if (mouseOver == true)
        {
            GameObject troopContainer = GameObject.Find("Store");
            Store storeScript = troopContainer.GetComponent<Store>();
            storeScript.assassinLevelText.text = "Level: " + storeScript.gameData.assassinLevel + " +  " + storeScript.assassinLevelIncrease;
            storeScript.assassinHealthText.text = "Health: " + storeScript.gameData.assassinHealth + " + " + storeScript.assassinHealthIncrease;
            storeScript.assassinDamageText.text = "Damage: " + storeScript.gameData.assassinDamage + " + " + storeScript.assassinDamageIncrease;
        }
    }

    public void mouseNotOver()
    {
        mouseOver = false;
        if (mouseOver == false)
        {
            GameObject troopContainer = GameObject.Find("Store");
            Store storeScript = troopContainer.GetComponent<Store>();
            storeScript.assassinLevelText.text = "Level: " + storeScript.gameData.assassinLevel;
            storeScript.assassinHealthText.text = "Health: " + storeScript.gameData.assassinHealth;
            storeScript.assassinDamageText.text = "Damage: " + storeScript.gameData.assassinDamage;
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
