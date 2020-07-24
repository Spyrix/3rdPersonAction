using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverShoulderCameraController : MonoBehaviour
{
    [SerializeField]
    GameObject shoulderPosition;

    internal PlayerInputActions inputAction;
    private Vector3 velocity = Vector3.zero;
    internal float smoothTime;
    internal float rotateSpeed;
    internal float rotationAngleZ;
    internal float rotationAngleY;
    Vector2 rotateInput;
    Vector2 cameraMoveInput;
    void Awake()
    {
        smoothTime = .10f;
        rotateSpeed = 20f;
        inputAction = new PlayerInputActions();
        //Setup input for horizontal movement value press
        inputAction.PlayerControls.RotateCamera.performed += ctx => rotateInput = ctx.ReadValue<Vector2>();
        //Setup input for horizontal movement release
        inputAction.PlayerControls.RotateCamera.canceled += ctx => rotateInput = ctx.ReadValue<Vector2>();
        //Setup input for horizontal movement value press
        inputAction.PlayerControls.MoveCamera.performed += ctx => cameraMoveInput = ctx.ReadValue<Vector2>();
        //Setup input for horizontal movement release
        inputAction.PlayerControls.MoveCamera.canceled += ctx => cameraMoveInput = ctx.ReadValue<Vector2>();
    }

    //OnEnable and OnDisable are required for the inputAction class to work
    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FollowPlayer();
        RotateCamera();
    }


    void FollowPlayer()
    {
        //This camera is always locked into place
        if (transform.position != shoulderPosition.transform.position)
        {
            transform.position = Vector3.SmoothDamp(transform.position, shoulderPosition.transform.position, ref velocity, smoothTime);
        }
    }

    void RotateCamera()
    {
        rotationAngleY = 90 * rotateInput.y;
        rotationAngleZ = 90 * rotateInput.x;
        Quaternion q = new Quaternion();
        q.eulerAngles = new Vector3(0,rotationAngleY,rotationAngleZ);
        transform.rotation = Quaternion.Lerp(transform.rotation, q, Time.time*rotateSpeed*rotateInput.magnitude);
    }
}
