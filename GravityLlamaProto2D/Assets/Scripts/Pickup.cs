/*
 * =============================================================================
 * 
 * [Gravity Llama]
 * Proof-of-concept
 * 
 * 
 * Script name:
 * Pickup.cs
 * 
 * 
 * Date:
 * 21-08-2019
 * 
 * 
 * Description:
 * Base class for handling all collectible properties
 * Child scripts should inherit from this as a superclass
 * 
 * 
 * Parameters:
 * None
 * 
 * 
 * Attaches to:
 * Any objects intended to serve as collectibles
 * 
 * 
 * Dependencies:
 * AdjustGravity.cs
 * ScoringSystem.cs
 * 
 * 
 * Changelog:
 * 21-08    Initial placeholder script
 * 29-08    Pickups now store an enum to denote their effect on gravity
 * 31-08    Pickups can now add a score amount
 * 
 * =============================================================================
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum AffectsGravity
    {
        Raise, 
        Lower
    }

    [Tooltip("Determines what the pickup is meant to do (raise/lower gravity).")]
    public AffectsGravity operationType;
    [HideInInspector]
    public GameObject gm;   // For retrieving the current level of gravity in scene

    void Start()
    {
        try
        {
            gm = GameObject.FindGameObjectWithTag("EditorOnly");    // For the GameManager object
        }
        catch (NullReferenceException)
        {
            Debug.Log("[PICKUP.CS] No GameManager detected within the scene. Please add the prefab to the scene or create one and add GravityLevel.cs to it.");
        }
    }

    // For whenever the player collides into the pickup
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            // Modify gravity based on operationType value
            switch(operationType)
            {
                case AffectsGravity.Lower:
                    if (gm != null)
                    {
                        gm.GetComponent<AdjustGravity>().DecreaseGravity();
                        gm.GetComponent<ScoringSystem>().currentScore++;
                    }
                    break;
                case AffectsGravity.Raise:
                    if (gm != null)
                    {
                        gm.GetComponent<AdjustGravity>().IncreaseGravity();
                        gm.GetComponent<ScoringSystem>().currentScore++;
                    }
                    break;
                default:
                    // Default should never occur but just in case...
                    break;
            }

            // Debug.Log("Delete me!");
            Destroy(gameObject);
        }
    }
}
