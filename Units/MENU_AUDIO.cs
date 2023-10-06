using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MENU_AUDIO : MonoBehaviour
{
    private static MENU_AUDIO menuA;

    private void Start()
    {
        DontDestroyOnLoad(this);

        if (menuA == null)
        {
            menuA = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }



}
