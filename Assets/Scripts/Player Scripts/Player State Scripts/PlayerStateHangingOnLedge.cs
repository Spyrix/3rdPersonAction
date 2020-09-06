using System;
using UnityEngine;

public class PlayerStateHangingOnLedge : PlayerState
{
    Vector2 mi;
    float cancelClimb;
    PlayerInputScript pi;
    float jumpInput;
    //This variable is used to ensure that the player lets go of the jump button before they can jump off the ledge again
    bool canJump;
    GameObject ledge;
    Vector3 p;
    public PlayerStateHangingOnLedge(PlayerInputScript player, GameObject go)
    {
        pi = player;
        ledge = go;
        //Ensure that the player is rotated facing the ledge
        Quaternion r = ledge.GetComponent<LedgeScript>().GetRotation();
        Transform t = pi.playerScript.GetPlayerTransform();
        t.eulerAngles = new Vector3(t.eulerAngles.x, r.eulerAngles.y - 90, t.eulerAngles.z);
        //Turn off gravity so that the player doesn't fall
        pi.gameObject.GetComponent<Rigidbody>().useGravity = false;
        Debug.Log("Hanging on ledge state");
        //Freeze the player so that they don't rotate or move while hanging on the ledge
        pi.GetRigidbody().constraints = pi.GetFreezeAll();
        //Player cannot jump until they stop pressing the jump input
        canJump = false;
    }
    public override void StateUpdate()
    {
        mi = pi.GetMovementInput();
        jumpInput = pi.GetJumpInput();
        //Only allow the player to jump if they stop pressing the jump input
        if (jumpInput == 0f)
        {
            canJump = true;
        }
        cancelClimb = pi.GetCancelClimbInput();
        //Default playerstate behavior to swap weapons in any state
        HandleWeaponSwapInput(pi);
    }
    public override void HandleInput()
    {
        //first, let's check to see if any ledge that we are 
        //If the player is moving on the horizontal axis, not accidentally
        if (Mathf.Abs(mi.x) >= 0.2f)
        {
            pi.GetRigidbody().constraints = pi.GetFreezeAllRotation();
            pi.currentState = new PlayerStateClimbing(pi, ledge);
        }
        if (cancelClimb > 0)
        {
            //drop the player and make it so that they won't accidentally grab the same ledge
            pi.playerScript.SetCanClimb(false);
            pi.GetRigidbody().constraints = pi.GetFreezeAllRotation();
            pi.gameObject.GetComponent<Rigidbody>().useGravity = true;
            //Disable the ledge so you dont instantly attach to it again
            ledge.GetComponent<LedgeScript>().DisableColliderTemporarily();
            pi.currentState = new PlayerStateFalling(pi);
        }
        if (jumpInput > 0f && canJump)
        {
            //drop the player and make it so that they won't accidentally grab the same ledge
            pi.playerScript.SetCanClimb(false);
            pi.GetRigidbody().constraints = pi.GetFreezeAllRotation();
            pi.gameObject.GetComponent<Rigidbody>().useGravity = true;
            //Disable the ledge so you dont instantly attach to it again
            ledge.GetComponent<LedgeScript>().DisableColliderTemporarily();
            pi.currentState = new PlayerStateJumping(pi);
        }
    }
    public Color GetColor()
    {
        return Color.red;
    }
}
