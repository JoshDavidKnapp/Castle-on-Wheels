using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Unlock : MonoBehaviour
{
    public GameData gamedata;

    public GameObject level2Panel;
    public GameObject level3Panel;
    public GameObject level4Panel;





    // Start is called before the first frame update
    void Start()
    {
        if (gamedata.isLevel1Complete == true)
        {
            level2Panel.SetActive(false);
            gamedata.isLevel1Complete = true;
        }

        if (gamedata.isLevel2Complete == true)
        {
            level3Panel.SetActive(false);
            gamedata.isLevel2Complete = true;
        }

        if (gamedata.isLevel3Complete == true)
        {
            level4Panel.SetActive(false);
            gamedata.isLevel3Complete = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
