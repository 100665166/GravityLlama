/*
 * =============================================================================
 * 
 * [Gravity Llama]
 * Alpha
 * 
 * 
 * Script name:
 * LowersGravityPickup.cs
 * 
 * 
 * Date:
 * 08-09-2019
 * 
 * 
 * Description:
 * Designed for drag and drop onto pickups that lower gravity
 * 
 * 
 * Parameters:
 * None
 * 
 * 
 * Attaches to:
 * Pickups intended to lower the amount of gravity in the scene
 * 
 * 
 * Dependencies:
 * GravityLevel.cs
 * 
 * 
 * Changelog:
 * 08-09    Initial
 * 
 * =============================================================================
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowersGravityPickup : PickupBase
{
    protected override void Reset()
    {
    }

    protected override void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            gm.GetComponent<AdjustGravity>().DecreaseGravity();
            gm.GetComponent<ScoringSystem>().currentScore++;
            Destroy(gameObject);
        }
    }
}
