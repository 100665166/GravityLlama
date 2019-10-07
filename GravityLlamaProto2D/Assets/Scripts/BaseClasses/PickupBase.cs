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
 * 24-09    Added bobbing effect for all pickups (cosmetic only)
 * 25-09    Disabled bobbing effect for now
 * 
 * =============================================================================
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupBase : MonoBehaviour
{
    /*private float degreesPerSecond = 15.0f;
    private float amplitude = 0.5f;
    private float frequency = 1f;
    private Vector3 posOffset = new Vector3();
    private Vector3 tempPos = new Vector3();*/

    protected virtual void Reset()
    {
        hideFlags = HideFlags.HideInInspector;
    }

    protected GameObject gm;

    protected virtual void Start()
    {
        //posOffset = transform.position;

        try
        {
            gm = GameObject.FindGameObjectWithTag("EditorOnly");
        }
        catch (NullReferenceException)
        {
            Debug.Log("[PICKUPBASE.CS] No GameManager detected within the scene. Please add the prefab to the scene or create one and add GravityLevel.cs to it.");
        }
    }

    protected virtual void Update()
    {
        transform.Rotate(0, 0, 200 * Time.deltaTime);
        //transform.Rotate(new Vector3(0f, Time.deltaTime * 15f, 0f), Space.World);
        /*transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;*/
    }

    protected virtual void OnTriggerEnter(Collider c)
    {
    }
}
