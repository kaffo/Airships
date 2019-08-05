using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class GattlingGun : MonoBehaviour
{
    [Header("References")]
    public SteamVR_Action_Boolean gunActivateAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("ToolActivate");
    public GameObject gattlingBulletPrefab;
    public GameObject bulletManager;
    public Transform bulletSpawnPoint;

    [Header("Setup")]
    public float fireRate = 0.5f;

    private float timePassed = 0f;

    private void Start()
    {
        if (gattlingBulletPrefab == null || bulletManager == null || bulletSpawnPoint == null)
        {
            Debug.LogError(this.name + " on " + this.gameObject + " has not been setup correctly!");
            this.enabled = false;
            return;
        }
    }

    protected virtual void HandAttachedUpdate(Hand hand)
    {
        if (gunActivateAction.GetState(hand.handType) && timePassed >= fireRate)
        {
            Instantiate(gattlingBulletPrefab, bulletSpawnPoint.position, transform.rotation, bulletManager.transform);
            timePassed = 0f;
        } else if (timePassed < fireRate)
        {
            timePassed += Time.deltaTime;
        }
    }
}
