/*
 * =============================================================================
 * 
 * [Gravity Llama]
 * Proof-of-concept
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
 * Handles the level of 'gravity' in the scene
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
 * 
 * =============================================================================
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityLevel : MonoBehaviour
{
    [Tooltip("Float - Use this to determine how much gravity is currently within the scene. Be aware that too high or too low values can cause strange effects on RigidBody objects within the scene!")]
    public float gravityLevel = 10f;
}
