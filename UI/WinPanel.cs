using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPanel : MonoBehaviour
{

    public GameObject winPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "castlePlayer")
        {
            winPanel.SetActive(true);

        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
