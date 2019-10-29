/*
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
 * 25-09    Temporarily disabled script again due to bugs
 * 06-10    CycleTerrain no longer segmented, levels move linearly as one giant prefab
 * 19-10    Dynamic speed change in CycleTerrain disabled and hidden again
 * 29-10    Functionality for end of level transitions
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
    [Tooltip("Drag the prefab of the level into this field.\n\nMake make sure it has a GOAL (dummy GameObject with IsTrigger Box Collider) defined the 'end' of the level prefab.")]
    public GameObject partToCycle;  // Changed to single GameObject rather than List

    [Header("GOAL")]
    [Tooltip("Drag (or select) the one GameObject on the scene which acts as a 'goal' for finishing the stage.\nIt needs to have a [Collider] component with the [IsTrigger] flag toggled on!")]
    public GameObject goal;

    [Header("OPTIONS")]
    [SerializeField]
    private float partMoveSpeed = 10f;   // Actual movement speed of level prefab based on cyclingSpeed

    [Tooltip("Select how fast the stage will cycle at the start.\nHigher or lower gravity levels can auto-affect this as well.")]
    private LevelSpeed cyclingSpeed = LevelSpeed.Medium;    // We'll default to Medium since gravityLevel is usually always at 5 anyway
    [Tooltip("Can gravity affect the speed at which parts 'move'?\n\nIf this flag is disabled, the speed at which levels cycle will be fixed for the entirety of this level (specifically).")]
    private bool gravityAffectsSpeed = false; // Does gravity make the level go faster, Y/N?

    private bool endOfLevel = false;    // Have we reached our goal yet?
    private GameObject gm;
    private GameObject player;

    // ********************************************************************************************************

    public LevelSpeed ModifyCyclingSpeed { get => cyclingSpeed; set => cyclingSpeed = value; }
    public bool IsGravityAffectingSpeed { get => gravityAffectsSpeed; set => gravityAffectsSpeed = value; }
    public bool HasFinishedLevel { get => endOfLevel; set => endOfLevel = value; }

    // ********************************************************************************************************

    void Start()
    {
        // ----------------------------------------------------------------------------------
        // DEBUG STUFF
        // Technically we don't need this part but it never hurts to be careful...
        if (partToCycle == null)
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
        // If gravity does affect how fast the level moves then...
        if (gravityAffectsSpeed)
        {
            // ...check the gravity levels every frame to ensure consistent terrain movement
            CycleTerrain();
        }

        // Check if the player's reached the end of the level
        if (endOfLevel)
        {
            EndLevel();
        }
    }

    // ********************************************************************************************************

    void FixedUpdate()
    {
        // Start moving level parts immediately
        partToCycle.transform.Translate(Vector3.back * Time.deltaTime * partMoveSpeed, Space.World);
    }

    // ********************************************************************************************************

    // CycleTerrain
    // Determines how fast the level moves to simulate llama running forward
    // Takes: Nothing
    // Returns: Nothing
    public void CycleTerrain()
    {
        switch (cyclingSpeed)
        {
            // 10
            case LevelSpeed.VeryLow:
                partMoveSpeed = 4f;
                break;
            // 9+8
            case LevelSpeed.Low:
                partMoveSpeed = 8f;
                break;
            // 7+6
            case LevelSpeed.Medium:
                partMoveSpeed = 12f;
                break;
            // 5+4
            case LevelSpeed.High:
                partMoveSpeed = 16f;
                break;
            // 3+2
            case LevelSpeed.VeryHigh:
                partMoveSpeed = 20f;
                break;
            // 1
            case LevelSpeed.Extreme:
                partMoveSpeed = 24f;
                break;
            default:
                // Should never occur but whatever...
                partMoveSpeed = 8f;
                break;
        }
    }

    // ********************************************************************************************************

    // EndLevel
    // Finish the level and tell GameManager to switch to show scores/transition scenes
    // Takes: Nothing
    // Returns: Nothing
    public void EndLevel()
    {
        // Stop moving level instantly
        gravityAffectsSpeed = false;
        partMoveSpeed = 0f;

        // Scene transition stuff blah blah blah
        endOfLevel = true;
        //Debug.Log("<color=yellow>YOU WIN!</color>");
    }

    // ********************************************************************************************************
}
