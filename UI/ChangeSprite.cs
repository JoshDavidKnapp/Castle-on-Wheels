using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour
{

    public Sprite moveCastle;
    public Sprite stopCastle;
    private Image myIMGcomponent;

    public bool isMoving = false;
    // Use this for initialization
    void Start()
    {
        myIMGcomponent = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

   public void ChangeTheSprite()
    {
        if(isMoving == false)
        {
            myIMGcomponent.sprite = stopCastle;
            isMoving = true;

        }
        else if (isMoving == true)
        {
            myIMGcomponent.sprite = moveCastle;
            isMoving = false;
        }
        

       

    }
}
