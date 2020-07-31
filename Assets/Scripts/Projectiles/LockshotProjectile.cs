using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockshotProjectile : MonoBehaviour
{
    [SerializeField]
    internal GameObject target;

    internal float raycastOffset = 2.5f;
    internal float distance = 4f;
    // Update is called once per frame
    void Update()
    {
        
    }

    void Move()
    {

    }

    void Pathfinding()
    {
        Vector3[] detectionVectors =
        {
            transform.position - transform.right * raycastOffset,
            transform.position + transform.right * raycastOffset,
            transform.position - transform.up * raycastOffset,
            transform.position + transform.up * raycastOffset,
        };
        RaycastHit hit;
        foreach (Vector3 dir in detectionVectors){
            Debug.DrawRay(dir,transform.forward * distance, Color.cyan);
        }
        if (Physics)
        {

        }
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
