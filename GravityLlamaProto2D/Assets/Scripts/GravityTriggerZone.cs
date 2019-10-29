/*
 * =============================================================================
 * 
 * [Gravity Llama]
 * Alpha
 * 
 * 
 * Script name:
 * GravityTriggerZone.cs
 * 
 * 
 * Date:
 * 29-10-2019
 * 
 * 
 * Description:
 * Pulls the llama to the ground to prevent them from "flying"
 * 
 * 
 * Parameters:
 * None
 * 
 * 
 * Attaches to:
 * Any GameObject with a Collider trigger
 * 
 * 
 * Dependencies:
 * None
 * 
 * 
 * Changelog:
 * 29-10    Initial
 * 
 * =============================================================================
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTriggerZone : MonoBehaviour
{
    [Tooltip("Will the player's weight increase constantly beyond at this zone? (to compensate for steep inclines)")]
    public bool heavyZone = false;

    [Tooltip("Does this zone make the player instantly fall to the ground? (use this to make steep transitions less jarring)")]
    public bool instantDrop = false;

    private Player player;

    // ********************************************************************************************************

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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

    void OnTriggerEnter(Collider col)
    {
        // Are we the player?
        if (col.CompareTag("Player"))
        {
            // Make them fall to the ground from this point onward since there's a depression in the terrain
            if (heavyZone)
            {
                //Debug.Log("Now dragging...");
                player.SetDragState = true;
            }

            // Same as above but only for a single trigger zone
            if (instantDrop)
            {
                //Debug.Log("Dropping the player instantly...");
                player.DropInstantly();
            }
        }
    }
}
