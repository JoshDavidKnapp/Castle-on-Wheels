using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodBar : MonoBehaviour
{
    public GameData gameData;

    public Transform foodBar;

    public int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        gameData.foodCurrent = 100;
        float food = gameData.foodTotal;
        food = 100;
       // foodBar.localScale = new Vector3(0.5f, 0.5f);
    }



    // Update is called once per frame
    void Update()
    {
        float food = gameData.foodTotal;
       // foodBar.localScale = new Vector3(0.5f / 0.5f, 1f);

        if (gameData.foodTotal < 0)
        {
            gameData.foodTotal = 0;

        }

        if(food>gameData.foodTotal)
        {
            food = gameData.foodTotal;
        }


        


    
    }

    public void SetSize()
    {
        //healthBar.localScale = new Vector3(gameData.castleHealth)
    }


}
