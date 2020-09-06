using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    [SerializeField]
    internal GameObject reticle;

    [SerializeField]
    internal GameObject player;
    [SerializeField]
    internal PlayerInputActions inputAction;
    [SerializeField]
    internal Vector2 cameraMoveInput;
    [SerializeField]
    internal float cameraSpeedX;
    [SerializeField]
    internal float maxDistanceFromPlayer;
    [SerializeField]
    internal float minDistanceFromPlayer;
    [SerializeField]
    internal float smoothTime;

    internal float maxDistanceOffset;//used to give some distance leeway for the camera so it's not just thumping

    private Vector3 velocity = Vector3.zero;

    [SerializeField]
    float minCameraAngleX;
    [SerializeField]
    float maxCameraAngleX;
    [SerializeField]
    float currentCameraAngleX;
    [SerializeField]
    float minCameraY;
    [SerializeField]
    float maxCameraY;
    [SerializeField]
    float currentCameraY;
    [SerializeField]
    float cameraPercentVertical; //Moving the right directional stick up and down changes this between 0 and 1. Affects going between min/max camera y and min/max camera angle x.
    [SerializeField]
    float cameraSpeedY;

    void Awake()
    {
        maxDistanceOffset = .5f;
        smoothTime = .15f;
        cameraSpeedY = .2f;
        cameraPercentVertical = 0f;
        minCameraAngleX = 0;
        maxCameraAngleX = 45;
        cameraSpeedX = 1.5f;
        maxDistanceFromPlayer = 10f;
        minDistanceFromPlayer = 9f;
        inputAction = new PlayerInputActions();
        //Setup input for horizontal movement value press
        inputAction.PlayerControls.MoveCamera.performed += ctx => cameraMoveInput = ctx.ReadValue<Vector2>();
        //Setup input for horizontal movement release
        inputAction.PlayerControls.MoveCamera.canceled += ctx => cameraMoveInput = ctx.ReadValue<Vector2>();
        DisableReticle();
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

    private void Update()
    {
        //Look at Player
        HorizontalCameraInput();
        VerticalCameraInput();

    }

    private void FixedUpdate()
    {
        CameraFollow();
    }


    internal void HorizontalCameraInput()
    {
        if (cameraMoveInput.x > 0.2f)
        {
            transform.RotateAround(player.transform.position, Vector3.up, 5 * cameraSpeedX * Time.fixedDeltaTime * cameraMoveInput.x);
        }
        if (cameraMoveInput.x < -0.2f)
        {
            transform.RotateAround(player.transform.position, Vector3.up, -5 * cameraSpeedX * Time.fixedDeltaTime * -cameraMoveInput.x);
        }
    }

    internal void CameraFollow()
    {
        //Horizontal Follow
        Vector2 cameraPosition2D = new Vector2(transform.position.x, transform.position.z);
        Vector2 playerPosition2D = new Vector2(player.transform.position.x, player.transform.position.z);
        float horizontalDistance = Mathf.Abs(Vector2.Distance(cameraPosition2D, playerPosition2D));
        Vector3 newPosition = new Vector3(0,0,0);
        float distanceDifference = horizontalDistance - maxDistanceFromPlayer;
        
        if (horizontalDistance < maxDistanceFromPlayer+maxDistanceOffset || horizontalDistance > maxDistanceFromPlayer-maxDistanceOffset)
        {
            newPosition = transform.position + new Vector3(transform.forward.x * distanceDifference, 0, transform.forward.z * distanceDifference);
        }
        //Vertical Follow
        //Adjust the min/max camera y position
        minCameraY = player.transform.position.y;
        maxCameraY = minCameraY + 5f;
        //adjust 
        currentCameraAngleX = maxCameraAngleX * cameraPercentVertical;
        float absCameraY = maxCameraY - minCameraY;
        currentCameraY = (absCameraY * cameraPercentVertical) + minCameraY;

        //lerp to new camera position
        newPosition = new Vector3(newPosition.x, currentCameraY, newPosition.z);
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
        //rotation
        var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
    }

    internal void VerticalCameraInput()
    {
        if (cameraMoveInput.y > 0.2f && cameraPercentVertical < 1f)
        {
            cameraPercentVertical += cameraSpeedY * Time.fixedDeltaTime * cameraMoveInput.y;
        }
        if (cameraMoveInput.y < -0.2f && cameraPercentVertical > 0f)
        {
            cameraPercentVertical -= cameraSpeedY * Time.fixedDeltaTime * -cameraMoveInput.y;
        }

    }

    internal void EnableReticle()
    {
        reticle.SetActive(true);
    }

    internal void DisableReticle()
    {
        reticle.SetActive(false);
    }

    /*internal void HorizontalCameraFollow()
    {
    Vector2 cameraPosition2D = new Vector2(transform.position.x, transform.position.z);
    Vector2 playerPosition2D = new Vector2(player.transform.position.x, player.transform.position.z);
    //The purpose of this function is to move the camera so that it's always with a certain distance of a player
    //This is 
    if (Mathf.Abs(Vector3.Distance(player.transform.position, transform.position)) > maxDistanceFromPlayer)
    {
    //Vector3 newPosition = transform.position + new Vector3(transform.forward.x, 0, transform.forward.z);
    //transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    Vector3 newPosition = playerPosition2D + (cameraPosition2D - playerPosition2D).normalized * maxDistanceFromPlayer;
    transform.position = newPosition;
    }
    if (Mathf.Abs(Vector2.Distance(cameraPosition2D, playerPosition2D)) < minDistanceFromPlayer)
    {
    //Vector3 newPosition = transform.position + new Vector3(-transform.forward.x, 0, -transform.forward.z);
    //transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    Vector3 newPosition = playerPosition2D + (cameraPosition2D - playerPosition2D).normalized * maxDistanceFromPlayer;
    transform.position = newPosition;
    }
    }*/

    /*internal void VerticalCameraFollow()
    {
        //Adjust the min/max camera y position
        minCameraY =  player.transform.position.y;
        maxCameraY = minCameraY + 5f;
        //adjust 
        currentCameraAngleX = maxCameraAngleX * cameraPercentVertical;
        float absCameraY = maxCameraY - minCameraY;
        currentCameraY = (absCameraY * cameraPercentVertical) + minCameraY;

        //lerp to new camera position
        Vector3 newPosition = new Vector3(transform.position.x, currentCameraY, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
        //rotation
        Quaternion newRotation = Quaternion.identity;
        newRotation.eulerAngles = new Vector3(currentCameraAngleX,newRotation.eulerAngles.y,newRotation.eulerAngles.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.fixedDeltaTime * 0.1f);
    }*/

}