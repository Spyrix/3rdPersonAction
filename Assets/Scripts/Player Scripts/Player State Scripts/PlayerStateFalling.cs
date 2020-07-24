using System;
using UnityEngine;

public class PlayerStateFalling : IPlayerState
{
    Vector2 mi;
    float jumpInput;
    bool superJumpReady;
    PlayerInputScript pi;
    public PlayerStateFalling(PlayerInputScript player)
    {
        pi = player;
        superJumpReady = false;
        Debug.Log("entering falling state");
    }
    public void StateUpdate()
    {
        mi = pi.GetMovementInput();
        jumpInput = pi.GetJumpInput();
        pi.playerScript.GroundMovement(mi);
        if (jumpInput == 0f)
        {
            superJumpReady = true;
        }
    }
    public void HandleInput()
    {
        if (superJumpReady && jumpInput == 1f && pi.GetJumpCount() > 0)
        {
            pi.currentState = new PlayerStateJumping(pi);
        }
        if (pi.IsGrounded())
        {
            pi.currentState = new PlayerStateIdle(pi);
        }
        if (pi.playerScript.GetCanClimb() != null)
        {
            //Ensure that the player is snapping to the right point, correctly getting the y axis
            Vector3 p = pi.playerScript.GetCanClimb().GetComponent<LedgeScript>().CalculateCorrectPointToSnapTo(pi.GetPlayerTransform().position);
            pi.playerScript.SetPlayerPosition(p);
            pi.currentState = new PlayerStateHangingOnLedge(pi, pi.playerScript.GetCanClimb());
        }
        if (pi.GetDashInput() == 1f && pi.GetCanDash())
        {
            pi.currentState = new PlayerStateDash(pi);
        }
    }
    public Color GetColor()
    {
        return Color.black;
    }
}
