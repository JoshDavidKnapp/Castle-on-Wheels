using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlliedSpawning : MonoBehaviour
{
    public GameData gameData;
    [Header("Sript For Ally Spawning Function")]
    public ALLIED_SPAWNING spawnFunction;
    [Header("Input Ally Prefab Here")]
    //public GameObject ally;
    Plane _plane;

    RaycastHit hit;
    public Castle player;


    public Transform foodBar;

    public int damage = 10;

    public Text foodAmount;

    private void Start()
    {
        _plane = new Plane(transform.up, Vector3.zero);
        //food = 10;
        StartCoroutine(foodRegen());

        float food = gameData.foodCurrent;
        foodBar.localScale = new Vector3(1f, 1f);
        foodAmount.text = gameData.foodCurrent.ToString() + "/" + gameData.foodTotal;


    }

    private void Update()
    {
        clickSpawn();
        float food = gameData.foodCurrent;
        foodBar.localScale = new Vector3(food / 100, 1f);
        foodAmount.text = gameData.foodCurrent.ToString() + "/" + gameData.foodTotal;


        if (gameData.foodCurrent < 0)
        {
            gameData.foodCurrent = 0;

        }


    }


/*
private void OnMouseDown()
{
    //Checks if the Ally Spawn Zone has been clicked. if it is, then a raycast determines the exact position and spawns an ally unit
    if (gameObject.tag == "allySpawnArea" && gameData.foodTotal > 0)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.Log("dfas");
        if (Physics.Raycast(ray, out hit, 200f))
        {
            Debug.Log("Raycast Hit at: " + hit.point);
            var hitPoint = hit.point;
            //hitPoint.y = transform.position.y;

            Vector3 clickedPosition = hitPoint;
            //Instantiate(ally, clickedPosition, Quaternion.identity);
            spawnFunction.Spawn(clickedPosition);
            gameData.foodTotal--;
            print("Food: " + gameData.foodTotal);

        }


    }
}
*/
public void clickSpawn()
    {
        //int layerMask = 1 << 8;
        //layerMask = ~layerMask;
       
            if (Input.GetMouseButtonDown(0))
             {
            


                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 1000))
                {

                    if ((Vector3.Distance(hit.point, player.currentPos) < player.maxDist) && (Vector3.Distance(hit.point, player.currentPos) > player.minDist) && gameData.foodCurrent>=damage)
                    {

                        spawnFunction.Spawn(hit.point);

                        gameData.foodCurrent -= damage;


                    }
               

                }
            }
    }
    IEnumerator foodRegen()
    {
        for (; ; )
        {
            if (gameData.foodCurrent < gameData.foodTotal)
            {
                gameData.foodCurrent++;
            }
            yield return new WaitForSeconds(3f);
        }
    }
}