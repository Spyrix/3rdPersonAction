using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerWeapon : MonoBehaviour
{
    public abstract void Fire();
    public abstract void StopFire();
    public abstract void SwapFromWeapon();
    public abstract void SwapToWeapon();
}
