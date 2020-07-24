using System;
using UnityEngine;

public class PlayerStateIdle : IPlayerState
{
    /*
     * The player starts in idle, and does not move.
     * */
    PlayerInputScript pi;
    Vector2 mi;
    float ai;
    float ji;
    /*The constructor takes in the player input script because it needs 
    * to be able to communicate with the player and accept data about the
    * player, like input etc*/
    public PlayerStateIdle(PlayerInputScript player)
    {
        pi = player;
        pi.ResetJumpCount();
        Debug.Log("Entering idle state");
    }
    //
    /*If the player is in this state, they will always do whatever is in state update.
     * Which includes accepting new input and */
    public void StateUpdate()
    {
        pi.playerScript.Idle();
        mi = pi.GetMovementInput();
        ji = pi.GetJumpInput();
        ai = pi.GetAimInput();
    }
    public void HandleInput()
    {
        if (mi.magnitude > 0f)
        {
            pi.currentState = new PlayerStateWalking(pi);
        }
        if (ji == 1f && pi.IsGrounded())
        {
            pi.currentState = new PlayerStateJumping(pi);
        }
        if (!pi.IsGrounded())
        {
            pi.currentState = new PlayerStateFalling(pi);
        }
        if (pi.GetDashInput() == 1f && pi.GetCanDash())
        {
            pi.currentState = new PlayerStateDash(pi);
        }
        if (ai == 1f)
        {
            pi.currentState = new PlayerStateAim(pi);
        }
        // if (ji == 1f && !pi.IsGrounded())
        //    pi.currentState = new PlayerStateSuperJump(pi);
    }
    //Debug gizmo
    public Color GetColor()
    {
        return Color.red;
    }
}