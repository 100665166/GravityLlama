/*
 * =============================================================================
 * 
 * [Gravity Llama]
 * Alpha
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
 * Handles all player properties i.e. animations, etc.
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
 * PlayerMovement.cs
 * 
 * 
 * Changelog:
 * 21-08    Initial placeholder script
 * 31-08    Added OnCollision events for checking llama stability
 * 08-09    Removed some redundant functionality
 * 
 * =============================================================================
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public bool isGrounded = false; // For checking whether the llama can jump again or not; note that this has no effect unless disableChainJumps in Movement.cs is enabled

    // For detecting whether the player is grounded (needed for Movement.cs jumping)
    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "TerrainWall")
        {
            Debug.Log("Llama is on the ground.");
            isGrounded = true;
        }
    }

    // Ditto, but for checking whether the llama's airborne or not
    void OnCollisionExit(Collision c)
    {
        if (c.gameObject.tag == "TerrainWall")
        {
            Debug.Log("Llama is in the air.");
            isGrounded = false;

            // Resets llama's original velocities
            if (GetComponent<PlayerMovement>().isChangingLane)
            {
                GetComponent<PlayerMovement>().ResetVelocity();
            }
        }
    }
}
