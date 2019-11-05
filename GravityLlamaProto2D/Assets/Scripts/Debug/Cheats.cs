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
 * 22-09    MoonJumping added
 * 24-09    Supersonic mode added
 * 06-11    Toggling VHS blur effect for camera
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

    [Tooltip("Activate cheats for testing. The commands are as follows (hold LEFTSHIFT & specific key):\n[V]: Levels automatically move at Extreme (gravity 1) speeds\n[C]: Enable moon jumping\n[Z]: Increase gravity levels by +1\n[X]: Lower gravity levels by -1\n[E]: Spawn Gravity+ Pickup in front of player\n[R]: Spawn Gravity- Pickup in front of player")]
    public bool enableCheats = true;    // Deactivate for public beta/release

    private Player player;   // Need to get player's position so that things can spawn properly
    private GameObject gm;   // Don't forget the GameManager
    private VHS mainCamera;  // ...or the camera

    // ********************************************************************************************************

    void Start()
    {
        player = GetComponent<Player>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<VHS>();

        try
        {
            gm = GameObject.FindGameObjectWithTag("GameManager");
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
                // Supersonic mode
                if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.V))
                {
                    SupersonicMode();
                }

                // Moon jumping
                if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.C))
                {
                    MoonJumping();
                }

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

                // Enable VHS effect on camera
                if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.F))
                {
                    ToggleVHSOn();
                }

                // Disable VHS effect on camera
                if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.G))
                {
                    ToggleVHSOff();
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

    // SupersonicMode
    // Sets level transition speed to Extreme (ignores gravityLevel)
    // Takes: Nothing
    // Returns: Nothing
    public void SupersonicMode()
    {
        Debug.Log("Supersonic mode triggered.");
        gm.GetComponent<TerrainMover>().ModifyCyclingSpeed = TerrainMover.LevelSpeed.Extreme;
    }

    // ********************************************************************************************************

    // MoonJumping
    // Multiplies jumpStrength of player by ten times
    // Takes: Nothing
    // Returns: Nothing
    public void MoonJumping()
    {
        Debug.Log("Moon Jumping activated.");
        player.SetJumpStrength = player.GetJumpStrength * 10;
    }

    // ********************************************************************************************************

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
        GameObject pickup = Instantiate(Resources.Load("Pickups/AddsGravity"), player.transform.position + (transform.right * 8) + (player.transform.up * 2), player.transform.rotation) as GameObject;
        pickup.GetComponent<Rigidbody>().AddForce(Vector3.back * 750f);
    }

    // ********************************************************************************************************

    // SpawnNegative
    // Creates a negative gravity pickup in front of the llama
    // Takes: Nothing
    // Returns: Nothing
    public void SpawnNegative()
    {
        Debug.Log("Spawned a Gravity-- pickup.");
        GameObject pickup = Instantiate(Resources.Load("Pickups/LowersGravity"), player.transform.position + (transform.right * 8) + (player.transform.up * 2), player.transform.rotation) as GameObject;
        pickup.GetComponent<Rigidbody>().AddForce(Vector3.back * 750f);
    }

    // ********************************************************************************************************

    // ToggleVHSOn
    // Enable VHS FX on camera
    // Takes: Nothing
    // Returns: Nothing
    public void ToggleVHSOn()
    {
        Debug.Log("VHS blur enabled.");
        mainCamera.enabled = true;
    }

    // ********************************************************************************************************

    // ToggleVHSOff
    // Disable VHS FX on camera
    // Takes: Nothing
    // Returns: Nothing
    public void ToggleVHSOff()
    {
        Debug.Log("VHS blur disabled.");
        mainCamera.enabled = false;
    }

    // ********************************************************************************************************
}
