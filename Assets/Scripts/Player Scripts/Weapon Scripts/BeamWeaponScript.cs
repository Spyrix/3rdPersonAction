using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamWeaponScript : PlayerWeapon
{
    [SerializeField]
    public GameObject laserPrefab;
    [SerializeField]
    public GameObject firePoint;

    private float energyLeft;
    private float cooldownTimer;
    private float cooldownTimeLimit;
    private float rechargeSpeed;
    private bool rechargeActive;
    private float energyMax;
    private GameObject spawnedLaser;

    void Awake()
    {
        energyMax = 100f;
        energyLeft = energyMax;
        cooldownTimer = 0f;
        cooldownTimeLimit = 5f;
        rechargeSpeed = 1f;
        rechargeActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleCooldownTimer();
        Recharge();
    }

    override public void Fire()
    {
        if (energyLeft > 0f && cooldownTimer == 0f) {
            spawnedLaser.SetActive(true);
            energyLeft -= Time.deltaTime;
        }
    }

    override public void StopFire()
    {
        if (cooldownTimer == 0f)
        {
            cooldownTimer = .8f * cooldownTimeLimit;
        }
        spawnedLaser.SetActive(false);
    }

    public override void SwapFromWeapon()
    {
        Destroy(spawnedLaser);
    }

    public override void SwapToWeapon()
    {
        spawnedLaser = Instantiate(laserPrefab, firePoint.transform) as GameObject;
        spawnedLaser.SetActive(false);
    }

    private void HandleCooldownTimer()
    {
        //Get it started if it needs to be started
        if (energyLeft <= 0f && cooldownTimer == 0f)
        {
            cooldownTimer = 0.001f;
        }
        //Start counting down
        if (cooldownTimer > 0f)
        {
            cooldownTimer = + Time.deltaTime;
        }
        //If the cooldownTimer is greater than the wait time, reset it and start charging
        if (cooldownTimer >= cooldownTimeLimit)
        {
            cooldownTimer = 0f;
            rechargeActive = true;
        }
    }

    private void Recharge()
    {
        if (rechargeActive) {
            energyLeft += Time.deltaTime * rechargeSpeed;
        }
        if (energyLeft >= energyMax)
        {
            rechargeActive = false;
        }
    }
}
