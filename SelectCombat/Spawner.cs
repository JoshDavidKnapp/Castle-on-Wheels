using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{

    public GameData gameData;
    [Header("Sript For Ally Spawning Function")]
    // public ALLIED_SPAWNING spawnFunction;
    [Header("Input Ally Prefab Here")]
    //public GameObject ally;
    Plane _plane;

    RaycastHit hit;
    public Castle player;

    public GameObject ally;

    public GameObject archer;
    public GameObject heavy;
    public GameObject balloon;
    public GameObject assassin;
    public GameObject sorcerer;

    public bool spawnAlly = true;
    public bool spawnArcher = false;

    public Transform foodBar;

    

    //public int damage = 10;

    public Text foodAmount;

    private void Start()
    {
        _plane = new Plane(transform.up, Vector3.zero);
        StartCoroutine(foodRegen());
        gameData.foodCurrent = gameData.foodTotal;
        float food = gameData.foodCurrent;
        foodBar.localScale = new Vector3(1f, 1f);
        foodAmount.text = gameData.foodCurrent.ToString() + "/" + gameData.foodTotal;


    }

    private void Update()
    {
        
        float food = gameData.foodCurrent;
        foodBar.localScale = new Vector3(food / 100, 1f);
        foodAmount.text = gameData.foodCurrent.ToString() + "/" + gameData.foodTotal;


        if (gameData.foodCurrent < 0)
        {
            gameData.foodCurrent = 0;

        }


        if (gameData.foodCurrent > gameData.foodTotal)
        {
            gameData.foodCurrent = gameData.foodTotal;

        }


    }


   
   // public void clickSpawn()
   // {
   //     
   //
   //     if (Input.GetMouseButtonDown(0))
   //     {
   //
   //
   //
   //         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
   //
   //         if (Physics.Raycast(ray, out hit, 1000))
   //         {
   //
   //             if ((Vector3.Distance(hit.point, player.currentPos) < player.maxDist) && (Vector3.Distance(hit.point, player.currentPos) > player.minDist) && gameData.foodCurrent >= damage && spawnAlly == true)
   //             {
   //
   //                 Instantiate(ally, hit.point, Quaternion.identity);
   //                 gameData.foodCurrent -= damage;
   //
   //
   //             }
   //
   //             if ((Vector3.Distance(hit.point, player.currentPos) < player.maxDist) && (Vector3.Distance(hit.point, player.currentPos) > player.minDist) && gameData.foodCurrent >= damage && spawnArcher == true)
   //             {
   //
   //                 Instantiate(archer, hit.point, Quaternion.identity);
   //                 gameData.foodCurrent -= damage;
   //
   //
   //
   //             }
   //
   //
   //         }
   //     }
   // }
    IEnumerator foodRegen()
    {
        for (; ; )
        {
            if (gameData.foodCurrent < gameData.foodTotal)
            {
                gameData.foodCurrent+=5;
            }
            yield return new WaitForSeconds(3f);
        }
    }

    public void SpawnAlly()
    {
        if(gameData.foodCurrent >= gameData.militiaCost && !GetComponentInParent<FollowPath>()._isMoving)
        {
            
            Instantiate(ally, this.transform.position, Quaternion.identity);
            gameData.foodCurrent -= gameData.militiaCost;   // changed from "damage" to represent unit cost 
            //ally.GetComponent<Click>().ClearSelection();
        }
    }

    public void SpawnArcher()
    {
        Debug.Log(this.name);
        if (gameData.foodCurrent >= gameData.archerCost && !GetComponentInParent<FollowPath>()._isMoving)
        {
            
            Instantiate(archer, this.transform.position, Quaternion.identity);
            gameData.foodCurrent -= gameData.archerCost;   // changed from "damage" to represent unit cost
            //archer.GetComponent<Click>().ClearSelection();
        }
    }

    public void SpawnHeavy()
    {
        Debug.Log(this.name);
        if (gameData.foodCurrent >= gameData.heavyCost && !GetComponentInParent<FollowPath>()._isMoving)
        {
            
            Instantiate(heavy, this.transform.position, Quaternion.identity);
            gameData.foodCurrent -= gameData.heavyCost;   // changed from "damage" to represent unit cost
            //heavy.GetComponent<Click>().ClearSelection();
        }
    }

    public void SpawnBalloon()
    {
        Debug.Log(this.name);
        if (gameData.foodCurrent >= gameData.balloonCost && !GetComponentInParent<FollowPath>()._isMoving)
        {
            Instantiate(balloon, this.transform.position, Quaternion.identity);
            gameData.foodCurrent -= gameData.balloonCost;   // changed from "damage" to represent unit cost
        }
    }

    public void SpawnAssassin()
    {
        Debug.Log(this.name);
        if (gameData.foodCurrent >= gameData.assassinCost && !GetComponentInParent<FollowPath>()._isMoving)
        {
            Instantiate(assassin, this.transform.position, Quaternion.identity);
            gameData.foodCurrent -= gameData.assassinCost;   // changed from "damage" to represent unit cost
        }
    }

    public void SpawnSorcerer()
    {
        Debug.Log(this.name);
        if (gameData.foodCurrent >= gameData.sorcererCost && !GetComponentInParent<FollowPath>()._isMoving)
        {
            Instantiate(sorcerer, this.transform.position, Quaternion.identity);
            gameData.foodCurrent -= gameData.sorcererCost;   // changed from "damage" to represent unit cost
        }
    }
}
