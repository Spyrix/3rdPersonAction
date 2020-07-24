using System;
using UnityEngine;
/*
 * 
 * State Design Pattern
 * Every state implenents the IPlayerState interface, which 
 * */
public interface IPlayerState
{
    void HandleInput();
    void StateUpdate();
    Color GetColor();
}