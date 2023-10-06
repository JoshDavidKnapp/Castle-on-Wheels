using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(this.gameObject.tag == "Ally" && other.gameObject.tag == "Enemy Unit")
        {
            print("COLLIDED");
        }

        if(this.gameObject.tag == "Enemy Unit" && other.gameObject.tag == "Ally")
        {
            print("COLLIDED into Ally");
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy Unit" && this.gameObject.tag == "Ally")
        {
            print("COLLIDED");
        }

        if (this.gameObject.tag == "Enemy Unit" && other.gameObject.tag == "Ally")
        {
            print("COLLIDED into Ally");
        }
    }
}
