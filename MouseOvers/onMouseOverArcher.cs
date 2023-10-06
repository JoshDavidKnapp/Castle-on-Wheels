using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onMouseOverArcher : MonoBehaviour
{

    public bool mouseOver = false;



    public void mouseOverButton()
    {
        mouseOver = true;
        if (mouseOver == true)
        {
            GameObject troopContainer = GameObject.Find("Store");
            Store storeScript = troopContainer.GetComponent<Store>();
            storeScript.archerLevelText.text = "Level: " + storeScript.gameData.archerLevel + " +  " + storeScript.archerLevelIncrease;
            storeScript.archerHealthText.text = "Health: " + storeScript.gameData.archerHealth + " + " + storeScript.archerHealthIncrease;
            storeScript.archerDamageText.text = "Damage: " + storeScript.gameData.archerDamage + " + " + storeScript.archerDamageIncrease;
        }
    }

    public void mouseNotOver()
    {
        mouseOver = false;
        if (mouseOver == false)
        {
            GameObject troopContainer = GameObject.Find("Store");
            Store storeScript = troopContainer.GetComponent<Store>();
            storeScript.archerLevelText.text = "Level: " + storeScript.gameData.archerLevel;
            storeScript.archerHealthText.text = "Health: " + storeScript.gameData.archerHealth;
            storeScript.archerDamageText.text = "Damage: " + storeScript.gameData.archerDamage;
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
