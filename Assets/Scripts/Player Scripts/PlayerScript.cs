using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Written by Will Frazee
 * This is the manager script for controlling everything that the player can do.
 * If the input script wants to talk to the movement script, it must go through this script, for example.
 * This is to segregate code and make sure that we know exactly what is responsible for doing what.
 * 
 * In theory, attaching this script to any player character gameobject should make it controllable by the player.
 */
[RequireComponent(typeof(PlayerInputScript))]
[RequireComponent(typeof(PlayerMovementScript))]
[RequireComponent(typeof(PlayerCollisionScript))]
[RequireComponent(typeof(PlayerClimbingScript))]
[RequireComponent(typeof(PlayerWeaponController))]
//[RequireComponent(typeof(PlayerAnimation))]
//[RequireComponent(typeof(PlayerHealthController))]
[RequireComponent(typeof(MeshCollider))]
public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    internal GameObject currentCamera;
    [SerializeField]
    internal PlayerWeaponController weaponScript;
    [SerializeField]
    internal PlayerInputScript inputScript;
    [SerializeField]
    internal PlayerMovementScript movementScript;
    [SerializeField]
    internal PlayerClimbingScript climbingScript;
    [SerializeField]
    internal GameObject ClimbDetector;
    [SerializeField]
    internal PlayerCollisionScript collisionScript;
    [SerializeField]
    internal Transform playerTransform;
    //[SerializeField]
    //internal PlayerAnimation animationScript;
    //[SerializeField]
    //internal PlayerHealthController healthScript;
    // Start is called before the first frame update
    void Start()
    {
        //Initialize things, set necessary variables.
        inputScript = GetComponent<PlayerInputScript>();
        movementScript = GetComponent<PlayerMovementScript>();
        climbingScript = GetComponent<PlayerClimbingScript>();
        collisionScript = GetComponent<PlayerCollisionScript>();
        //animationScript = GetComponent<PlayerAnimation>();
        //healthScript = GetComponent<PlayerHealthController>();
        playerTransform = GetComponent<Transform>();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        GetComponent<MeshCollider>().convex = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public void adjustHealth(float value)
    {
        //healthScript.addHealth(value);
    }

    internal void GroundMovement(Vector2 movementVector)
    {
        movementScript.InstantPlayerRotation(movementVector);
        movementScript.GroundMovement(movementVector);
        //Debug.Log("movementinput float:"+ movementInput);
       //animationScript.EnterWalkingState(movementInput);
    }


    internal void Strafe(Vector2 movementVector, Vector2 rotationVector)
    {
        movementScript.Strafe(movementVector);
        movementScript.SmoothPlayerRotation(rotationVector);
        //Debug.Log("movementinput float:"+ movementInput);
        //animationScript.EnterWalkingState(movementInput);
    }

    internal void Idle()
    {
        /*animationScript.LeaveJumpState();
        animationScript.LeaveWalkingState();
        animationScript.LeaveSpellAimingState();*/
    }

    internal float Jump(Vector3 startPosition, float startTime, Vector2 movementInput)
    {
        return movementScript.Jump(startPosition, startTime, movementInput);
        //animationScript.PlayJump();
    }

    internal void SetCanClimb(bool b)
    {
        ClimbDetector.GetComponent<PlayerClimbCollisionDetector>().SetCanClimb(b);
    }

    internal GameObject GetCanClimb()
    {
        if (ClimbDetector.GetComponent<PlayerClimbCollisionDetector>().GetCanClimb())
        {
            return ClimbDetector.GetComponent<PlayerClimbCollisionDetector>().GetTouchingLedge();
        }
        else
        {
            return null;
        }
    }

    internal void FireCurrentWeapon()
    {
        weaponScript.FireWeapon();
    }

    internal void SwapFromWeapon()
    {
        weaponScript.SwapFromWeapon();
    }

    internal float ClimbAlongLedge(Vector3 startPosition, float startTime, Vector3 endPosition)
    {
        return climbingScript.ClimbAlongLedge(startPosition,startTime,endPosition);
    }

    internal float ClimbToNewLedge()
    {
        return 0f;
    }

    /*This is a debug function for the player state machine
     * The gizmo will change a different color depending on the state
     * internal void OnDrawGizmos()
    {
        float radius = 0.5f;
        Gizmos.color = inputScript.currentState.GetColor();
        Vector3 center = transform.position + new Vector3(0,3,0);
        Gizmos.DrawSphere(center, radius);
    }*/

    internal void Death()
    {
        inputScript.Death();
        //animationScript.PlayDeath();
        StartCoroutine(WaitToDestroy(2f));
    }

    /*public float GetPlayerHealth()
    {
        return healthScript.GetPlayerHealth();
    }*/

    internal float Dash(Vector3 startPos, Vector3 endPos, float startTime)
    {
        return movementScript.Dash(startPos, endPos, startTime);
    }

    public void SetPlayerPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public Transform GetPlayerTransform()
    {
        return playerTransform;
    }

    public bool GetCollisionActive()
    {
        return collisionScript.GetCollisionActive();
    }

    IEnumerator WaitToDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    public GameObject GetCurrentCamera()
    {
        return currentCamera;
    }
}

