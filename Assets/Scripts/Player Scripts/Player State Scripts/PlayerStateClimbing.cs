using System;
using UnityEngine;

public class PlayerStateClimbing : IPlayerState
{
    //This state occurs after the player has input a direction to climb in.
    //The player will linerally interpolate from one ledge to the next in the network
    PlayerInputScript pi;
    Vector2 mi;
    Vector3 startPosition;
    Vector3 endPosition;
    GameObject ledge;
    float startTime;
    float climbProgress = 0f;
    public PlayerStateClimbing(PlayerInputScript player, GameObject go)
    {
        pi = player;
        startPosition = pi.GetPlayerTransform().position;
        startTime = Time.time;
        ledge = go;

        //If this is the end of the ledge, and there's no ledge to jump onto, then don't climb
        if (startPosition == endPosition)
        {
            pi.currentState = new PlayerStateHangingOnLedge(pi, ledge);
        }

        mi = pi.GetMovementInput();
        if (mi.x >= 0.2f)
        {
            endPosition = ledge.GetComponent<LedgeScript>().GetRightMostPoint();
        }
        if (mi.x <= -0.2f)
        {
            endPosition = ledge.GetComponent<LedgeScript>().GetLeftMostPoint();
        }
        Debug.Log("entering climbing");

    }
    public void StateUpdate()
    {
        mi = pi.GetMovementInput();
        climbProgress = pi.playerScript.ClimbAlongLedge(startPosition, startTime, endPosition);
    }
    public void HandleInput()
    {
        //If there is no more ledge left to climb, or if the player has stopped pressing the controller stick, go into ledge hanging state
        if (climbProgress > 1f || mi.magnitude == 0f)
        {
            pi.currentState = new PlayerStateHangingOnLedge(pi, ledge);
        }
    }
    public Color GetColor()
    {
        return Color.red;
    }
}