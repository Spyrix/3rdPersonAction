using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class LedgeScript : MonoBehaviour
{
    [SerializeField]
    internal GameObject UpLedge;
    [SerializeField]
    internal GameObject DownLedge;
    [SerializeField]
    internal GameObject LeftLedge;
    [SerializeField]
    internal GameObject RightLedge;

    [SerializeField]
    GameObject leftMostPoint;
    [SerializeField]
    GameObject rightMostPoint;
    [SerializeField]
    float playerClimbLedgeHeight;

    internal RaycastHit hitInfo;

    private void Awake()
    {
        playerClimbLedgeHeight = 2f;
    }

    private void Update()
    {
        //DrawDebugRays();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(leftMostPoint.transform.position, .3f);
        Gizmos.DrawSphere(rightMostPoint.transform.position, .3f);
    }

    public void DisableColliderTemporarily()
    {
        GetComponent<Collider>().enabled = false;
        StartCoroutine(EnableColliderAfterSeconds());
    }

    IEnumerator EnableColliderAfterSeconds()
    {
        yield return new WaitForSeconds(.5f);
        GetComponent<Collider>().enabled = true;
    }

    public Vector3 GetLeftMostPoint()
    {
        return leftMostPoint.transform.position;
    }

    public Vector3 GetRightMostPoint()
    {
        return rightMostPoint.transform.position;
    }


    public GameObject GetUpLedge()
    {
        return UpLedge;
    }
    public GameObject GetLeftLedge()
    {
        return LeftLedge;
    }
    public GameObject GetRightLedge()
    {
        return RightLedge;
    }
    public GameObject GetDownLedge()
    {
        return DownLedge;
    }

    public Vector3 CalculateCorrectPointToSnapTo(Vector3 playerPos)
    {
        //This function is invoked when the player collides with a ledge, and calculates the correct point to snap the player to along the ledge, between the two end points.
        //This is to prevent goofy behavior that occurs when lineraly interpolating between an initial incorrect snap point.
        //We're going to build the formula for the line between the two ledge points, plug the player X, the leftmost point z Then, rotate it, by the ledge's y axis rotation. Then calculate y.
        //establish point
        //debugP1 = point;
        Vector3 leftPos = leftMostPoint.transform.position;
        Vector3 rightPos = rightMostPoint.transform.position;
        Debug.Log("leftPos" + leftPos.x + "rightpos" + rightPos.x);
        Vector3 point = new Vector3(0,0,0);
        if (!Mathf.Approximately(leftPos.x, rightPos.x))
        {
            Vector3 direction = rightPos - leftPos;
            //Solve for t
            //playerPos.x = leftPos.x + direction.x*t
            //z(t) = leftpos.z + direction.z*t
            float t = (playerPos.x - leftPos.x) / direction.x;
            //Once we have t, plug it in to find y
            //y(t) = leftpos.y + direction.y * t
            float y = leftPos.y + (direction.y * t);
            point = new Vector3(playerPos.x, y, playerPos.z);
        }
        else
        {
            //This is probably a vertical line that we're climbing, so we need to set something else instead, player x
            point = new Vector3(leftPos.x, playerPos.y, leftPos.z);
        }
        return point;
    }

    public Quaternion GetRotation()
    {
        return transform.rotation;
    }

    public bool CanPlayerClimbOverLedge(GameObject player)
    {

        bool climbOver = false;
        Vector3 abovePlayerPoint = new Vector3(player.transform.position.x, player.transform.position.y + playerClimbLedgeHeight, player.transform.position.z);
        //Just raytrace down and see if you collide with the player. If so, then yes, the player can reach the exit point
        if (Physics.Raycast(abovePlayerPoint, player.GetComponent<Rigidbody>().transform.forward, out hitInfo, 2f))
        {
            climbOver = true;
        }
        return climbOver;
    }

    public Vector3 GetDirectionVectorOfLedge()
    {
        Vector3 direction = leftMostPoint.transform.position - rightMostPoint.transform.position;
        direction = direction.normalized;
        return direction;
    }
}
