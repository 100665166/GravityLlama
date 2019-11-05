﻿/*
 * =============================================================================
 * 
 * [Gravity Llama]
 * Alpha
 * 
 * 
 * Script name:
 * AudioManager.cs
 * 
 * 
 * Date:
 * 08-10-2019
 * 
 * 
 * Description:
 * Stores sound effects that can be accessed by other scripts
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
 * 08-10    Initial
 * 
 * =============================================================================
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer MainMixer;
    public float MainVol;
    public float volume;
    public AudioClip negativePickupSound;
    public AudioClip positivePickupSound;

    private void Start()
    {
        MainMixer.GetFloat("MasterVolume", out(MainVol));
        volume = (.8f - Mathf.Abs(MainVol / 100))*.05f;
    }
    private void Update()
    {
        MainMixer.GetFloat("MasterVolume", out (MainVol));
        volume = (.8f - Mathf.Abs(MainVol / 100)) * .05f;
        Debug.Log("volume:" + volume);
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
}
