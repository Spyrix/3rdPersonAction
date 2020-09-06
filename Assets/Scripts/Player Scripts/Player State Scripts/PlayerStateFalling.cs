using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class PlayerStateFalling : PlayerState
{
    Vector2 mi;
    float jumpInput;
    bool superJumpReady;
    float fallingVelocity;
    PlayerInputScript pi;
    public PlayerStateFalling(PlayerInputScript player)
    {
        fallingVelocity = -2f;
        pi = player;
        superJumpReady = false;
        Debug.Log("entering falling state");
        Thread thread = new Thread(() => {
            Thread.Sleep(150);
            fallingVelocity = -8f;
        });
        thread.Start();
    }

    public override void StateUpdate()
    {
        mi = pi.GetMovementInput();
        jumpInput = pi.GetJumpInput();
        pi.playerScript.GroundMovement(mi);
        pi.playerScript.GetPlayerRigidbody().velocity = new Vector3(0, fallingVelocity, 0);
        if (jumpInput == 0f)
        {
            superJumpReady = true;
        }
        //Default playerstate behavior to swap weapons in any state
        HandleWeaponSwapInput(pi);
    }
    public override void HandleInput()
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

    private IEnumerator WaitToIncreaseVelocity()
    {
        yield return new WaitForSeconds(.5f);
        fallingVelocity = -8f;
    }
}
