using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialArrow : MonoBehaviour
{
    public Camera cam;
    private bool yDir = false;
    [Header("Arrow is on")]
    public bool arrowOn = true;

    public bool isVisible = false;
    public float counter;

    public bool onTimer = true;
    private bool lookInstance = false;

    public float timeStaysOn = 6;
    public float timeStaysOff = 15;
   
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ymove());
    }

    // Update is called once per frame
    void Update()
    {
        if(arrowOn)
        {
            if (yDir)
                transform.position += new Vector3(0, 1 * Time.deltaTime, 0);
            else
                transform.position += new Vector3(0, -1 * Time.deltaTime, 0);

            transform.Rotate(0, 1, 0, Space.World);
        }
        
        if(ObjectVisible())
        {
            //Debug.Log("We can see " + this.name);
            isVisible = true;
        }
        else
        {
            //Debug.Log("We canNNOT see " + this.name);
            isVisible = false;
            counter = 0;
            onTimer = true;
        }

        //if object is visible and countdown hasnt been started
        if(isVisible && onTimer)
        {
            arrowOn = true;
            counter += Time.deltaTime;
            if (counter >= timeStaysOn)
            {
                //Debug.Log("turn off");
                onTimer = false;
                this.GetComponent<Renderer>().enabled = false;
                arrowOn = false;
                counter = 0;
            }
        }

        if(isVisible && !onTimer)
        {
            counter += Time.deltaTime;
            if (counter >= timeStaysOff)
            {
                onTimer = true;
                this.GetComponent<Renderer>().enabled = true;
                arrowOn = true;
                counter = 0;
            }
        }
    }

    //move in y dir
    IEnumerator ymove()
    {
        yield return new WaitForSeconds(1.0f);
        if (yDir)
            yDir = false;
        else
            yDir = true;

        StartCoroutine(ymove());

    }

    //is it visible
    bool ObjectVisible()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
        if (GeometryUtility.TestPlanesAABB(planes, this.GetComponent<Collider>().bounds))
            return true;
        else
            return false;
    }
}

