using System;
using UnityEngine;

public class PlayerStateJumping : PlayerState
{
    Vector2 mi;
    Vector3 startPosition;
    float jumpInput;
    float startTime;
    float jumpProgress = 0f;
    PlayerInputScript pi;
    public PlayerStateJumping(PlayerInputScript player)
    {
        pi = player;
        pi.DecrementJumpCount();
        startPosition = pi.GetPlayerTransform().position;
        startTime = Time.time;
    }
    public override void StateUpdate()
    {
        mi = pi.GetMovementInput();
        jumpInput = pi.GetJumpInput();
        jumpProgress = pi.playerScript.Jump(startPosition, startTime, mi);
    }
    public override void HandleInput()
    {
        if (jumpInput == 0f || jumpProgress > 1f)
        {
            pi.currentState = new PlayerStateFalling(pi);
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