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
 * ScoringSystem.cs
 * 
 * 
 * Changelog:
 * 07-10    Initial
 * 08-10    Temporarily restarts level once goal reached
 * 22-10    Supports string-based scene changes; saving high scores
 * 
 * =============================================================================
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalEndLevel : MonoBehaviour
{
    [Tooltip("String - Type in the name of the scene that needs to be loaded once this trigger is hit.\n\nMUST BE THE EXACT NAME OF THE SCENE!")]
    public string changeToLevel = "";

    private GameObject gm;

    // ********************************************************************************************************

    void Start()
    {
        try
        {
            gm = GameObject.FindGameObjectWithTag("GameManager");
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

            // Pass SaveScore the name of the scene (SaveScore handles the actual score saving)
            gm.GetComponent<ScoringSystem>().SaveScore(SceneManager.GetActiveScene().name.ToString());
            Debug.Log("Saving high score...");

            //SceneManager.LoadScene("Level1");   // Temp
            SceneManager.LoadScene(changeToLevel);
        }
    }
}
