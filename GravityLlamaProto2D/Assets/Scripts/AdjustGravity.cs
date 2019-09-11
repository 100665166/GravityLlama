/*
 * =============================================================================
 * 
 * [Gravity Llama]
 * Alpha
 * 
 * 
 * Script name:
 * AdjustGravity.cs
 * 
 * 
 * Date:
 * 20-08-2019
 * 
 * 
 * Description:
 * Modifies gravity levels
 * 
 * 
 * Parameters:
 * None
 * 
 * 
 * Attaches to:
 * Any Canvas UI element (buttons, etc.)
 * 
 * 
 * Known issues:
 * - Negative gravity levels can cause affected objects to clip through colliders
 * 
 * 
 * Dependencies:
 * GravityLevel.cs
 * 
 * 
 * Changelog:
 * 20-08    Initial
 * 21-08    Added validators to functions
 * 25-08    Added validators to prevent unintended gravity settings
 * 11-09    Deleted some artefacts from the PoC
 * 
 * =============================================================================
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AdjustGravity : MonoBehaviour
{
    // ********************************************************************************************************

    private GameObject gm;   // For retrieving the current level of gravity in scene

    // ********************************************************************************************************

    void Start()
    {
        try
        {
            gm = GameObject.FindGameObjectWithTag("EditorOnly");    // For the GameManager object
        }
        catch (NullReferenceException)
        {
            Debug.Log("[ADJUSTGRAVITY.CS] No GameManager detected within the scene. Please add the prefab to the scene or create one and add GravityLevel.cs to it.");
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

    // IncreaseGravity
    // Raises amount of gravity in scene
    // Takes: Nothing
    // Returns: Nothing
    public void IncreaseGravity()
    {
        if (gm != null)
        {
            // Validator to prevent drag levels from getting overboard
            if (gm.GetComponent<GravityLevel>().SetGravityLevel != 10)
            {
                gm.GetComponent<GravityLevel>().SetGravityLevel++;
            }
        }
    }

    // ********************************************************************************************************

    // DecreaseGravity
    // Lowers amount of gravity in scene
    // Takes: Nothing
    // Returns: Nothing
    public void DecreaseGravity()
    {
        if (gm != null)
        {
            // Ditto, but for negative values
            if (gm.GetComponent<GravityLevel>().SetGravityLevel != 0)
            {
                gm.GetComponent<GravityLevel>().SetGravityLevel--;
            }
        }
    }

    // ********************************************************************************************************
}
