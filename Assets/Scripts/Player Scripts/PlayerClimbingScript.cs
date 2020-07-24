using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerClimbingScript : MonoBehaviour
{
    //This script needs to be attached to the climbing collider that is apart of the player
    [SerializeField]
    internal PlayerScript ps;
    float climbSpeed = 2f;

    public float ClimbAlongLedge(Vector3 startPosition, float startTime, Vector3 endPosition)
    {
        //if the start position is the same as the end position, we're done
        if (startPosition == endPosition)
        {
            return 1f;
        }
        float distanceToCover = Vector3.Distance(startPosition, endPosition);
        float distCovered = (Time.time - startTime) * climbSpeed;
        float fractionOfJourney = distCovered / distanceToCover;
        transform.position = Vector3.Lerp(startPosition, endPosition, fractionOfJourney);

        //Debug.Log("Start Position:"+startPosition+":: End Position:"+endPosition);
        return fractionOfJourney;
    }
}
