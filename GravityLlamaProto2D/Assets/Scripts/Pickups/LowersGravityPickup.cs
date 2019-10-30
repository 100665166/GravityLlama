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
using UnityEngine.Audio;

public class LowersGravityPickup : PickupBase
{
    protected override void Reset()
    {
    }

    protected override void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            Debug.Log("pickup audio" + (1 - (Math.Abs(gm.GetComponent<AudioManager>().volume) / 100)) * .4f);
            AudioSource.PlayClipAtPoint(gm.GetComponent<AudioManager>().negativePickupSound, c.transform.position, (1-(Math.Abs(gm.GetComponent<AudioManager>().volume) / 100)) * .1f);
            //gm.GetComponent<AdjustGravity>().DecreaseGravity();
            gm.GetComponent<ScoringSystem>().currentScore++;
            Destroy(gameObject);
        }
    }
}
