using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockPanel : MonoBehaviour
{
    public GameData data;

    public int archerCoins = 100;
    public int heavyCoins = 150;
    public int balloonCoins = 200;
    public int assassinCoins = 250;
    public int sorcCoins = 300;

    public GameObject archerUnlockPanel;
    public GameObject heavyUnlockPanel;
    public GameObject balloonUnlockPanel;
    public GameObject assassinUnlockPanel;
    public GameObject sorcUnlockPanel;





    public void ArcherCoins()
    {

        

        if (data.coins >= archerCoins)
        {
            data.coins -= archerCoins;
            GameObject troopContainer = GameObject.Find("Store");
            Store storeScript = troopContainer.GetComponent<Store>();
            storeScript.coinsText.text = "Coins: " + data.coins.ToString();
            archerUnlockPanel.SetActive(false);
            data.isArcherUnlocked = true;
           
        
        }

    }

    public void HeavyCoins()
    {
        if (data.coins >= heavyCoins)
        {
            data.coins -= heavyCoins;
            GameObject troopContainer = GameObject.Find("Store");
            Store storeScript = troopContainer.GetComponent<Store>();
            storeScript.coinsText.text = "Coins: " + data.coins.ToString();
            heavyUnlockPanel.SetActive(false);
            data.isHeavyUnlocked = true;

        }
    }

    public void BalloonCoins()
    {
        if (data.coins >= balloonCoins)
        {
            data.coins -= balloonCoins;
            GameObject troopContainer = GameObject.Find("Store");
            Store storeScript = troopContainer.GetComponent<Store>();
            storeScript.coinsText.text = "Coins: " + data.coins.ToString();
            balloonUnlockPanel.SetActive(false);
            data.isBalloonUnlocked = true;

        }
    }

    public void AssassinCoins()
    {
        if (data.coins >= assassinCoins)
        {
            data.coins -= assassinCoins;
            GameObject troopContainer = GameObject.Find("Store");
            Store storeScript = troopContainer.GetComponent<Store>();
            storeScript.coinsText.text = "Coins: " + data.coins.ToString();
            assassinUnlockPanel.SetActive(false);
            data.isAssassinUnlocked = true;

        }
    }

    public void SorcCoins()
    {
        if (data.coins >= sorcCoins)
        {
            data.coins -= sorcCoins;
            GameObject troopContainer = GameObject.Find("Store");
            Store storeScript = troopContainer.GetComponent<Store>();
            storeScript.coinsText.text = "Coins: " + data.coins.ToString();
            sorcUnlockPanel.SetActive(false);
            data.isSorcUnlocked = true;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (data.isArcherUnlocked == false)
        {
            archerUnlockPanel.SetActive(true);

        }

        if (data.isHeavyUnlocked == false)
        {
            heavyUnlockPanel.SetActive(true);

        }

        if (data.isBalloonUnlocked == false)
        {
            balloonUnlockPanel.SetActive(true);

        }

        if (data.isAssassinUnlocked == false)
        {
            assassinUnlockPanel.SetActive(true);

        }

        if (data.isSorcUnlocked == false)
        {
            sorcUnlockPanel.SetActive(true);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
