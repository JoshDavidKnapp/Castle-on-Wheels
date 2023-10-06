using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOn : MonoBehaviour
{
    [SerializeField]
    public Material red;

    [SerializeField]
    public Material green;

    private MeshRenderer myRend;
    private MeshRenderer[] rends;

    [HideInInspector]
    public bool currentlySelected = false;
    // Start is called before the first frame update
    void Start()
    {
        myRend = GetComponent<MeshRenderer>();
        rends = GetComponentsInChildren<MeshRenderer>();
        Camera.main.gameObject.GetComponent<Click>().selectableObjects.Add(this.gameObject);
        ClickMe();
    }

    public void ClickMe()
    {
        if(currentlySelected == false)
        {
            //myRend.material = red;
            SetRendsRed();
        }
        else
        {
            //myRend.material = green;
            SetRendsGreen();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void SetRendsGreen()
    {
        foreach(MeshRenderer rend in rends)
        {
            rend.material = green;
        }
    }

    void SetRendsRed()
    {
        foreach (MeshRenderer rend in rends)
        {
            rend.material = red;
        }
    }
}
