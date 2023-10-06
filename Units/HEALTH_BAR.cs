using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HEALTH_BAR : MonoBehaviour
{
    
    public GameObject canvas;
    public Image healthBar;
    public EnemyStats thisE;
    public ENEMY_STRUCTURE thisS;
    public Stronghold stronghold;
    public SiegeTracker siegeEngine;
    public AllyMovement ally;
    float health;
    float currentHealth;
    [Header("'structure' for enemy structure")]
    [Header("'enemy' for character")]
    [Header("'stronghold' for stronghold")]
    [Header("'siege engine' for siege engine")]
    [Header("'ally' for ally unit")]
    public string type;

    

    // Start is called before the first frame update
    void Start()
    {
        if (type == "enemy")
        {
            currentHealth = health = thisE.health;
           
        }
        if (type == "structure")
        {
            currentHealth = health = thisS.health;
        }
        if(type == "stronghold")
        {
            currentHealth = health = stronghold.health;
        }
        if(type == "siege engine")
        {
            currentHealth = health = siegeEngine.health;
        }
        if(type == "ally")
        {
            currentHealth = health = ally.health;
        }
        canvas.SetActive(false);
    }

    // Update is called once per frame
   
     IEnumerator showBar()
    {
        if (type == "enemy")
        {
            currentHealth = thisE.health;
        }
        if (type == "structure")
        {
            currentHealth = thisS.health;
        }
        if (type == "stronghold")
        {
            currentHealth = stronghold.health;
        }
        if (type == "siege engine")
        {
            currentHealth = siegeEngine.health;
        }
        if (type == "ally")
        {
            currentHealth = ally.health;
        }
        canvas.SetActive(true);
        healthBar.fillAmount = currentHealth / health;
        canvas.transform.LookAt(Camera.main.transform.position);
        yield return new WaitForSeconds(3);
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
    }
    public void  startShowBar()
    {
        StartCoroutine(showBar());
    }

    
}
