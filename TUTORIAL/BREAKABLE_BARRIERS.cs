using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// 
/// A barrier is to have some sort of object to it for the player to get before it can disappear and progress. 
/// Used in tutorials and scripted events. 
/// For each barrier there is a node that is linked to it. We can have multiple nodes. 
/// Just type it in inspector and attatch corresponding gameobject :D
/// </summary>
public class BREAKABLE_BARRIERS : MonoBehaviour
{
    [Header("Wall Stats - Set Number of Barriers Here")]
    public GameObject[] barrierNodes;

    private int _barrierNodesNum;

    [Header("Barrier Variables")]
    public bool wallActive = true;

    //to avoid a repetition for startCoroutine
    private bool _delayActive = false;

    [Header("Wait time before barrier deactivation")]
    public float destructWaitTime = 2.0f;

    [Header("The Radius stopping castle")]
    public float checkRadius = 10f;

    [Header("Is Castle Stopped at Wall")]
    public bool castleInRange = false;

    [Header("Castle Player ref")]
    public GameObject player;

    [Header("Pathblocking Objs")]
    //public GameObject debris;
    public GameObject[] debrisObjs;

    [Header("On Objectives met")]
    public bool blowUpOnObjectivesMet = false;
    public bool triggerEventOnObjectivesMet = false;
    //public GameObject triggerEvent;

    [Header("Toggle is scripted Tutorial")]
    [Header("NOT IN USE")]
    [Header("KEEP FALSE")]
    public bool cameraShowsNodes = false;
    public GameObject CamSciptObj;

    [Header("Explosion Effect")]
    public ParticleSystem explosionEffect;
    

    //castle nearby
    Transform[] _nearbyCastle;

    //on start it sets the objective set up in the inspector
    void Start()
    {
        _barrierNodesNum = barrierNodes.Length;
        bool[] barActive = new bool[_barrierNodesNum];
    }

    void FixedUpdate()
    {
        if(CheckNodes() && !_delayActive)
        {
            _delayActive = true;
            StartCoroutine(DeactivateDelay());           
        }

        //if castle is in range, it will activate a bool on the castle movement that while active the player cannot progress forward any more until breakable barrier is gone
        if(CastleCheck() && wallActive)
        {
            //Debug.Log("In Range");
            castleInRange = true;
            player.GetComponent<FollowPath>()._isMoving = false;
            player.GetComponent<FollowPath>()._blocked = true;
        }
        else
        {
            castleInRange = false;
        }

        //CheckNodesDeactivation();
    }

    bool CastleCheck()
    {
        bool castleInRange = false;
        //if castle is close, it cannot move
        _nearbyCastle = collidersToTransforms(Physics.OverlapSphere(transform.position, checkRadius));
        foreach (Transform castleTarget in _nearbyCastle)
        {
            if (castleTarget.gameObject.tag == "castlePlayer")
            {
                castleInRange = true;

            }
        }

        return castleInRange;
    }

    bool CheckNodes()
    {
        bool allOff = true;
        for(int x = 0; x < _barrierNodesNum; x++)
        {
            if(barrierNodes[x] != null && barrierNodes[x].gameObject.GetComponent<BARRIER_NODE>().objectiveMet == false)
            {
                allOff = false;
                break;
            }
        }
        return allOff;
    }

    void CheckNodesDeactivation()
    {
        for (int x = 0; x < _barrierNodesNum; x++)
        {
            if (barrierNodes[x] != null && barrierNodes[x].gameObject.GetComponent<BARRIER_NODE>().objectiveMet == true)
            {
                //debrisObjs[x].gameObject.SetActive(false);
                StartCoroutine(NodeDestruction(debrisObjs[x], barrierNodes[x].GetComponent<BARRIER_NODE>().timeDetonation));
            }
        }
    }

    public void DestroyNode(int nodeNum, float timeToDetonate)
    {
        if(this.gameObject.activeInHierarchy)
            StartCoroutine(NodeDestruction(debrisObjs[nodeNum], timeToDetonate));
    }

    IEnumerator NodeDestruction(GameObject node, float timeToDetonate)
    {
        yield return new WaitForSeconds(timeToDetonate);
        node.SetActive(false);
    }

    //add delay for deactivating wall thing
    IEnumerator DeactivateDelay()
    {
        Debug.Log("Wall Destoryed " + this.gameObject.name);
        explosionEffect.Play();
        yield return new WaitForSeconds(destructWaitTime);
        wallActive = false;

        //this.gameObject.SetActive(false);
        //events
        if (triggerEventOnObjectivesMet)
        {
            //trigger event here
            //explosionEffect.Play();
            StartCoroutine(Detonation());
        }

        //destory the gameobjects
        if (blowUpOnObjectivesMet)
            Destroy(this.gameObject);
        else
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            player.GetComponent<FollowPath>()._isMoving = false;
            player.GetComponent<FollowPath>()._blocked = false;
        }
                
    }

    private Transform[] collidersToTransforms(Collider[] colliders)
    {
        Transform[] transforms = new Transform[colliders.Length];
        for (int i = 0; i < colliders.Length; i++)
        {
            transforms[i] = colliders[i].transform;
        }
        return transforms;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }

    //when this obj is destoyed, turn off bool on player so they can keep moving forward
    private void OnDestroy()
    {
        if(wallActive == false && player.activeInHierarchy == false)
            player.GetComponent<FollowPath>()._blocked = false;
    }

    IEnumerator Detonation()
    {
        this.GetComponent<MeshRenderer>().enabled = false;
        //this.GetComponent<Rigidbody>().isKinematic = true;
        //bool change = false;
        //explode, wait, destroy
        //explosionEffect.Play();
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
        //this.gameObject.SetActive(false);
    }
}
