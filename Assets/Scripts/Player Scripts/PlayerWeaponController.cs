using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    IPlayerWeapon currentWeapon;
    //This class is responsible
    void Awake()
    {
        currentWeapon = null;
    }

    public void FireWeapon()
    {
        Debug.Log("Firing Weapon!");
        if (currentWeapon != null)
        {
            currentWeapon.Fire();
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
