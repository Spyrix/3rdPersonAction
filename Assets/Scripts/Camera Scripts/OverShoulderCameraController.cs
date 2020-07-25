using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverShoulderCameraController : MonoBehaviour
{
    [SerializeField]
    GameObject shoulderPosition;

    internal PlayerInputActions inputAction;
    private Vector3 velocity = Vector3.zero;
    [SerializeField]
    internal float smoothTime;
    [SerializeField]
    internal float rotateSpeed;
    [SerializeField]
    internal float maxRotationAngle;

    Vector2 rotateInput;
    Vector2 cameraMoveInput;
    void Awake()
    {
        smoothTime = .05f;
        rotateSpeed = 7f;
        maxRotationAngle = 20f;
        inputAction = new PlayerInputActions();
        //Setup input for horizontal movement value press
        inputAction.PlayerControls.RotateCamera.performed += ctx => rotateInput = ctx.ReadValue<Vector2>();
        //Setup input for horizontal movement release
        inputAction.PlayerControls.RotateCamera.canceled += ctx => rotateInput = ctx.ReadValue<Vector2>();
        //Setup input for horizontal movement value press

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
        MoveCamera();
        RotateCameraVertical();
    }

    void MoveCamera()
    {
        transform.position = Vector3.SmoothDamp(transform.position, shoulderPosition.transform.position, ref velocity, smoothTime);
    }

    internal void AttachToPlayer()
    {
        transform.parent = shoulderPosition.transform;
        //ensures that the camera is looking in the direction of the player
        transform.forward = shoulderPosition.transform.forward;
    }

    internal void RotateCameraVertical()
    {
        Quaternion r = transform.rotation;
        Quaternion newRotation = new Quaternion();
        if (rotateInput.y != 0f) {
            float delta = maxRotationAngle * -rotateInput.y;
            newRotation.eulerAngles = new Vector3(delta, r.eulerAngles.y, r.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.fixedDeltaTime * rotateSpeed);
        }
        else
        {
            newRotation.eulerAngles = new Vector3(0, r.eulerAngles.y, r.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.fixedDeltaTime * rotateSpeed);
        }
    }
        internal void FreeCamera()
    {
        transform.parent = null;
    }
}
