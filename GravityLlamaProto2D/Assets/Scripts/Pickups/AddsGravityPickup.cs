﻿/*
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
 * 
 * =============================================================================
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddsGravityPickup : PickupBase
{
    protected override void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            gm.GetComponent<AdjustGravity>().IncreaseGravity();
            gm.GetComponent<ScoringSystem>().currentScore++;
            Destroy(gameObject);
        }
    }
}
