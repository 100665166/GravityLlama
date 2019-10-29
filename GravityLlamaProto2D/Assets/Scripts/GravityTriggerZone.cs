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
 * Enables options for pulling the llama to the ground to prevent them from "flying"
 * As well as changing gravity upon entering/leaving zone
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
 * Player.cs
 * TranslateMove.cs
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

    [Tooltip("Is this zone going to affect the player's inertia?\n\nNote that the entire collider is used to detect whether the player is inside the low gravity area, so make sure your collider box extends the entire length (don't forget to make it isTrigger too!)")]
    public bool lowGravityZone = false;

    private Player player;
    private GameObject gm;

    // ********************************************************************************************************

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gm = GameObject.FindGameObjectWithTag("GameManager");
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

            // Ditto again, but for low gravity instead
            if (lowGravityZone)
            {
                //Debug.Log("Entering LG zone...");
                player.SetLowGravityState = true;
                gm.GetComponent<GravityLevel>().SetGravityLevel = 1;
                gm.GetComponent<TranslateMove>().sideSpeed = gm.GetComponent<TranslateMove>().sideSpeed * 2;
            }
        }
    }

    // Fire only once we leave a gravity zone
    void OnTriggerLeave(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            if (lowGravityZone)
            {
                //Debug.Log("Leaving LG zone...");
                player.SetLowGravityState = false;
                lowGravityZone = false;
                // TODO: This shouldn't be hardcoded...
                gm.GetComponent<GravityLevel>().SetGravityLevel = 5;
                gm.GetComponent<TranslateMove>().sideSpeed = 8f;
            }
        }
    }
}
