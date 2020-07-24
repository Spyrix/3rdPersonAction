using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLockshotScript : MonoBehaviour
{
    [SerializeField]
    internal List<GameObject> targets;
    [SerializeField]
    internal float maxTargets;
    /*
     * The targetreticle is used as the source of raycasts to potential targets, hovering the cursor over enemies.
     */
    [SerializeField]
    internal GameObject targetReticle;


    void Awake()
    {
        targets = new List<GameObject>();
        maxTargets = 8;
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
    }
}
