using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateAim : IPlayerState
{
    PlayerInputScript pi;
    Vector2 mi;
    float ji;
    float ai;
    public PlayerStateAim(PlayerInputScript player)
    {
        pi = player;
        pi.playerScript.GetCurrentCamera().GetComponent<OverShoulderCameraController>().enabled = true;
        pi.playerScript.GetCurrentCamera().GetComponent<ThirdPersonCameraController>().enabled = false;
        Debug.Log("Entering aim state");
    }
    public void StateUpdate()
    {
        mi = pi.GetMovementInput();
        ji = pi.GetJumpInput();
        ai = pi.GetAimInput();
        pi.playerScript.GroundMovement(mi);
    }
    public void HandleInput()
    {
        if (ai < 1f)
        {
            pi.playerScript.GetCurrentCamera().GetComponent<OverShoulderCameraController>().enabled = false;
            pi.playerScript.GetCurrentCamera().GetComponent<ThirdPersonCameraController>().enabled = true;
            pi.currentState = new PlayerStateIdle(pi);
        }
    }

    public Color GetColor()
    {
        return Color.green;
    }
}