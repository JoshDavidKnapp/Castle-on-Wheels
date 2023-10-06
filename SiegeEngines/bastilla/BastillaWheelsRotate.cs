using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BastillaWheelsRotate : MonoBehaviour
{
    //bastilla mesh was off so each wheel its its own local space
    public void Rotate()
    {
        transform.Rotate(0, 0, 90 * Time.deltaTime, Space.Self);
    }
}
