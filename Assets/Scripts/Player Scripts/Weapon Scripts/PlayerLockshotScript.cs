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
public class PlayerLockshotScript : PlayerWeapon
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
    [SerializeField]
    internal GameObject projectile;
    [SerializeField]
    internal bool canFire;

    RaycastHit hit;
    /*
     * The targetreticle is used as the source of raycasts to potential targets, hovering the cursor over enemies.
     */
    [SerializeField]
    internal GameObject targetReticle;

    [SerializeField]
    internal List<GameObject> rayOrigins;

    internal float triggerPullInput;
    void Awake()
    {
        targets = new List<GameObject>();
        maxTargets = 8;
        maxDistance = 40f;
        cooldownTimeLimit = 5f;
        canFire = true;
    }

    // Update is called once per frame
    void Update()
    {
        HandleCooldown();
        LookForTargets();
    }

    void LookForTargets()
    {
        /*
         * This function shoots out raycasts from the targetting reticle to 
         * 
         */
        if (cooldownTimer == 0f) {
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
                    //Swap it's material to the targeted version
                    
                }
            }
        }
    }

    public override void SwapFromWeapon()
    {
        targets.Clear();
    }

    public override void SwapToWeapon()
    {

    }
    
    public void ClearTargets()
    {
        //This function clears the target list, and turns every target's material in the target list back to normal

    }

    void AddNewTarget(GameObject t)
    {
        if (!targets.Exists(x => x == hit.collider.gameObject)&&targets.Count < maxTargets)
        {
            targets.Add(t);
        }
        //play a sound, pitch that sound up based on how full the targets list is
        //add some sort of marker above the target to 
    }

    public override void Fire()
    {
        /*
         * For each target, spawn a lockshot projectile homing prefab. Then, wait a small amount of time before firing the next one.
         * Currently, works more like a shotgun. Also the projectile goes through stuff because pathfinding is hard
         */
        /* if (cooldownTimer == 0f) {
             for(int i = 0; i < targets.Count; i++)
             {
                 GameObject go = Instantiate(projectile, transform.position + transform.forward, Quaternion.identity);
                 go.GetComponent<LockshotProjectile>().SetTarget(targets[i]);
             }
         }
         cooldownTimer = 0.1f;*/
        if (canFire)
        {
            StartCoroutine(WaitForNextProjectile(0));
            canFire = false;
        }
    }

    public override void StopFire()
    {
        
    }

    private void HandleCooldown()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= cooldownTimeLimit)
            {
                cooldownTimer = 0f;
                canFire = true;
            }
        }
    }

    IEnumerator WaitForNextProjectile(int i)
    {
        if (i < targets.Count)
        {
            Debug.Log("Firing projectile #:" + i);
            GameObject go = Instantiate(projectile, transform.position + transform.forward, Quaternion.identity);
            go.GetComponent<LockshotProjectile>().SetTarget(targets[i]);
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(WaitForNextProjectile(i + 1));
        }
        else
        {
            cooldownTimer = 0.1f;
            targets.Clear();
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
