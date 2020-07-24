using System;
using UnityEngine;

public class PlayerStateMoveToNewLedge : IPlayerState
{
    //This state automatically moves the player to a new ledge
    GameObject ledge;
    PlayerInputScript pi;
    Vector3 startPosition;
    Vector3 endPosition;
    float startTime;
    float climbProgress = 0f;
    public PlayerStateMoveToNewLedge(PlayerInputScript player, GameObject go)
    {
        ledge = go;
        pi = player;
        startPosition = player.GetPlayerTransform().position;
        startTime = Time.time;
    }
    public void StateUpdate()
    {
        //climbProgress = pi.playerScript.ClimbAlongLedge(startPosition, startTime, endPosition);
    }
    public void HandleInput()
    {
        if (climbProgress >= 1f)
        {
            pi.currentState = new PlayerStateHangingOnLedge(pi, ledge);
        }
    }
    public Color GetColor()
    {
        return Color.red;
    }
}