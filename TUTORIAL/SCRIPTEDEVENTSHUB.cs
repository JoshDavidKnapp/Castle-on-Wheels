using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


/// <summary>
/// Will run the text and overall scripted tutorial based off the barriers objects
/// 
/// First barrier will introduce main concepts such as movement and mechanics (spawning). Will only have one node
/// 
/// Second barrier will not show much in terms of checks. Will have two nodes.
/// 
/// ***use queues of strings to display texts. After prototype I will be changing this to display tutorial text through the use of queues and a button 
/// </summary>
public class SCRIPTEDEVENTSHUB : MonoBehaviour
{
    public Queue _q = new Queue();

    //[Header ("The player :D")]
    //public GameObject castlePlayer;

    [Header ("UI Text")]
    public Text mainText;

    [Header ("All barriers in scene")]
    public GameObject barrier1;
    public GameObject barrier2;

    [Header ("Showing when scripteEventHub needs control of camera")]
    public bool camTutorialActive = false;

    //to avoid repeats and unwanted things to activate
    private bool _barrierNodeActive;
    private bool _text1Active = false;
    private bool _text2Active = false;
    private bool _text3Active = false;
    private bool _text4Active = true;
    private bool _text5Active = false;
    private bool _text6Active = false;
    private bool _text7Active = false;
    private bool _avoidRepeatDequeue = false;


    //will turn on when _targetingActive in barrier one is active, will turn off when barrierOne is destroyed
    private bool _barrierOneActive = false;
    private bool _barrierOneInitiated = false;
    private bool _barrierTwoActive = false;
    private bool _barrierTwoInitiated = false;

    //bools used for buttons to initiate if players spawn
    private bool _alliesSpawned = false;
    private bool _alliesMoved = false;

    [Header ("Button for future tutorial mechanic (WIP)")]
    //when button is pressed and player is on barrier one, will display text
    public Button changeText;

    //when player reaches final castle
    public bool finalNode1 = false;
    public bool finalNode2 = false;
    public bool stoppedAtCastle = false;

    //for button colors if we end up using the
    //private ColorBlock _ogColors;

    void Start()
    {
        //_ogColors = changeText.colors;
        //mainText.text = "To move your camera around use WASD. Toggle your castle movement with spacebar.";
        _q.Enqueue("To move your camera around use WASD. Toggle your castle movement with spacebar");
        mainText.text = _q.Dequeue().ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //access the barrierCamShowTrigger game object
        if(_barrierOneInitiated == false && barrier1.transform.GetChild(1).gameObject.GetComponent<SCRIPTED_CAM>()._targetingActive == true)
        {
            _barrierOneInitiated = true;
            _barrierOneActive = true;
            StartCoroutine(barrierOne());
        }
        if(_barrierTwoInitiated == false && barrier2.transform.GetChild(1).gameObject.GetComponent<SCRIPTED_CAM>()._targetingActive == true)
        {
            _barrierTwoActive = true;
            _barrierTwoInitiated = true;
        }

        //show players how to spawn (when targeting active is off and barrier one is active)
        if(_text4Active == false && barrier1.transform.GetChild(1).gameObject.GetComponent<SCRIPTED_CAM>()._targetingActive == false && barrier1.GetComponent<BREAKABLE_BARRIERS>().wallActive == true)
        {
            _text4Active = true;
            //Debug.Log("To spawn your allies click inside the circle around your castle.");
            _q.Enqueue("To spawn your allies click the spawn button");
            //mainText.text = "To spawn your allies click the spawn button";
            StartCoroutine(spawning());
        }

        //display text for first node being destoyed
        if(_text3Active == false && barrier1.transform.GetChild(2).gameObject.GetComponent<BARRIER_NODE>().objectiveMet == true)
        {
            _text3Active = true;
            _q.Enqueue("Node destroyed!");
            Debug.Log("Node destoryed!");
            //mainText.text = "Node destoryed!";
        }

        //if the barrier is destroyed then false
       if(barrier1.GetComponent<BREAKABLE_BARRIERS>().wallActive == false)
        {
            _barrierOneActive = false;
            if(_text1Active == false)
            {
                _text1Active = true;
                _q.Enqueue("Time to move to the next barrier. Press spacebar to continue moving forward");
                Debug.Log("Time to move to the next barrier. Press spacebar to continue moving forward");
               // mainText.text = "Time to move to the next barrier. Press spacebar to continue moving forward";
            }
        }

       if(barrier2.GetComponent<BREAKABLE_BARRIERS>().wallActive == false)
        {
            _barrierTwoActive = false;
            if(_text2Active == false)
            {
                _text2Active = true;
                _q.Enqueue("Time to move and destroy the Enemy Castle!");
                Debug.Log("Time to move and destroy the Enemy Castle!");
                //mainText.text = "Time to move and destroy the Enemy Castle!";
            }
        }

       //must have bool to show when we take control of cam to temporarly stop any overriding from cam controller obj
       if(barrier1.transform.GetChild(1).gameObject.GetComponent<SCRIPTED_CAM>()._targetingActive == true || barrier2.transform.GetChild(1).gameObject.GetComponent<SCRIPTED_CAM>()._targetingActive == true)
            camTutorialActive = true;
       else
            camTutorialActive = false;

        //if (barrier2.transform.GetChild(1).gameObject.GetComponent<SCRIPTED_CAM>()._targetingActive == true)
            //camTutorialActive = true;
        //else
            //camTutorialActive = false;

        //final approach nodes
        if(_text5Active == false && finalNode1)
        {
            _text5Active = true;
            //mainText.text = "Now that you are approaching the stronghold, you must be aware of its strong weapondry";
           _q.Enqueue("Now that you are approaching the stronghold, you must be aware of its strong weapondry");
        }
        if(_text6Active == false && finalNode2)
        {
            _text6Active = true;
            //mainText.text = "The stronghold has two high powered weapons capable of taking out your castle if your not careful";
            _q.Enqueue("The stronghold has two high powererd weapons capable of taking out your castle if your not careful");
        }
        if(_text7Active == false && stoppedAtCastle)
        {
            _text7Active = true;
            //mainText.text = "Having a group of units to attack at once can overwelm the defense and insure you victory!";
            _q.Enqueue("Having a group of units to attack at once can overwelm the defense and insure you victory!");
        }

        textManager();
        //Debug.Log("next text" + _q.Peek());
    }

