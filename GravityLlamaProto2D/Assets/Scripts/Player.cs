/*
 * =============================================================================
 * 
 * [Gravity Llama]
 * Proof-of-concept
 * 
 * 
 * Script name:
 * Player.cs
 * 
 * 
 * Date:
 * 21-08-2019
 * 
 * 
 * Description:
 * Handles all player properties i.e. animations, health, etc.
 * Also determines whether the player is airborne or flat on the ground
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
 * GravityLevel.cs
 * Movement.cs
 * 
 * 
 * Changelog:
 * 21-08    Initial placeholder script
 * 31-08    Added OnCollision events for checking llama stability
 * 
 * =============================================================================
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Tooltip("Player's current health.")]
    public float currentHitPoints = 5f;
    [Tooltip("Player's maximum health.")]
    public float maximumHitPoints = 10f;

    [HideInInspector]
    public bool isGrounded = false; // For checking whether the llama can jump again or not; note that this has no effect unless disableChainJumps in Movement.cs is enabled

    // For detecting whether the player is grounded (needed for Movement.cs jumping)
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "TerrainWall")
        {
            Debug.Log("Llama is on the ground.");
            isGrounded = true;
        }
    }

    // Ditto, but for checking whether the llama's airborne or not
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "TerrainWall")
        {
            Debug.Log("Llama is in the air.");
            isGrounded = false;
        }
    }
}
