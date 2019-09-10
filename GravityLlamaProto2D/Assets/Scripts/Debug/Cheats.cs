/*
 * =============================================================================
 * 
 * [Gravity Llama]
 * Alpha
 * 
 * 
 * Script name:
 * Cheats.cs
 * 
 * 
 * Date:
 * 31-08-2019
 * 
 * 
 * Description:
 * Cheat codes for debug and build testing
 * 
 * 
 * Parameters:
 * None
 * 
 * 
 * Attaches to:
 * Player object
 * 
 * 
 * Dependencies:
 * Player.cs
 * 
 * 
 * Changelog:
 * 31-08    Initial
 * 11-09    Fixed orientation problem with spawned pickups
 * 
 * =============================================================================
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    [Tooltip("Activate cheats for testing. The commands are as follows:\nLEFTSHIFT + E: Spawn Gravity+ Pickup\nLEFTSHIFT + R: Spawn Gravity- Pickup")]
    public bool enableCheats = true;    // Deactivate for public beta/release

    [HideInInspector]
    public GameObject player;   // Need to get player's position so that things can spawn properly

    void Start()
    {
        try
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        catch (NullReferenceException)
        {
            Debug.Log("[CHEATS.CS] You need a player object in the scene before you can activate cheats.");
        }
    }

    void Update()
    {
        // No cheats for you unless there's a player first
        if (player != null)
        {
            // No cheats for you either if the flag isn't toggled
            if (enableCheats)
            {
                // Positive gravity pickup
                if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.E))
                {
                    SpawnPositive();
                }

                // Negative gravity pickup
                if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.R))
                {
                    SpawnNegative();
                }
            }
        }
    }

    // ========================================================================================================
    // ********************************************************************************************************
    // ========================================================================================================
    // ********************************************************************************************************
    // ========================================================================================================
    // ********************************************************************************************************
    // ========================================================================================================
    // ********************************************************************************************************
    // ========================================================================================================
    // ********************************************************************************************************
    // ========================================================================================================

    // SpawnPositive
    // Creates a positive gravity pickup in front of the llama
    // Takes: Nothing
    // Returns: Nothing
    public void SpawnPositive()
    {
        Debug.Log("Spawned a Gravity++ pickup.");
        Instantiate(Resources.Load("Pickups/AddsGravity"), player.transform.position + (transform.right * 8) + (player.transform.up * 2), player.transform.rotation);
    }

    // SpawnNegative
    // Creates a negative gravity pickup in front of the llama
    // Takes: Nothing
    // Returns: Nothing
    public void SpawnNegative()
    {
        Debug.Log("Spawned a Gravity-- pickup.");
        Instantiate(Resources.Load("Pickups/LowersGravity"), player.transform.position + (transform.right * 8) + (player.transform.up * 2), player.transform.rotation);
    }
}
