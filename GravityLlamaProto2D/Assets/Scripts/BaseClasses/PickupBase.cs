/*
 * =============================================================================
 * 
 * [Gravity Llama]
 * Alpha
 * 
 * 
 * Script name:
 * PickupBase.cs
 * 
 * 
 * Date:
 * 08-09-2019
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
 * This is the parent class for all pickups
 * It SHOULD NOT be attached to anything by itself!
 * 
 * 
 * Dependencies:
 * GravityLevel.cs
 * 
 * 
 * Changelog:
 * 08-09    Initial
 * 
 * =============================================================================
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupBase : MonoBehaviour
{
    void Reset()
    {
        hideFlags = HideFlags.HideInInspector;
    }

    protected GameObject gm;

    protected virtual void Start()
    {
        try
        {
            gm = GameObject.FindGameObjectWithTag("EditorOnly");
        }
        catch (NullReferenceException)
        {
            Debug.Log("[PICKUP.CS] No GameManager detected within the scene. Please add the prefab to the scene or create one and add GravityLevel.cs to it.");
        }
    }

    protected virtual void OnTriggerEnter(Collider c)
    {
    }
}
