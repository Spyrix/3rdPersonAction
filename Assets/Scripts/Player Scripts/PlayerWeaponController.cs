using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    [SerializeField]
    internal PlayerWeapon currentWeapon;

    //This class is responsible
    void Awake()
    {
    }

    public void FireWeapon()
    {
        Debug.Log("Firing Weapon!");
        if (currentWeapon != null)
        {
            currentWeapon.Fire();
        }
    }

    public void StopFire()
    {
        Debug.Log("Firing Weapon!");
        if (currentWeapon != null)
        {
            currentWeapon.StopFire();
        }
    }

    public void SwapFromWeapon()
    {
        if (currentWeapon != null)
        {
            currentWeapon.SwapFromWeapon();
        }
    }
}
