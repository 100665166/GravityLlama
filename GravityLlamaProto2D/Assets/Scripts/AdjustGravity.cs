/*
 * =============================================================================
 * 
 * [Gravity Llama]
 * Alpha
 * 
 * 
 * Script name:
 * AdjustGravity.cs
 * 
 * 
 * Date:
 * 20-08-2019
 * 
 * 
 * Description:
 * Modifies gravity levels
 * 
 * 
 * Parameters:
 * None
 * 
 * 
 * Attaches to:
 * Any Canvas UI element (buttons, etc.)
 * 
 * 
 * Known issues:
 * - Negative gravity levels can cause affected objects to clip through colliders
 * 
 * 
 * Dependencies:
 * GravityLevel.cs
 * TerrainMover.cs
 * 
 * 
 * Changelog:
 * 20-08    Initial
 * 21-08    Added validators to functions
 * 25-08    Added validators to prevent unintended gravity settings
 * 11-09    Deleted some artefacts from the PoC
 * 24-09    Added functionality related to TerrainMover
 * 06-10    ValidateLevelSpeed moved to Update
 * 
 * =============================================================================
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AdjustGravity : MonoBehaviour
{
    // ********************************************************************************************************

    private GameObject gm;   // For retrieving the current level of gravity in scene
    private TerrainMover tm;    // For checking whether we also affect the level's movement

    // ********************************************************************************************************

    void Start()
    {
        try
        {
            gm = GameObject.FindGameObjectWithTag("EditorOnly");    // For the GameManager object
        }
        catch (NullReferenceException)
        {
            Debug.Log("[ADJUSTGRAVITY.CS] No GameManager detected within the scene. Please add the prefab to the scene or create one and add GravityLevel.cs to it.");
        }

        // We shouldn't need to worry about try/catch here since TerrainMover won't (or rather shouldn't) be attached to anything else...
        tm = GetComponent<TerrainMover>();
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
        // Need to make sure speed changes are always checked every frame
        ValidateLevelSpeed();
    }

    // ********************************************************************************************************

    // IncreaseGravity
    // Raises amount of gravity in scene
    // Takes: Nothing
    // Returns: Nothing
    public void IncreaseGravity()
    {
        if (gm != null)
        {
            // Validator to prevent drag levels from getting overboard
            if (gm.GetComponent<GravityLevel>().SetGravityLevel != 10)
            {
                gm.GetComponent<GravityLevel>().SetGravityLevel++;
            }
        }
    }

    // ********************************************************************************************************

    // DecreaseGravity
    // Lowers amount of gravity in scene
    // Takes: Nothing
    // Returns: Nothing
    public void DecreaseGravity()
    {
        if (gm != null)
        {
            // Ditto, but for negative values
            if (gm.GetComponent<GravityLevel>().SetGravityLevel != 0)
            {
                gm.GetComponent<GravityLevel>().SetGravityLevel--;
            }
        }
    }

    // ********************************************************************************************************

    // ValidateLevelSpeed
    // Modifies the TerrainMover's pacing of parts being moved through the player
    // It is dependent on the current gravityLevel 
    // and also requires the gravityAffectsSpeed flag to be true in TerrainMover
    public void ValidateLevelSpeed()
    {
        // Does gravity actually affect the speed of the level's movement?
        if (tm.IsGravityAffectingSpeed)
        {
            // If yes, then raise or lower it depending on our current gravity level
            switch(GetComponent<GravityLevel>().SetGravityLevel)
            {
                case 10:
                    tm.ModifyCyclingSpeed = TerrainMover.LevelSpeed.VeryLow;
                    break;
                case 9:
                    tm.ModifyCyclingSpeed = TerrainMover.LevelSpeed.Low;
                    break;
                case 8:
                    tm.ModifyCyclingSpeed = TerrainMover.LevelSpeed.Low;
                    break;
                case 7:
                    tm.ModifyCyclingSpeed = TerrainMover.LevelSpeed.Medium;
                    break;
                case 6:
                    tm.ModifyCyclingSpeed = TerrainMover.LevelSpeed.Medium;
                    break;
                case 5:
                    tm.ModifyCyclingSpeed = TerrainMover.LevelSpeed.High;
                    break;
                case 4:
                    tm.ModifyCyclingSpeed = TerrainMover.LevelSpeed.High;
                    break;
                case 3:
                    tm.ModifyCyclingSpeed = TerrainMover.LevelSpeed.VeryHigh;
                    break;
                case 2:
                    tm.ModifyCyclingSpeed = TerrainMover.LevelSpeed.VeryHigh;
                    break;
                case 1:
                    tm.ModifyCyclingSpeed = TerrainMover.LevelSpeed.Extreme;
                    break;
                default:
                    break;
            }
        }
    }

    // ********************************************************************************************************
}
