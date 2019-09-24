﻿/*
 * =============================================================================
 * 
 * [Gravity Llama]
 * Alpha
 * 
 * 
 * Script name:
 * TerrainMover.cs
 * 
 * 
 * Date:
 * 19-09-2019
 * 
 * 
 * Description:
 * Handles "movement" of the level itself through the player
 * 
 * 
 * Parameters:
 * None
 * 
 * 
 * Attaches to:
 * GameManager
 * 
 * 
 * Dependencies:
 * None
 * 
 * 
 * Changelog:
 * 19-09    Initial
 * 20-09    ShiftTerrain updated to use waypoint system
 * 21-09    Added AdjustTerrain function, deleted unneccessary references
 * 22-09    Temporarily disabled script due to serious issues
 * 24-09    Refactored; now functions properly and cycles through prefab stages
 * 25-09    It is now possible to increase/decrease speed of level cycling
 * 
 * =============================================================================
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMover : MonoBehaviour
{
    public enum LevelSpeed
    {
        VeryLow,    // GL -> 10
        Low,        // 9-8
        Medium,     // 7-6
        High,       // 5-4
        VeryHigh,   // 3-2
        Extreme     // 1
    }

    [Header("STAGE PARTS")]
    [Tooltip("Drag the prefabs of each part of the level into this list.\n\nIt can either be a single prefab or multiple ones; just make sure they a START and END node (dummy GameObject) defined on each 'end' of the prefab.")]
    public List<GameObject> partsToCycle = new List<GameObject>();

    [Header("GOAL")]
    [Tooltip("Drag (or select) the one GameObject on the scene which acts as a 'goal' for finishing the stage.\nIt needs to have a [Collider] component with the [IsTrigger] flag toggled on!")]
    public GameObject goal;

    [Header("OPTIONS")]
    [Tooltip("Select how fast the stage will cycle at the start.\nHigher or lower gravity levels can auto-affect this as well.")]
    [SerializeField]
    private LevelSpeed cyclingSpeed = LevelSpeed.Medium;    // We'll default to Medium since gravityLevel is usually always at 5 anyway
    [Tooltip("Can gravity affect the speed at which parts 'move'?\n\nIf this flag is disabled, the speed at which levels cycle will be fixed for the entirety of this level (specifically).")]
    [SerializeField]
    private bool gravityAffectsSpeed = true; // Does gravity make the level go faster, Y/N?

    private bool endOfLevel = false;    // Have we reached our goal yet?

    private GameObject gm;
    private GameObject player;

    // ********************************************************************************************************

    public LevelSpeed ModifyCyclingSpeed { get => cyclingSpeed; set => cyclingSpeed = value; }
    public bool IsGravityAffectingSpeed { get => gravityAffectsSpeed; set => gravityAffectsSpeed = value; }

    // ********************************************************************************************************

    void Start()
    {
        // ----------------------------------------------------------------------------------
        // DEBUG STUFF
        // Technically we don't need this part but it never hurts to be careful...
        if (partsToCycle.Count == 0)
        {
            Debug.Log("<color=red>You haven't defined any parts of the level. Please add them to this list first!</color>");
        }

        if (goal == null)
        {
            Debug.Log("<color=green>There's no goal zone defined. Please create one first before attempting to test the level!</color>");
        }

        try
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        catch (NullReferenceException)
        {
            Debug.Log("[TERRAINMOVER.CS] Can't find the player!");
        }

        try
        {
            gm = gameObject;
        }
        catch (NullReferenceException)
        {
            Debug.Log("[TERRAINMOVER.CS] This object isn't a GameManager. Please drag the prefab into the scene or create a dummy one and include GravityLevel.cs to it.");
        }
        // END DEBUG STUFF
        // ----------------------------------------------------------------------------------
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

    void Update()
    {
        // Start moving level parts immediately
        //StartCoroutine(CycleTerrain());
    }

    // ********************************************************************************************************

    // DisableTerrain
    // Hides unused portions of the terrain which have gone past the llama
    // Takes: Nothing
    // Returns: Nothing
    public void DisableTerrain()
    {

    }

    // ********************************************************************************************************

    // CycleTerrain
    // Loops to the next part of the terrain (if any)
    // Takes: Nothing
    // Returns: Nothing
    IEnumerator CycleTerrain()
    {
        while (true)
        {

        }
    }

    // ********************************************************************************************************
}
