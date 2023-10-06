using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockInGame : MonoBehaviour
{
    public GameData gameData;


    public GameObject archerButton;
    public GameObject heavyButton;
    public GameObject balloonButton;
    public GameObject assassinButton;
    public GameObject sorcButton;


    // Start is called before the first frame update
    void Start()
    {
        if(gameData.isArcherUnlocked == true)
        {
            archerButton.SetActive(true);
        }

        if (gameData.isHeavyUnlocked == true)
        {
            heavyButton.SetActive(true);
        }

        if (gameData.isBalloonUnlocked == true)
        {
            balloonButton.SetActive(true);
        }

        if (gameData.isAssassinUnlocked == true)
        {
            assassinButton.SetActive(true);
        }

        if (gameData.isSorcUnlocked == true)
        {
            sorcButton.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
