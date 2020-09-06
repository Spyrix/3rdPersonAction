using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    // Start is called before the first frame update
    private float prevWeaponInput;
    private float nextWeaponInput;
    private bool canSwapNext;
    private bool canSwapPrev;

    public void HandleWeaponSwapInput(PlayerInputScript pi)
    {
        nextWeaponInput = pi.GetNextWeaponInput();
        prevWeaponInput = pi.GetPrevWeaponInput();
        if (nextWeaponInput == 0f)
        {
            canSwapNext = true;
        }
        if (prevWeaponInput == 0f)
        {
            canSwapPrev = true;
        }
        if (nextWeaponInput > 0f && canSwapNext)
        {
            canSwapNext = false;
            Debug.Log("Calling playerscript to ask the weapon script to swap to the next!");
        }
        else if (prevWeaponInput > 0f && canSwapPrev)
        {
            canSwapPrev = false;
            Debug.Log("Calling playerscript to ask the weapon script to swap to the prev!");
        }
    }

    public abstract void StateUpdate();
    public abstract void HandleInput();
}
