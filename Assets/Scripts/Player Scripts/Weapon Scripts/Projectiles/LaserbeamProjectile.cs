using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserbeamProjectile : MonoBehaviour
{
    [SerializeField]
    private GameObject origin;
    private LineRenderer line;
    private BoxCollider col;
    private RaycastHit hit;
    [SerializeField]
    private float maxDistance;

    private float DamagePerSecond;
    void Awake()
    {
        maxDistance = 30f;
        line = GetComponent<LineRenderer>();
        col = GetComponent<BoxCollider>();
        DamagePerSecond = 1f;
    }
    void Update()
    {
        if (Physics.Raycast(origin.transform.position, transform.forward, out hit, maxDistance)) {
            if (hit.collider.gameObject.name != "Laser") {
            float d = Vector3.Distance(origin.transform.position, hit.transform.position);
            line.SetPosition(1, new Vector3(0, 0, d));
            //extend the collider out a litte further to tag the thing we're hitting
            col.size = new Vector3(col.size.x, col.size.y, d + 1f);
            col.center = new Vector3(col.center.x, col.center.y, d + 1f / 2);
            }
        }
        else
        {
            line.SetPosition(1, new Vector3(0, 0, maxDistance));
            //extend the collider out a litte further to tag the thing we're hitting
            col.size = new Vector3(col.size.x, col.size.y, maxDistance + 1f);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        
    }
}
