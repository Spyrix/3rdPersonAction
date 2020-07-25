using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script dictates the behavior of the lockshot weapon.
* While this weapon is equipped, if the player is aiming and holding down right trigger, 
* the lockshot will start to collect targets as the aiming reticle passes over potential targets.
* The reticle uses raycasting to collect targets, and it does not collect the same target more than once.
* 8 total targets can be collected.
* If a target is maxDistance away from the player, it drops from the total targets.
* Once the right trigger is released, it fires projectiles that home in on all the targets.
*/
public class PlayerLockshotScript : MonoBehaviour
{
    [SerializeField]
    internal List<GameObject> targets;
    [SerializeField]
    internal float maxTargets;
    [SerializeField]
    internal float maxDistance;

    RaycastHit hit;
    /*
     * The targetreticle is used as the source of raycasts to potential targets, hovering the cursor over enemies.
     */
    [SerializeField]
    internal GameObject targetReticle;


    void Awake()
    {
        targets = new List<GameObject>();
        maxTargets = 8;
        maxDistance = 40f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ClearTargets()
    {
        targets = new List<GameObject>();
    }

    void AddNewTarget(GameObject t)
    {
        targets.Add(t);
    }

    void LookForTargets()
    {
        /*
         * This function shoots out raycasts from the targetting reticle to 
         * 
         */
        for (int i = 0; i < 3; i++) {
            Physics.Raycast(targetReticle.transform.position, targetReticle.transform.forward, out hit, maxDistance);

        }
        for (int i = 0; i < 2; i++)
        {
            Physics.Raycast(targetReticle.transform.position, targetReticle.transform.forward, out hit, maxDistance);

        }
    }

    internal void CreateAimingCylinder()
    {
        
    }
}
