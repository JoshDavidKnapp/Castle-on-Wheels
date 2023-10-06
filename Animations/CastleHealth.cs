using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastleHealth : MonoBehaviour
{

    [Header("References to castle models we will be flipping colors with")]
    public GameData gameData;
    public EnemyData enemyData;
    public GameObject playerCastleBase;
    public GameObject playerCastleWheels;
    public GameObject Canon;

    public GameObject deadCastle;

    public GameObject GameOver;
    //will it need to include the wheels as well for damage indiction?
    //default shaders on main castle
    public Material defaultMat1;
    public Material defaultMat2;
    public Material damagedMat;
    //wheels
    public Material wheelDefault1;
    public Material wheelDefault2;
    //cannon
    public Material cannonDefaut1;

    //array of materials on castle
    private Material[] _matsCastle1;
    private Material[] _matWheels;
    //private Material[] cannon;

    /// <summary>
    /// when castle takes damage, there will be a small inidication for the player and UI will update
    /// </summary>
    [Header("Castle Health and Status")]
    //private int castleHealth;

    public Transform healthBar;
    public Text healthAmount;

    private void Awake()
    {
        gameData.castleHealth = gameData.castleMaxHealth;

        float health = gameData.castleHealth;
        healthBar.localScale = new Vector3(health/gameData.castleMaxHealth, 1f);
        healthAmount.text = gameData.castleHealth.ToString() + "/" + gameData.castleMaxHealth;


        _matsCastle1 = playerCastleBase.GetComponent<Renderer>().materials;
        _matWheels = playerCastleWheels.GetComponent<Renderer>().materials;
    }

    public void Start()
    {
        Time.timeScale = 1;

    }

    public void Update()
    {
        float health = gameData.castleHealth;
        healthBar.localScale = new Vector3(health / gameData.castleMaxHealth, 1f);
        healthAmount.text = gameData.castleHealth.ToString() + "/" + gameData.castleMaxHealth;
        if (gameData.castleHealth <= 0)
        {
            GetComponent<FollowPath>()._blocked = true;
            deadCastle.SetActive(true);
            playerCastleBase.SetActive(false);
            playerCastleWheels.SetActive(false);
            Canon.SetActive(false);

            GameOver.SetActive(true);
            Time.timeScale = 0;

        }
    }

    //damage collisions with towers will be done in castleHitBox script

   // private void OnCollisionEnter(Collision other)
    //{
        //if(other.gameObject.tag == "ExplosiveBarrels")
        //{
            //gameData.castleHealth = gameData.castleHealth - other.gameObject.GetComponent<SiegeTowerBarrels>().barrelDamage;
            //trigger detonation sequence
            //other.gameObject.GetComponent<SiegeTowerBarrels>().BeginDetonation();
            //damageIndication();
        //}
    //}

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy Unit")
        {
            other.gameObject.GetComponent<ENEMY>().currentStatus = status.combat;
            StartCoroutine("Damage");
        }
        if(other.tag == "BigGuyAttack")
        {
            gameData.castleHealth -= enemyData.bDamage;
        }

        if(other.tag == "enemyProjectile")
        {
            //Debug.Log("hit");
            gameData.castleHealth -= other.gameObject.GetComponent<Projectile>().projectileDamage;
        }

        if(other.tag == "cannonballCart")
        {
            //Debug.Log("hit");
            gameData.castleHealth -= other.gameObject.GetComponent<cannonBallBehavior>().cannonBallDamage;
        }

        if(other.tag == "BastillaShot")
        {
            Debug.Log("hit");
            gameData.castleHealth -= other.gameObject.GetComponent<Projectile>().projectileDamage;
        }
    }

    public IEnumerator Damage()
    {

        gameData.castleHealth--;
        yield return new WaitForSeconds(0.5f);
    }

    //everytime our castle takes damage, this needs to run to show the player
    //when take damage the castles colors will change from default to red and blink a few times
    //will have animations, update ui, etc
    public void damageIndication()
    {
        StartCoroutine(DamageFlicker());
    }

    //temp prototype for damage indication (just changes color to different material)
    IEnumerator DamageFlicker()
    {
        bool colorChange = false;
        for(int flicker = 0; flicker <= 5; flicker++)
        {
            yield return new WaitForSeconds(0.07f);
            if(colorChange == false)
            { 
                //change to damange indicated mat (from default)
                _matsCastle1[0] = damagedMat;
                _matsCastle1[1] = damagedMat;
                playerCastleBase.GetComponent<Renderer>().materials = _matsCastle1;
                _matWheels[0] = damagedMat;
                _matWheels[0] = damagedMat;
                playerCastleWheels.GetComponent<Renderer>().materials = _matWheels;
                Canon.gameObject.GetComponent<Renderer>().material = damagedMat;
                colorChange = true;
            }
            else
            {
                //from damage to default
                _matsCastle1[0] = defaultMat1;
                _matsCastle1[1] = defaultMat2;
                playerCastleBase.GetComponent<Renderer>().materials = _matsCastle1;
                _matWheels[0] = wheelDefault1;
                _matWheels[1] = wheelDefault2;
                playerCastleWheels.GetComponent<Renderer>().materials = _matWheels;
                Canon.gameObject.GetComponent<Renderer>().material = cannonDefaut1;
                colorChange = false;
            }
        }
    }
}
