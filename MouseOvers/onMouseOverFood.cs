using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onMouseOverFood : MonoBehaviour
{


    public bool mouseOver = false;



    public void mouseOverButton()
    {
        mouseOver = true;
        if (mouseOver == true)
        {
            GameObject storeContainer = GameObject.Find("Store");
            Store storeScript = storeContainer.GetComponent<Store>();
            storeScript.foodTotalText.text = "Total: " + storeScript.gameData.foodTotal + " +  " + storeScript.foodTotalIncrease;
            storeScript.foodRateText.text = "Regen Rate: " + storeScript.gameData.foodRate + " + " + storeScript.foodRateIncrease;
        }
    }

    public void mouseNotOver()
    {
        mouseOver = false;
        if (mouseOver == false)
        {
            GameObject troopContainer = GameObject.Find("Store");
            Store storeScript = troopContainer.GetComponent<Store>();
            storeScript.foodTotalText.text = "Total: " + storeScript.gameData.foodTotal;
            storeScript.foodRateText.text = "Regen Rate: " + storeScript.gameData.foodRate;
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
