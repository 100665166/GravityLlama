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
 * None
 * 
 * 
 * Changelog:
 * 31-08    Initial
 * 21-10    Scores can now be read/written from an external text file
 * 22-10    Integrated scores.txt into Resources; no longer uses StreamReader
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
    public int highScore = 0;   // For storing the player's highest achieved score from the latest gameplay session
    [HideInInspector]
    GameObject uiScoreTracker;  // Get the score text object on the canvas
    [HideInInspector]
    public TextAsset scoresText;    // For storing data from scores.txt

    private string[] scores;

    // ********************************************************************************************************

    void Start()
    {
        // Grab the scores file
        scoresText = (TextAsset)Resources.Load("scores", typeof(TextAsset));
        //scores = scoresText.text;
        //Debug.Log("Scores.txt now loaded. It contains: " + scores);

        try
        {
            uiScoreTracker = GameObject.FindGameObjectWithTag("ScoreCounter");    // Grab the score tracker on UI
            ReadScore(SceneManager.GetActiveScene().name.ToString());   // Grab the highscore for this level (run just once)
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

    // ReadScore
    // Grab the highscore for the current scene and transfer it to currentScore
    // Takes: string
    // Returns: int
    public int ReadScore(string level)
    {
        int highScore = 0;  // Temp for storing the soon-to-be returned score
        
        /*
        // We need to find the right score first
        for (int i = 0; i < highScores.Length; i++)
        {
            if (highScores[i].StartsWith(level))
            {
                // Then output the correct one if it matches the scene's name
                highScore = 
                break;
            }
        }*/

        /*
        // Retrieve it from scores.txt
        StreamReader reader = new StreamReader("Assets/Resources/scores.txt");
        string line = reader.ReadLine();
        string[] split = line.Split(',');

        // Only for this scene
        if (split[0] == level.ToString())
        {
            // We only care about the score obviously
            highScore = int.Parse(split[1]);
        }*/

        // The game assumes no previous highscore was saved if a matching scene string isn't found
        return highScore;
    }

    // ********************************************************************************************************

    // SaveScore
    // Save the current level's score count to external text file
    // Takes: string
    // Returns: Nothing
    public void SaveScore(string level)
    {
        // Convert end of level score (activated by GoalEndLevel's OnTriggerEnter)
        string serializedData = level + "," + currentScore.ToString();  // Format is similar to csv with comma as separator

        /*
        // Now write the files to scores.txt
        StreamWriter writer = new StreamWriter("Assets/Resources/scores.txt", true);
        writer.Write(serializedData);*/
    }
}
