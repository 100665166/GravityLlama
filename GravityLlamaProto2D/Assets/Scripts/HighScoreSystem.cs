/*
 * =============================================================================
 * 
 * [Gravity Llama]
 * Alpha
 * 
 * 
 * Script name:
 * HighScoreSystem.cs
 * 
 * 
 * Date:
 * 05-11-2019
 * 
 * 
 * Description:
 * Handles end-of-level scoring stuff
 * Actual in-game tracking is done in ScoringSystem.cs
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
 * ScoringSystem.cs
 * 
 * 
 * Notes:
 * - The current system only supports one level and five scores
 * - Possible expansion methods would be to add two strings as an identifier
 * - Then sorting them based on level/rank string + score value
 * 
 * 
 * Changelog:
 * 05-11    Initial
 * 06-11    Made sorting happen earlier in the case of first-time runs
 * 06-11    Changed scoresText to hardcoded (there's only ever five anyway)
 * 
 * =============================================================================
 */

using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreSystem : MonoBehaviour
{
    // This stores the actual list of high scores (retrieved from PlayerPrefs)
    [Tooltip("The highest five recorded scores.\n\nThis is dependent on the player's PlayerPref settings so it will appear as a list of zeroes until a game is finished and a score is recorded.")]
    public List<int> highScores;

    [Tooltip("Drag the highscores UI object here.")]
    public GameObject leaderboard;

    [Header("TEXT OBJECTS")]
    // Text objects used for the high scores
    public GameObject scoresText1;
    public GameObject scoresText2;
    public GameObject scoresText3;
    public GameObject scoresText4;
    public GameObject scoresText5;

    // ********************************************************************************************************

    void Start()
    {
        // For testing only
        /*PlayerPrefs.SetFloat("HS1", 5f);
        PlayerPrefs.SetFloat("HS2", 1f);
        PlayerPrefs.SetFloat("HS3", 3f);
        PlayerPrefs.SetFloat("HS4", 2f);
        PlayerPrefs.SetFloat("HS5", 4f);*/

        // Always retrieve the stored high scores first
        LoadScore();

        //scoresText = GameObject.FindGameObjectsWithTag("Scores");

        // Set values on leaderboard initially
        UpdateScoreboard();

        // Then hide the screen (we don't show up again until the level is done)
        leaderboard.SetActive(false);

        // Also for testing
        /*foreach (int i in highScores)
        {
            Debug.Log(i);
        }
        Debug.Log("================= LOADED");
        RecordScore(3500);
        foreach (int i in highScores)
        {
            Debug.Log(i);
        }
        Debug.Log("================= SAVING");
        Debug.Log(PlayerPrefs.GetFloat("HS1"));
        Debug.Log(PlayerPrefs.GetFloat("HS2"));
        Debug.Log(PlayerPrefs.GetFloat("HS3"));
        Debug.Log(PlayerPrefs.GetFloat("HS4"));
        Debug.Log(PlayerPrefs.GetFloat("HS5"));
        Debug.Log("================= AFTER SAVING");*/
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

    // LoadScore
    // Grabs the highest five scores from the player's prefs
    // Takes: Nothing
    // Returns: Nothing
    public void LoadScore()
    {
        // Empty the list if there are scores already within it
        if (highScores != null)
        {
            highScores.Clear();
        }

        // Hardcoded
        highScores.Add((int)PlayerPrefs.GetFloat("HS1"));
        highScores.Add((int)PlayerPrefs.GetFloat("HS2"));
        highScores.Add((int)PlayerPrefs.GetFloat("HS3"));
        highScores.Add((int)PlayerPrefs.GetFloat("HS4"));
        highScores.Add((int)PlayerPrefs.GetFloat("HS5"));

        // Now rearrange them in order from highest to lowest and update the list
        highScores.Sort();
        //highScores.Reverse();
    }

    // ********************************************************************************************************

    // CheckScore
    // Determines if the latest score should be recorded
    // Takes: int
    // Returns: bool
    public bool CheckScore(int score)
    {
        bool canSaveScore = false;  // For level restarts

        // Iterate through all five until we find the first lower than our current score
        for (int i = 0; i < highScores.Count; i++)
        {
            // Find the score that is lower than the current one
            if (highScores[i] < score)
            {
                // Toggle flag since we should save
                canSaveScore = true;
                // Replace the lowest score with the latest
                highScores[4] = score;
                // Now exit the loop; we don't care about the other scores
                break;
            }
            else
            {
                canSaveScore = false;
            }
        }

        if (canSaveScore)
        {
            return true;
        }
        else
        {
            //highScores.Sort();
            return false;
        }
    }

    // ********************************************************************************************************

    // RecordScore
    // Does the actual saving of the current score passed from ScoringSystem
    // Takes: int
    // Returns: Nothing
    public void RecordScore(int score)
    {
        // For first time runs
        //highScores.Sort();
        //highScores.Reverse();
        //UpdateScoreboard();

        // Validate the high score to see if we need to store it
        if (CheckScore(score))
        {
            // Sort the scores one last time before saving
            //highScores.Sort();
            //highScores.Reverse();
            //UpdateScoreboard();

            // Now store them into the player's profile
            // Shouldn't be hardcoded but since we only have five anyway...
            PlayerPrefs.SetFloat("HS1", highScores[0]);
            PlayerPrefs.SetFloat("HS2", highScores[1]);
            PlayerPrefs.SetFloat("HS3", highScores[2]);
            PlayerPrefs.SetFloat("HS4", highScores[3]);
            PlayerPrefs.SetFloat("HS5", highScores[4]);

            // Update the board again
            highScores[0] = (int)PlayerPrefs.GetFloat("HS1");
            highScores[1] = (int)PlayerPrefs.GetFloat("HS2");
            highScores[2] = (int)PlayerPrefs.GetFloat("HS3");
            highScores[3] = (int)PlayerPrefs.GetFloat("HS4");
            highScores[4] = (int)PlayerPrefs.GetFloat("HS5");

            /*for (int i = 0; i < scoresText.Length; i++)
            {
                scoresText[i].GetComponent<Text>().text = highScores[i].ToString();
            }*/

            UpdateScoreboard();
        }
        else
        {
            // No score is saved if the latest session didn't earn above the previous ones
            //highScores.Sort();
            //highScores.Reverse();

            /*for (int i = 0; i < scoresText.Length; i++)
            {
                scoresText[i].GetComponent<Text>().text = highScores[i].ToString();
            }*/

            UpdateScoreboard();
            //Debug.Log("No high score was recorded from this session.");
        }
    }

    // ********************************************************************************************************

    // UpdateScoreboard
    // Updates values on the leaderboard
    // Takes: Nothing
    // Returns: Nothing
    public void UpdateScoreboard()
    {
        highScores.Sort();
        highScores.Reverse();

        scoresText1.GetComponent<Text>().text = highScores[0].ToString();
        scoresText2.GetComponent<Text>().text = highScores[1].ToString();
        scoresText3.GetComponent<Text>().text = highScores[2].ToString();
        scoresText4.GetComponent<Text>().text = highScores[3].ToString();
        scoresText5.GetComponent<Text>().text = highScores[4].ToString();

        /*for (int i = 0; i < scoresText.Length; i++)
        {
            scoresText[i].GetComponent<Text>().text = highScores[i].ToString();
        }*/
    }

    // ********************************************************************************************************

    // DisplayScoreboard
    // Shows the leaderboard and lerps in the items
    // Takes: Nothing
    // Returns: Nothing
    public void DisplayScoreboard()
    {
        leaderboard.SetActive(true);
    }

    // ********************************************************************************************************
}
