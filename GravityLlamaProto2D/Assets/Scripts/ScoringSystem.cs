/*
 * =============================================================================
 * 
 * [Gravity Llama]
 * Alpha
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
 * HighScoresScreen.cs
 * 
 * 
 * Changelog:
 * 31-08    Initial
 * 21-10    Scores can now be read/written from an external text file
 * 22-10    Integrated scores.txt into Resources; no longer uses StreamReader
 * 29-10    Score saving no longer handled by this script; transferred to HighScoresScreen
 * 
 * =============================================================================
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    [Tooltip("Integer - The player's current score amount.")]
    public int currentScore = 0;

    [HideInInspector]
    GameObject uiScoreTracker;  // Get the score text object on the canvas

    // ********************************************************************************************************

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

    // ********************************************************************************************************

    void Update()
    {
        UpdateScore();
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

    // ********************************************************************************************************

    // SaveScore
    // Pass the current level's score count to HighScoresScreen
    // Takes: Nothing
    // Returns: Nothing
    public void SaveScore()
    {
        HighScoresScreen scoreTable = GameObject.FindGameObjectWithTag("HighScoreTable").GetComponent<HighScoresScreen>();
        scoreTable.AddHighScore(currentScore);
    }
}
