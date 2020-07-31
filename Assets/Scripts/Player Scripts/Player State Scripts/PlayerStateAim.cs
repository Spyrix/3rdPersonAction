using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateAim : IPlayerState
{
    PlayerInputScript pi;
    Vector2 mi;
    Vector2 rsi;
    float ji;
    float fi;
    float ai;
    public PlayerStateAim(PlayerInputScript player)
    {
        pi = player;
        pi.playerScript.GetCurrentCamera().GetComponent<OverShoulderCameraController>().enabled = true;
        pi.playerScript.GetCurrentCamera().GetComponent<ThirdPersonCameraController>().enabled = false;
        pi.playerScript.GetCurrentCamera().GetComponent<OverShoulderCameraController>().AttachToPlayer();
        Debug.Log("Entering aim state");
    }
    public void StateUpdate()
    {
        fi = pi.GetFireWeaponInput();
        mi = pi.GetMovementInput();
        rsi = pi.GetRightStickInput();
        ji = pi.GetJumpInput();
        ai = pi.GetAimInput();

        if (fi > 0f)
        {
            pi.playerScript.FireCurrentWeapon();
        }

        pi.playerScript.Strafe(mi,rsi);
    }
    public void HandleInput()
    {
        if (ai < 1f)
        {
            pi.playerScript.GetCurrentCamera().GetComponent<OverShoulderCameraController>().FreeCamera();
            pi.playerScript.GetCurrentCamera().GetComponent<OverShoulderCameraController>().enabled = false;
            pi.playerScript.GetCurrentCamera().GetComponent<ThirdPersonCameraController>().enabled = true;
            pi.playerScript.SwapFromWeapon();
            pi.currentState = new PlayerStateIdle(pi);
        }
    }

    public Color GetColor()
    {
        return Color.green;
    }
}