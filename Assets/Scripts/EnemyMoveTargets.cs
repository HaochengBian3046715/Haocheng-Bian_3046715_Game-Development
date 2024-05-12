using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveTargets : MonoBehaviour
{
    public Transform[] targets;

    int index = 0;
    public Transform GetCurrentTargetPoint()
    {

        var t = targets[index];

        return t;

    }

    public void NextTarget()
    {
        index++;
        index = (index >= targets.Length) ? 0 : index;
    }
}
