using System;
using UnityEngine;

public class PlayerStateDash : PlayerState
{
    PlayerInputScript pi;
    float dashLength = 3f;
    Vector3 startPosition;
    Vector3 endPosition;
    float startTime;
    float dashProgress;

    public PlayerStateDash(PlayerInputScript player)
    {
        pi = player;
        startPosition = pi.GetPlayerTransform().position;
        endPosition = startPosition + pi.GetPlayerTransform().forward * dashLength;
        startTime = Time.time;
        pi.gameObject.GetComponent<Rigidbody>().useGravity = false;
    }
    public override void StateUpdate()
    {
        dashProgress = pi.playerScript.Dash(startPosition, endPosition, startTime);
        //Default playerstate behavior to swap weapons in any state
        HandleWeaponSwapInput(pi);
    }
    public override void HandleInput()
    {
        if (dashProgress >= 1f)
        {
            pi.currentState = new PlayerStateIdle(pi);
            pi.gameObject.GetComponent<Rigidbody>().useGravity = true;
            pi.StartDashCooldown();
        }
    }
    public Color GetColor()
    {
        return Color.red;
    }
}