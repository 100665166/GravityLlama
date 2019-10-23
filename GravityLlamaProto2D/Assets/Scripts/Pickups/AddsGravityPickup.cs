/*
 * =============================================================================
 * 
 * [Gravity Llama]
 * Alpha
 * 
 * 
 * Script name:
 * AddsGravityPickup.cs
 * 
 * 
 * Date:
 * 08-09-2019
 * 
 * 
 * Description:
 * Designed for drag and drop onto pickups that raise gravity
 * 
 * 
 * Parameters:
 * None
 * 
 * 
 * Attaches to:
 * Pickups intended to raise the amount of gravity in the scene
 * 
 * 
 * Dependencies:
 * GravityLevel.cs
 * 
 * 
 * Changelog:
 * 08-09    Initial
 * 08-10    Added sound effect parameter
 * 19-10    Fixed SFX being too quiet
 * 21-10    Disabled gravity changes upon pickup
 * 
 * =============================================================================
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddsGravityPickup : PickupBase
{
    protected override void Reset()
    {
    }

    protected override void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(gm.GetComponent<AudioManager>().positivePickupSound, c.transform.position, 1f);
            //gm.GetComponent<AdjustGravity>().IncreaseGravity();
            gm.GetComponent<ScoringSystem>().currentScore += 1000;
            Destroy(gameObject);
        }
    }
}
