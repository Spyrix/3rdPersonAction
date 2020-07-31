using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem;

public class PlayerInputScript : MonoBehaviour
{

    Vector2 movementInput;
    Vector2 rightStickInput;
    Vector2 runInput;
    float jumpInput;
    float cancelClimbInput;
    float dashInput;
    float aimInput;
    float fireWeaponInput;
    [SerializeField]
    int jumpCountMax;
    int jumpsLeft;
    [SerializeField]
    internal PlayerInputActions inputAction;
    [SerializeField]
    internal PlayerScript playerScript;
    [SerializeField]
    internal int playerNumber = 0;
    Rigidbody playerRB;
    InputUser _user;
    internal IPlayerState currentState;

    [SerializeField]
    internal float dashCooldownTimer;
    internal float dashCooldownMax;
    [SerializeField]
    internal bool canDash;

    private void Awake()
    {
        canDash = true;
        dashCooldownTimer = 0f;
        dashCooldownMax = 1f;

        jumpCountMax = 2;
        jumpsLeft = jumpCountMax;
        //Default player to player 1 (which is 0 in the gamepad order)
        if (playerNumber == -1)
        {
            playerNumber = 0;
        }
        playerScript = GetComponent<PlayerScript>();
        playerRB = GetComponent<Rigidbody>();
        inputAction = new PlayerInputActions();

        //Here we handle multiple users
        //_user = new InputUser();
       // _user = InputUser.PerformPairingWithDevice(Gamepad.all[playerNumber]);
       // _user.AssociateActionsWithUser(inputAction);

        currentState = new PlayerStateIdle(this);
        //Setup input for horizontal movement value press
        inputAction.PlayerControls.Walk.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        //Setup input for horizontal movement release
        inputAction.PlayerControls.Walk.canceled += ctx => movementInput = ctx.ReadValue<Vector2>();

        //Setup input for jump values press
        inputAction.PlayerControls.Jump.performed += ctx => jumpInput = ctx.ReadValue<float>();
        //Setup input for jump value release
        inputAction.PlayerControls.Jump.canceled += ctx => jumpInput = ctx.ReadValue<float>();


        //Setup input for cancel climb values press
        inputAction.PlayerControls.CancelClimb.performed += ctx => cancelClimbInput = ctx.ReadValue<float>();
        //Setup input for cancel climb value release
        inputAction.PlayerControls.CancelClimb.canceled += ctx => cancelClimbInput = ctx.ReadValue<float>();

        //Setup input for dash values press
        inputAction.PlayerControls.Dash.performed += ctx => dashInput = ctx.ReadValue<float>();
        //Setup input for dash value release
        inputAction.PlayerControls.Dash.canceled += ctx => dashInput = ctx.ReadValue<float>();

        //Setup input for aim values press
        inputAction.PlayerControls.Aim.performed += ctx => aimInput = ctx.ReadValue<float>();
        //Setup input for aim value release
        inputAction.PlayerControls.Aim.canceled += ctx => aimInput = ctx.ReadValue<float>();

        //Setup input for fire weapon values press
        inputAction.PlayerControls.Fire.performed += ctx => fireWeaponInput = ctx.ReadValue<float>();
        //Setup input for fire weapon value release
        inputAction.PlayerControls.Fire.canceled += ctx => fireWeaponInput = ctx.ReadValue<float>();


    }

    void Start()
    {

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //The player's actions are controlled through two functions.
        //State update is what actually happens each frame during the state.
        currentState.StateUpdate();
        currentState.HandleInput();

        //handle dash cooldown
        if (canDash == false)
        {
            dashCooldownTimer += Time.deltaTime;
        }
        if (dashCooldownTimer >= dashCooldownMax)
        {
            canDash = true;
        }
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

    /*
     * Get_Input functions are used by the state classes to determine when to switch states.
     */
     //###
    public Vector2 GetMovementInput()
    {
        return movementInput;
    }

    public float GetJumpInput()
    {
        return jumpInput;
    }

    public void SetPlayerNumber(int p)
    {
        playerNumber = p;
        _user = new InputUser();
        if (playerNumber >= Gamepad.all.Count)
        {
            //This means that somehow, there are more players in the game then gamepads
            //Remember to throw an exception
            playerNumber = 0;
        }
        _user = InputUser.PerformPairingWithDevice(Gamepad.all[playerNumber]);
        _user.AssociateActionsWithUser(inputAction);
    }
    //###

    public bool IsGrounded()
    {
        /*Used in idle and walking states to determine if the player can jump again 
         * (if the player is on the ground and has stopped ariel movement).*/
        return Physics.Raycast(transform.position, Vector3.down, 1.2f);
    }

    public void ResetJumpCount()
    {
        jumpsLeft = jumpCountMax;
    }

    public int GetJumpCount()
    {
        return jumpsLeft;
    }

    public void DecrementJumpCount()
    {
        --jumpsLeft;
    }

    public void StartDashCooldown()
    {
        canDash = false;
        dashCooldownTimer = 0f;
    }

    public float GetDashInput()
    {
        return dashInput;
    }

    public float GetAimInput()
    {
        return aimInput;
    }

    public void Death()
    {
        currentState = new PlayerStateDeath(this);
    }

    public Transform GetPlayerTransform()
    {
        return transform;
    }

    public bool GetCanDash()
    {
        return canDash;
    }

    public float GetCancelClimbInput()
    {
        return cancelClimbInput;
    }

    public float GetFireWeaponInput()
    {
        return fireWeaponInput;
    }

    public Vector2 GetRightStickInput()
    {
        return rightStickInput;
    }

    public Rigidbody GetRigidbody()
    {
        return GetComponent<Rigidbody>();
    }

    public RigidbodyConstraints GetFreezeAll()
    {
        return RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
    }

    public RigidbodyConstraints GetFreezeAllRotation()
    {
        return RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

}