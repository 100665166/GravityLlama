/*
 * =============================================================================
 * 
 * [Gravity Llama]
 * Alpha
 * 
 * 
 * Script name:
 * HighScoresScreen.cs
 * 
 * 
 * Date:
 * 28-10-2019
 * 
 * 
 * Description:
 * Handles end-of-level UI functionality.
 * Displays high scores screen with top five rankings.
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
 * Changelog:
 * 28-10    Initial
 * 
 * =============================================================================
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoresScreen : MonoBehaviour
{
    [Tooltip("Defines the amount of space in-between rows.")]
    float rowDist = 35f;

    // For the scores themselves
    private List<HighScoreEntry> highScoresList;
    private List<Transform> highScoreEntryTransforms;

    // Child objects in the HighScoresTable prefab
    private Transform scoreContainer;
    private Transform scoreTemplate;

    // Outputs
    private string rankText = "";
    private int scoreText = 0;
    //private string nameText = "";

    // ********************************************************************************************************

    void Start()
    {
        scoreContainer = transform.Find("HighScoreContainer");
        scoreTemplate = scoreContainer.Find("HighScoreTemplate");

        scoreTemplate.gameObject.SetActive(false);

        // We only want a max of five scores displayed
        string jsonString = PlayerPrefs.GetString("SaveData");
        HighScores loadedScores = JsonUtility.FromJson<HighScores>(jsonString);

        // Auto sort the five scores in order of highest to lowest
        for (int i = 0; i < loadedScores.highScores.Count; i++)
        {
            for (int j = i + 1; j < loadedScores.highScores.Count; j++)
            {
                if (loadedScores.highScores[j].score > loadedScores.highScores[i].score)
                {
                    // Swap values by making a new fake entry and exchanging them there
                    HighScoreEntry tempScore = loadedScores.highScores[i];
                    loadedScores.highScores[i] = loadedScores.highScores[j];
                    loadedScores.highScores[j] = tempScore;
                }
            }
        }

        // Start adding the entries
        highScoreEntryTransforms = new List<Transform>();
        foreach (HighScoreEntry hs in highScoresList)
        {
            PopulateTable(hs, scoreContainer, highScoreEntryTransforms);
        }

        /*
        // Save the scores
        HighScores currentSessionScores = new HighScores { highScores = highScoresList };
        string savedScores = JsonUtility.ToJson(currentSessionScores);
        PlayerPrefs.SetString("SaveData", savedScores);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("SaveData"));*/
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

    // EndGame
    // Displays the high score screen and enables end-of-level UI functionality
    // Takes: Nothing
    // Returns: Nothing
    public void EndGame()
    {

    }

    // ********************************************************************************************************

    // AddHighScore
    // Adds a single new high score entry to the table
    // Takes: int
    // Returns: Nothing
    public void AddHighScore(int s)
    {
        HighScoreEntry hs = new HighScoreEntry { score = s };

        // Don't forget to update the player's save data before adding a new one!
        string jsonString = PlayerPrefs.GetString("SaveData");
        HighScores scores = JsonUtility.FromJson<HighScores>(jsonString);
        scores.highScores.Add(hs);

        // Now update the save data with the new entry
        string json = JsonUtility.ToJson(scores);
        PlayerPrefs.SetString("SaveData", json);
        PlayerPrefs.Save();
    }

    // ********************************************************************************************************

    // PopulateTable
    // Fills the high score list with data
    // Takes: HighScoreEntry, Transform, List<Transform>
    // Returns: Nothing
    public void PopulateTable(HighScoreEntry hs, Transform t, List<Transform> tl)
    {
        // Populate each of the entries
        Transform scoreTransform = Instantiate(scoreTemplate, t);
        RectTransform scoreRectTransform = scoreTransform.GetComponent<RectTransform>();
        scoreRectTransform.anchoredPosition = new Vector2(0, -rowDist * tl.Count);
        scoreTransform.gameObject.SetActive(true);  // Then unhide them

        // Now we need to adjust the ranks of each row
        int rank = tl.Count + 1;   // Is index + 1 (otherwise first place would never be one)

        switch (rank)
        {
            // Only first, second and third places get special suffixes (obviously)
            case 1:
                rankText = "1ST";
                break;
            case 2:
                rankText = "2ND";
                break;
            case 3:
                rankText = "3RD";
                break;
            default:
                // Everything else just gets a generic 'TH' suffix after it
                rankText = rank + "TH";
                break;
        }

        // Final output with populated entries
        // TODO: There should be a better way of outputting this but whatever...
        scoreTransform.Find("RankingNumber").GetComponent<Text>().text = rankText;

        int score = hs.score;
        scoreTransform.Find("RankingScore").GetComponent<Text>().text = score.ToString();

        //string name = hs.name;
        //scoreTransform.Find("RankingName").GetComponent<Text>().text = name;

        // Now add it to the actual table
        tl.Add(scoreTransform);
    }

    // ********************************************************************************************************

    // [OBJECT]
    // HighScores
    // Used for saving the score table
    public class HighScores
    {
        public List<HighScoreEntry> highScores;
    }


    // ********************************************************************************************************

    // [OBJECT]
    // HighScoreEntry
    // Represents the data 'rows' themselves
    public class HighScoreEntry
    {
        public int score;
        //public string name;
    }

    // ********************************************************************************************************
}
