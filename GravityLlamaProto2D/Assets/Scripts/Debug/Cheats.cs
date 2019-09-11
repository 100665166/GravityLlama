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
 * GravityLevel.cs
 * 
 * 
 * Changelog:
 * 31-08    Initial
 * 11-09    Fixed orientation problem with spawned pickups
 * 12-09    Can now change gravity on the fly without having to press UI buttons
 * 
 * =============================================================================
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    // ********************************************************************************************************

    [Tooltip("Activate cheats for testing. The commands are as follows:\nLEFTSHIFT + E: Spawn Gravity+ Pickup\nLEFTSHIFT + R: Spawn Gravity- Pickup")]
    public bool enableCheats = true;    // Deactivate for public beta/release

    private Player player;   // Need to get player's position so that things can spawn properly
    private GameObject gm;   // Don't forget the GameManager

    // ********************************************************************************************************

    void Start()
    {
        player = GetComponent<Player>();

        try
        {
            gm = GameObject.FindGameObjectWithTag("EditorOnly");
        }
        catch (NullReferenceException)
        {
            Debug.Log("[CHEATS.CS] No GameManager detected within the scene. Please add the prefab to the scene or create one and add GravityLevel.cs to it.");
        }
    }

    // ********************************************************************************************************

    void Update()
    {
        // No cheats for you unless this is attached to a player first
        if (player != null)
        {
            // No cheats for you either if the flag isn't toggled
            if (enableCheats)
            {
                // Increase gravity
                if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Z))
                {
                    RaiseGravity();
                }

                // Decrease gravity
                if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.X))
                {
                    LowerGravity();
                }

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

    // RaiseGravity
    // Instantly raise gravityLevel by one
    // Takes: Nothing
    // Returns: Nothing
    public void RaiseGravity()
    {
        Debug.Log("Level of gravity raised by +1.");
        gm.GetComponent<AdjustGravity>().IncreaseGravity();
    }

    // ********************************************************************************************************

    // LowerGravity
    // Instantly lower gravityLevel by one
    // Takes: Nothing
    // Returns: Nothing
    public void LowerGravity()
    {
        Debug.Log("Level of gravity lowered by -1.");
        gm.GetComponent<AdjustGravity>().DecreaseGravity();
    }

    // ********************************************************************************************************

    // SpawnPositive
    // Creates a positive gravity pickup in front of the llama
    // Takes: Nothing
    // Returns: Nothing
    public void SpawnPositive()
    {
        Debug.Log("Spawned a Gravity++ pickup.");
        Instantiate(Resources.Load("Pickups/AddsGravity"), player.transform.position + (transform.right * 8) + (player.transform.up * 2), player.transform.rotation);
    }

    // ********************************************************************************************************

    // SpawnNegative
    // Creates a negative gravity pickup in front of the llama
    // Takes: Nothing
    // Returns: Nothing
    public void SpawnNegative()
    {
        Debug.Log("Spawned a Gravity-- pickup.");
        Instantiate(Resources.Load("Pickups/LowersGravity"), player.transform.position + (transform.right * 8) + (player.transform.up * 2), player.transform.rotation);
    }

    // ********************************************************************************************************
}
