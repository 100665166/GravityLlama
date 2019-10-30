using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// MainMenu.cs main script for managing main menu button presses and scene switching
/// 
/// changelog
/// 19/9 - init
/// </summary>
public class MainMenu : MonoBehaviour
{

    //uses scenemanager to load scene
    public void PlayGame()
    {
        SceneManager.LoadScene("Level2");
    }
    //quit game/application
    public void QuitGame()
    {
        Debug.Log("QuitGame!");
        Application.Quit();
    }
}
