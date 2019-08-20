/*
 * =============================================================================
 * 
 * [Gravity Llama]
 * Proof-of-concept
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
 * - Negative gravity levels can cause affect objects to clip through colliders
 * 
 * 
 * Dependencies:
 * GravityLevel.cs
 * 
 * 
 * Changelog:
 * 20-08    Initial
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
    [HideInInspector]
    public Button btn;
    [HideInInspector]
    public GameObject gm;   // For retrieving the current level of gravity in scene

    void Start()
    {
        btn = GetComponent<Button>();

        // Raising
        //btn.onClick.AddListener(IncreaseGravity);

        // Lowering
        //btn.onClick.AddListener(DecreaseGravity);

        try
        {
            gm = GameObject.FindGameObjectWithTag("EditorOnly");    // For the GameManager object
        }
        catch (NullReferenceException)
        {
            Debug.Log("No GameManager detected within the scene. Please add the prefab to the scene or create one and add GravityLevel.cs to it.");
        }
    }

    // IncreaseGravity
    // Raises amount of gravity in scene
    // Takes: Nothing
    // Returns: Nothing
    public void IncreaseGravity()
    {
        gm.GetComponent<GravityLevel>().gravityLevel++;
    }

    // DecreaseGravity
    // Lowers amount of gravity in scene
    // Takes: Nothing
    // Returns: Nothing
    public void DecreaseGravity()
    {
        gm.GetComponent<GravityLevel>().gravityLevel--;
    }
}
