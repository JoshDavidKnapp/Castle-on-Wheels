using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterSpawn : MonoBehaviour
{

    public static bool canSet = true;

    // Start is called before the first frame update
    void Start()
    {
       if(canSet == false)
        {
            StartCoroutine(Destroy());

        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Delete()
    {
        Destroy(this.gameObject);
        canSet = true;

    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
        canSet = true;
    }
}
