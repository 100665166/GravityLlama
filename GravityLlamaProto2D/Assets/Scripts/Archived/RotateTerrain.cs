/*
 * =============================================================================
 * 
 * [Gravity Llama]
 * Alpha
 * 
 * 
 * Script name:
 * RotateTerrain.cs
 * 
 * 
 * Date:
 * 19-10-2019
 * 
 * 
 * Description:
 * Rotates the terrain itself (player not affected)
 * 
 * 
 * Parameters:
 * None
 * 
 * 
 * Attaches to:
 * Any GameObject with a Collider trigger
 * 
 * 
 * Dependencies:
 * None
 * 
 * 
 * Changelog:
 * 19-10    Initial
 * 21-10    Deprecated
 * 
 * =============================================================================
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTerrain : MonoBehaviour
{
    [Tooltip("What angle should the terrain rotate towards at this point?")]
    public float angle = 0f;

    void OnTriggerEnter(Collider col)
    {
        // Are we the player?
        if (col.CompareTag("Player"))
        {
            transform.parent.Rotate(new Vector3(0, 0, angle));
        }
    }
}