    //every time the player hits button, pop q to display last in first out
    void textManager()
    {
        if(_q.Count != 0)
        {
            changeText.enabled = true;
            //if(_avoidRepeatDequeue == false)
            //{
                //changeText.onClick.AddListener(popQueue);
            //}
            
            changeText.GetComponentInChildren<Text>().text = "Next hint";
        }
        else
        {
            changeText.enabled = false;
            changeText.GetComponentInChildren<Text>().text = "No Hints currently";
        }
    }

    public void popQueue()
    {
        if (_q.Count != 0)
        {
            Debug.Log(_q.Count);
            //text obj
            mainText.text = _q.Dequeue().ToString();

            //button text
            
            //revert back to og color
            //ColorBlock cdOG = changeText.colors;
            //cdOG.normalColor = new Color(245, 245, 245);
            //changeText.colors = cdOG;


            changeText.GetComponentInChildren<Text>().text = "Next hint";
        }
        else
        {
            changeText.GetComponentInChildren<Text>().text = "No Hints currently";
        }
    }

    IEnumerator barrierOne()
    {
        //mainText.text = "Walls can be destroyed or moved by destroying their acompaning nodes. Move your allies to the acompaning nodes to destroy them";
        Debug.Log("Walls can be destroyed or moved by destroying their acompaning nodes. Move your allies to the acompaning nodes to destroy them");
        _q.Enqueue("Walls can be destroyed or moved by destroying their acompaning nodes. Move your allies to the acompaning nodes to destroy them");
        yield return new WaitForSeconds(4.5f);
       // mainText.text = "Spawn and Direct your forces to move to the Nodes to destory them and open the gate!";
        Debug.Log("Spawn and Direct your forces to move to the Nodes to destory them and open the gate!");
        _q.Enqueue("Spawn and Direct your forces to move to the Nodes to destory them and open the gate!");
        yield return new WaitForSeconds(3f);
        _text4Active = false;
    }

    IEnumerator spawning()
    {
        //mainText.text = "To spawn your allies click inside the circle around your castle.";
        _q.Enqueue("To spawn your allies click inside the circle around your castle.");
        yield return new WaitForSeconds(3.0f);
        _q.Enqueue("Remember you have limited resources to spawn your troops, so be careful!");
       // mainText.text = "Remember you have limited resources to spawn your troops, so be careful!";
    }
}
