using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MoveButton : MonoBehaviour
{
    public AudioSource castleWheels;
    public GameObject castle;
    public GameObject panel;
    //public Text moveButtonText;
    private string moveString = "Move Castle";
    private string stopString = "Stop";

    public bool isStopped = true;

    public void ChangeText()
    {
     
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(castle.GetComponent<FollowPath>()._isMoving)
        {
           // moveButtonText.text = stopString;
        }
        else
        {
           // moveButtonText.text = moveString;
        }

    }

    public void Stop()
    {
        
       // moveButtonText.text = moveString;

        isStopped = true;
        castle.GetComponent<FollowPath>().stopMoving();
        castleWheels.Stop();

    }

    public void Go()
    {
       // moveButtonText.text = stopString;
        isStopped = false;
        castleWheels.Play();
        castle.GetComponent<FollowPath>().continueMoving();
    }

    public void Changer()
    {
        if(isStopped)
        {
            Go();
            panel.SetActive(true);
        }
        else
        {
            Stop();
            panel.SetActive(false);

        }
    }
}
