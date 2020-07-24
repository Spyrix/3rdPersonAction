using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbCollisionDetector : MonoBehaviour
{
    //This variable is set to true if we are colliding against something that is climbable
    [SerializeField]
    internal bool canClimb;
    [SerializeField]
    internal GameObject contactLedge;
    // Start is called before the first frame update
    void Awake()
    {
        canClimb = false;
    }

    internal void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Climbable")
        {
            canClimb = true;
            contactLedge = collider.gameObject;
        }
    }

    internal void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Climbable")
        {
            canClimb = false;
            contactLedge = null;
        }
    }

    public void SetCanClimb(bool b)
    {
        canClimb = b;
    }

    public bool GetCanClimb()
    {
        return canClimb;
    }

    public GameObject GetTouchingLedge()
    {
        return contactLedge;
    }
}
