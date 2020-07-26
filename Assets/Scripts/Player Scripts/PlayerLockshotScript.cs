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
    [SerializeField]
    internal float cooldownTimer = 0f;
    [SerializeField]
    internal float cooldownTimeLimit;

    RaycastHit hit;
    /*
     * The targetreticle is used as the source of raycasts to potential targets, hovering the cursor over enemies.
     */
    [SerializeField]
    internal GameObject targetReticle;

    [SerializeField]
    internal List<GameObject> rayOrigins;

    internal PlayerInputActions inputAction;
    internal float triggerPullInput;
    void Awake()
    {
        targets = new List<GameObject>();
        maxTargets = 8;
        maxDistance = 40f;
        cooldownTimeLimit = 5f;

        inputAction = new PlayerInputActions();
        //Setup input for jump values press
        inputAction.PlayerControls.Aim.performed += ctx => triggerPullInput = ctx.ReadValue<float>();
        //Setup input for jump value release
        inputAction.PlayerControls.Aim.canceled += ctx => triggerPullInput = ctx.ReadValue<float>();
    }

    // Update is called once per frame
    void Update()
    {
        LookForTargets();
    }

    void LookForTargets()
    {
        /*
         * This function shoots out raycasts from the targetting reticle to 
         * 
         */
        Vector3[] origins = new Vector3[]
        {
            targetReticle.transform.position
        };
        Vector3 rayDirection = Vector3.Cross(targetReticle.transform.forward, targetReticle.transform.right);
        for (int i = 0; i < origins.Length; i++) {
            //If there is a hit
            if (Physics.Raycast(origins[i], rayDirection, out hit, maxDistance))
            {
                Debug.DrawRay(origins[i], rayDirection, Color.green);
                //If the gameobject hit is not already a target
                AddNewTarget(hit.collider.gameObject);
            }
        }
    }

    void ClearTargets()
    {
        targets.Clear();
    }

    void AddNewTarget(GameObject t)
    {
        if (!targets.Exists(x => x == hit.collider.gameObject)&&targets.Count < maxTargets)
        {
            targets.Add(t);
        }
    }

    /*private void OnDrawGizmos()
    {
        Vector3[] origins = new Vector3[]
        {
            targetReticle.transform.position,
        };
        for (int i = 0; i < origins.Length; i++)
        {
            Gizmos.color = new Color(1, 0, 0, 1f);
            Gizmos.DrawCube(origins[i], new Vector3(.05f, .05f, .05f));
        }
    }*/
}
