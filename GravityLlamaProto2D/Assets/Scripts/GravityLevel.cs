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
 * 
 * =============================================================================
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityLevel : MonoBehaviour
{
    // ********************************************************************************************************

    [SerializeField]
    [Tooltip("Use this to determine how much gravity is currently within the scene.")]
    private float gravityLevel = 5f;

    public float SetGravityLevel { get => gravityLevel; set => gravityLevel = value; }

    // ********************************************************************************************************
}
