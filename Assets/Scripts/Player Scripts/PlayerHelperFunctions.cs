using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerHelperFunctions
{
    public static Vector3 CalculateForward(Rigidbody playerRB, float height, RaycastHit hitInfo, LayerMask groundLayer)
    {
        Vector3 cf;
        if (Physics.Raycast(new Vector3(playerRB.transform.position.x, playerRB.transform.position.y - height / 1.5f, playerRB.transform.position.z), -playerRB.transform.up, out hitInfo, 1f, groundLayer))
        {
            cf = Vector3.Cross(playerRB.transform.right, hitInfo.normal);
        }
        else
        {
            cf = playerRB.transform.forward;
        }
        return cf;
    }
}
