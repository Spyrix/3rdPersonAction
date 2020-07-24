using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionScript : MonoBehaviour
{
    [SerializeField]
    internal PlayerScript playerScript;

    [SerializeField]
    internal bool collisionActive;
    //This variable is set to true if we are colliding against something that is climbable

    // Start is called before the first frame update
    void Start()
    {
        playerScript = GetComponent<PlayerScript>();
        collisionActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void OnCollisionEnter(Collision collision)
    {
        collisionActive = true;
    }

    internal void OnCollisionExit(Collision collision)
    {
        collisionActive = false;
    }

    public bool GetCollisionActive()
    {
        return collisionActive;
    }

}
