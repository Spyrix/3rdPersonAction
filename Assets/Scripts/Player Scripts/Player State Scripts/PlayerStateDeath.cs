using System;
using UnityEngine;

public class PlayerStateDeath : IPlayerState
{
    PlayerInputScript pi;
    public PlayerStateDeath(PlayerInputScript player)
    {
        pi = player;
    }
    public void StateUpdate()
    {

    }
    public void HandleInput()
    {
    }
    public Color GetColor()
    {
        return Color.magenta;
    }
}