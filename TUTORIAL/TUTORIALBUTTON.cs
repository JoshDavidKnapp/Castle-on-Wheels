using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TUTORIALBUTTON : MonoBehaviour
{
    private GameObject hub;

    private void Start()
    {
        hub = GameObject.Find("TutorialScriptedEventsHub");
    }


    public void pressed()
    {
        hub.gameObject.GetComponent<SCRIPTEDEVENTSHUB>().popQueue();
    }
}
