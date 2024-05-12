using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovePointClick : MonoBehaviour
{
    public NavMeshAgent agent;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, 100))
            {
                MoveWithPoint(hit.point);
            }
        }
    }


    void MoveWithPoint(Vector3 pos)
    {
        agent.SetDestination(pos);
    }
}
