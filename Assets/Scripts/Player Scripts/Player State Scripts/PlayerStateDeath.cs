using System;
using UnityEngine;

public class PlayerStateDeath : PlayerState
{
    PlayerInputScript pi;
    public PlayerStateDeath(PlayerInputScript player)
    {
        pi = player;
    }
    public override void StateUpdate()
    {

    }
    public override void HandleInput()
    {
    }
    public Color GetColor()
    {
        return Color.magenta;
    }
}