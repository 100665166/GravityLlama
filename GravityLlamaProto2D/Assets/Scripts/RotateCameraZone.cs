﻿/*
 * =============================================================================
 * 
 * [Gravity Llama]
 * Alpha
 * 
 * 
 * Script name:
 * RotateCameraZone.cs
 * 
 * 
 * Date:
 * 19-10-2019
 * 
 * 
 * Description:
 * Rotates the player's camera to match incline of the level terrain
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
 * 21-10    Initial
 * 
 * =============================================================================
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraZone : MonoBehaviour
{
    [Tooltip("What angle should the camera rotate to?")]
    public float angle = 0f;

    [Tooltip("How much does the player's jump get boosted? (to accomodate for height slopes)")]
    public float playerNewJumpStrength = 5000f;

    [Tooltip("Will the player's weight increase dramatically at this zone? (to compensate for steep inclines)")]
    public bool heavyZone = false;

    // For the MainCamera
    private GameObject cam;

    // ********************************************************************************************************

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // ********************************************************************************************************

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position, 25f);
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
            //cam.transform.Rotate(new Vector3(angle, 0, 0));
            cam.transform.Rotate(new Vector3(angle, 0, 0));
            col.GetComponent<Player>().SetJumpStrength = playerNewJumpStrength;

            // Force us back to the ground if this trigger is near a drop
            if (heavyZone)
            {
                col.GetComponent<Player>().CanPull = true;
            }
        }
    }
}
