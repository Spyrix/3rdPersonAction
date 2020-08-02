using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperFunctions
{
    public static Vector3 ConvertMoveInputToCam(Vector2 movementVector2, Transform camTransform)
    {
        /*
         * This function takes the transform of a camera and the input used to control a player's movement
         * and makes it relative to the way that the camera is facing
        */
        Vector3 camF = camTransform.forward;
        Vector3 camR = camTransform.right;
        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;
        return (camF * movementVector2.y + camR * movementVector2.x);
    }
}
