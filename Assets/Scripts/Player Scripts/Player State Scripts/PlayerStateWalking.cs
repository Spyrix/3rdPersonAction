using System;
using UnityEngine;

public class PlayerStateWalking : IPlayerState
{
    /*
     * Purpose: To direct the player character in the direction that the player moves the left analog stick.
     * Step by step:
     * 
     */
    PlayerInputScript pi;
    Vector2 mi;
    float ai;
    float ji;
    public PlayerStateWalking(PlayerInputScript player)
    {
        pi = player;
        Debug.Log("Entering walking state");
    }
    public void StateUpdate()
    {
        ai = pi.GetAimInput();
        mi = pi.GetMovementInput();
        ji = pi.GetJumpInput();
        pi.playerScript.GroundMovement(mi);
    }
    public void HandleInput()
    {
        if (mi.magnitude == 0f)
        {
            pi.currentState = new PlayerStateIdle(pi);
        }
        //check to see if a spell input was pressed
        if (ji == 1f && pi.IsGrounded())
        {
            pi.currentState = new PlayerStateJumping(pi);
        }
        if (pi.GetDashInput() == 1f && pi.GetCanDash())
        {
            pi.currentState = new PlayerStateDash(pi);
        }
        if (!pi.IsGrounded())
        {
            pi.currentState = new PlayerStateFalling(pi);
        }
        if (ai == 1f)
        {
            pi.currentState = new PlayerStateAim(pi);
        }
        // if (ji == 1f && !pi.IsGrounded())
        //    pi.currentState = new PlayerStateSuperJump(pi);
    }
    public Color GetColor()
    {
        return Color.blue;
    }
}