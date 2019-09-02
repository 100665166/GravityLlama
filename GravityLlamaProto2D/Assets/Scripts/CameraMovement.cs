/*
 * =============================================================================
 * 
 * [Gravity Llama]
 * Proof-of-concept
 * 
 * 
 * Script name:
 * CameraMovement.cs
 * 
 * 
 * Date:
 * 25-08-2019
 * 
 * 
 * Description:
 * Handles movement of camera
 * Used to track the player's movements through the scene
 * 
 * 
 * Parameters:
 * None
 * 
 * 
 * Attaches to:
 * Main camera object
 * 
 * 
 * Dependencies:
 * Player.cs
 * 
 * 
 * Changelog:
 * 25-08    Initial placeholder script
 * 31-08    Added functionality
 * 02-08    Supports following the player's rotation angle (NYI)
 * 
 * =============================================================================
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Distance between the player and camera
    private Vector3 offset;
    // Direction that player object is facing
    private Transform heading;

    [HideInInspector]
    public GameObject player;

    void Start()
    {
        try
        {
            player = GameObject.FindGameObjectWithTag("Player");    // Get the Llama
            offset = transform.position - player.transform.position;
        }
        catch (NullReferenceException)
        {
            Debug.Log("[CAMERAMOVEMENT.CS] There's no player object in the scene! Please add one first before running this script.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            //Debug.Log("Camera is following " + player.name + " at: " + transform.position);
            transform.position = player.transform.position + offset;

            //heading = player.transform;
            //transform.rotation = heading.rotation;
        }
    }
}
