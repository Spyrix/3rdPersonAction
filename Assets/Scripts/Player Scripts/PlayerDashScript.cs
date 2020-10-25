using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class PlayerDashScript : MonoBehaviour
{
    [SerializeField]
    internal Rigidbody playerRB;
    [SerializeField]
    LayerMask groundLayer;
    [SerializeField]
    Vector3 calculatedForward;
    [SerializeField]
    float height = 1.4f;
    [SerializeField]
    float dashSpeed;
    [SerializeField]
    float dashTimer = 0f;
    [SerializeField]
    float dashTimerMax;

    internal RaycastHit hitInfo;
    // Start is called before the first frame update
    void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
        //init constants
        dashTimerMax = .25f;
        dashSpeed = 15f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal float Dash(Vector3 startPos, Vector3 endPos, float startTime)
    {
        calculatedForward = PlayerHelperFunctions.CalculateForward(playerRB, height, hitInfo, groundLayer);
        /*float distCovered = (Time.time - startTime) * dashSpeed;
        float fractionOfJourney = distCovered / (Vector3.Distance(startPos,endPos));
        transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);*/
        dashTimer += Time.fixedDeltaTime;
        float fractionOfJourney = dashTimer / dashTimerMax;
        if (dashTimer >= dashTimerMax)
        {
            dashTimer = 0f;
        }
        float forwardMovement = dashSpeed * Time.fixedDeltaTime;
        playerRB.MovePosition(transform.position+(calculatedForward * forwardMovement));
        return fractionOfJourney;
    }
}
