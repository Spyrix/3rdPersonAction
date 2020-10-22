using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiAutoWeapon : PlayerWeapon
{
    [SerializeField]
    internal float cooldownTimer = 0f;
    [SerializeField]
    internal float cooldownTimeLimit;
    [SerializeField]
    GameObject shotOrigin;
    internal RaycastHit hit;
    [SerializeField]
    float maxDistance;
    private void Awake()
    {
        cooldownTimeLimit = .2f;
        maxDistance = 50f;
    }
    public override void Fire()
    {
        if (Physics.Raycast(shotOrigin.transform.position, shotOrigin.transform.forward, out hit, maxDistance))
        {
            //Draw a line renderer from the origin to the point at which the target was hit
            //create a particle effect of the shot hitting
            //if the object can be damaged, damage it
            //start the cooldown
            //the line renderer and particle effect should fade/disappear after quickly

        }
        else
        {
            //draw a line renderer from the origin to the max distance
            //make it fade quickly/destroy it
        }
    }
    public override void StopFire()
    {
        throw new System.NotImplementedException();
    }
    public override void SwapFromWeapon()
    {
    }

    public override void SwapToWeapon()
    {

    }
}
