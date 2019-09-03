/*
 * =============================================================================
 * 
 * [Gravity Llama]
 * Proof-of-concept
 * 
 * 
 * Script name:
 * ScoringSystem.cs
 * 
 * 
 * Date:
 * 31-08-2019
 * 
 * 
 * Description:
 * For tracking the player's score
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
 * 31-08    Initial
 * 
 * =============================================================================
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    [Tooltip("Integer - The player's current score amount.")]
    public int currentScore = 0;

    [HideInInspector]
    public int highScore = 0;   // For storing the player's highest achieved score from the latest gameplay session NB: Not functional yet
    [HideInInspector]
    GameObject uiScoreTracker;  // Get the score text object on the canvas

    void Start()
    {
        try
        {
            uiScoreTracker = GameObject.FindGameObjectWithTag("ScoreCounter");    // Grab the score tracker on UI
        }
        catch (NullReferenceException)
        {
            Debug.Log("[SCORINGSYSTEM.CS] You need a Text UI object to the canvas. It must have the ScoreCounter tag applied to it for this script to work!");
        }
    }

    void Update()
    {
        UpdateScore();
    }

    // UpdateScore
    // Constantly updates the player's current score amount on the UI
    // Takes: Nothing
    // Returns: Nothing
    public void UpdateScore()
    {
        if (uiScoreTracker != null)
        {
            uiScoreTracker.GetComponent<Text>().text = currentScore.ToString();
        }
    }
}
