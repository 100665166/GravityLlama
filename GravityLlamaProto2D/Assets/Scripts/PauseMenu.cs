﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject PauseMenuCanvas;
    public AudioSource BGM;

    // For blur FX
    private VHS mainCamera;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<VHS>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("update");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Debug.Log("esc pressed");
            if (gameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        gameIsPaused = false;
        //mainCamera.enabled = false;  // Disable VHS blur effect
        PauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        BGM.Play();
    }

    void Pause()
    {
        gameIsPaused = true;
        //mainCamera.enabled = true; // Enable VHS blur effect
        PauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        BGM.Pause();
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadScene("MainMenu");
    }
}
