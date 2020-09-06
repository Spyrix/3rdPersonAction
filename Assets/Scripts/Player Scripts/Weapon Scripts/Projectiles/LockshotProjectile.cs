using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockshotProjectile : MonoBehaviour
{
    [SerializeField]
    internal GameObject target;

    internal float raycastOffset = .5f;
    internal float detectionDistance = 2f;
    internal float drawDistance = 2f;
    internal float rotationalDamp = 1f;
    internal float movementSpeed = 15f;
    // Update is called once per frame
    void Update()
    {
        //Pathfinding();
        TurnTowardsTarget();
        Move();
    }

    void Move()
    {
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }

    void Pathfinding()
    {
        //This is difficult... it probably works for avoiding small objects, but not really for avoiding a wall.
        Vector3 pathfindingOffset = Vector3.zero;
        Vector3[] offsetVectors =
        {
            Vector3.right,
            Vector3.left,
            Vector3.up,
            Vector3.down
        };
        Vector3[] detectionVectors =
        {
            //to the left
            transform.position - transform.right * raycastOffset,
            //to the right
            transform.position + transform.right * raycastOffset,
            //to the down
            transform.position - transform.up * raycastOffset,
            //to the up
            transform.position + transform.up * raycastOffset,
        };
        RaycastHit[] hit = new RaycastHit[detectionVectors.Length];

        if (Physics.Raycast(detectionVectors[0], transform.forward, out hit[0], detectionDistance))
        {
            pathfindingOffset += offsetVectors[0];
        }
        else if (Physics.Raycast(detectionVectors[1], transform.forward, out hit[1], detectionDistance))
        {
            pathfindingOffset += offsetVectors[1];
        }

        if (Physics.Raycast(detectionVectors[2], transform.forward, out hit[2], detectionDistance))
        {
            pathfindingOffset += offsetVectors[2];
        }
        else if (Physics.Raycast(detectionVectors[3], transform.forward, out hit[3], detectionDistance))
        {
            pathfindingOffset += offsetVectors[3];
        }

        foreach (Vector3 v in detectionVectors) {
            Debug.DrawRay(v, transform.forward * drawDistance, Color.cyan);
        }
        //Decide how to turn
        if (pathfindingOffset != Vector3.zero)
        {
            
            Debug.Log(pathfindingOffset);
            Debug.Log("Got here!");
            transform.Rotate(pathfindingOffset * 200f * Time.deltaTime);
        }
        else
        {
            TurnTowardsTarget();
        }
    }

    void TurnTowardsTarget()
    {
        Vector3 pos = target.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1);
    }

    public void SetTarget(GameObject t)
    {
        target = t;
    }

    public GameObject GetTarget()
    {
        return target;
    }
}
