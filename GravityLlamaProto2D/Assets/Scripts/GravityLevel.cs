/*
 * =============================================================================
 * 
 * [Gravity Llama]
 * Alpha
 * 
 * 
 * Script name:
 * GravityLevel.cs
 * 
 * 
 * Date:
 * 17-08-2019
 * 
 * 
 * Description:
 * Handles the amount of 'gravity' in the scene
 * 
 * 
 * Parameters:
 * None
 * 
 * 
 * Attaches to:
 * GameManger
 * 
 * 
 * Dependencies:
 * None
 * 
 * 
 * Changelog:
 * 17-08    Initial
 * 11-09    Tweaked to support properties
 * 25-09    Added support for UI gauge bar display
 * 21-10    Added range limits to prevent gravity from going overboard
 * 
 * =============================================================================
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityLevel : MonoBehaviour
{
    // ********************************************************************************************************

    [SerializeField]
    [Tooltip("Use this to determine how much gravity is currently within the scene.")]
    [Range(0f, 10f)]
    private float gravityLevel = 5f;

    // For UI elements
    private Slider gravitySlider;

    // ********************************************************************************************************

    public float SetGravityLevel { get => gravityLevel; set => gravityLevel = value; }

    // ********************************************************************************************************

    void Start()
    {
        try
        {
            // Grab the gauge on canvas first
            gravitySlider = GameObject.FindGameObjectWithTag("GravityStatusBar").GetComponent<Slider>();
            gravitySlider.value = gravityLevel;
        }
        catch (NullReferenceException)
        {
            Debug.Log("[GRAVITYLEVEL.CS] You need a Slider UI object in the canvas. It must have the GravityStatusBar tag applied to it for this script to work!");
        }
    }

    // ********************************************************************************************************

    void Update()
    {
        if (gravitySlider != null)
        {
            gravitySlider.value = gravityLevel;
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
    
}
