﻿/*
 * =============================================================================
 * 
 * [Gravity Llama]
 * Alpha
 * 
 * 
 * Script name:
 * GoalEndLevel.cs
 * 
 * 
 * Date:
 * 07-10-2019
 * 
 * 
 * Description:
 * Used for end-of-level goal triggers
 * 
 * 
 * Parameters:
 * None
 * 
 * 
 * Attaches to:
 * Goal GameObject in level prefab
 * 
 * 
 * Dependencies:
 * TerrainMover.cs
 * 
 * 
 * Changelog:
 * 07-10    Initial
 * 
 * =============================================================================
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalEndLevel : MonoBehaviour
{
    private GameObject gm;

    // ********************************************************************************************************

    void Start()
    {
        try
        {
            gm = GameObject.FindGameObjectWithTag("EditorOnly");
        }
        catch (NullReferenceException)
        {
            Debug.Log("[GOALENDLEVEL.CS] No GameManager detected within the scene. Please add the prefab to the scene or create one and add GravityLevel.cs to it.");
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

    void OnTriggerEnter(Collider col)
    {
        // Are we the player?
        if (col.CompareTag("Player"))
        {
            // If yes, then confirm that we've reached the end of the level
            gm.GetComponent<TerrainMover>().HasFinishedLevel = true;
        }
    }
}
