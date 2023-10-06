using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    public GameData gameData;

    public Transform healthBar;

    public int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        gameData.castleHealth = gameData.castleMaxHealth;
        float health = gameData.castleMaxHealth;
        //health = 100;
        //healthBar.localScale = new Vector3(gameData.castleHealth / 100, 1f);
    }



    // Update is called once per frame
    void Update()
    {
        //float health = gameData.castleHealth;
        //healthBar.localScale = new Vector3(gameData.castleHealth / 100, 1f);

        if (gameData.castleHealth <= 0)
        {
            gameData.castleHealth = 0;

        }

        

    }

    public void SetSize()
    {
        
        
        
    }
}

   

