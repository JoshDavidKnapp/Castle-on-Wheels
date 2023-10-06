using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Castle",menuName ="Castle")]
public class Castle : ScriptableObject
{
    public Vector3 currentPos;
    public int minDist = 5;
    public int maxDist = 20;
}
