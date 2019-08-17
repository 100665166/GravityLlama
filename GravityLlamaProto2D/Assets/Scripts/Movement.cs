/*
 * =============================================================================
 * 
 * [Gravity Llama]
 * Proof-of-concept
 * 
 * 
 * Script name:
 * Movement.cs
 * 
 * 
 * Date:
 * 17-08-2019
 * 
 * 
 * Description:
 * Controller for handling player movement in 2D environment
 * 
 * 
 * Parameters:
 * None
 * 
 * 
 * Attaches to:
 * Any object but should be the Player
 * 
 * 
 * Dependencies:
 * Gravity.cs
 * 
 * 
 * Known issues:
 * - Player keeps sliding after moving left or right
 * - gravityLevel changes only take effect after jumping; should be handled elsewhere?
 * 
 * 
 * Changelog:
 * 17-08    Initial
 * 
 * =============================================================================
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Tooltip("Float - Adjusts how quickly the player moves left or right.")]
    public float movementSpeed = 1000f;
    [Tooltip("Float - Adjusts the jump height of the player (vertically).")]
    public float jumpSpeed = 500f; // Technically not needed but we'll separate it for now

    [HideInInspector]
    public Rigidbody rb;
    [HideInInspector]
    public GameObject gm;   // For retrieving the current level of gravity in scene

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        try
        {
            gm = GameObject.FindGameObjectWithTag("EditorOnly");    // For the GameManager object
        }
        catch (NullReferenceException)
        {
            Debug.Log("No GameManager detected within the scene. Please add the prefab to the scene or create one and add GravityLevel.cs to it.");
        }
    }

    void Update()
    {
        Move();

        // For jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    // Move
    // Player object moves left or right based on horizontal input
    // Takes: Nothing
    // Returns: Nothing
    public void Move()
    {
        // Grab horizontal input (support for gamepads, etc.)
        float hdir = Input.GetAxisRaw("Horizontal");

        // Grab direction of Vector; we don't care about other axes
        Vector3 vectDir = new Vector3(hdir, 0, 0);
        // Normalise it
        Vector3 vectUnit = vectDir.normalized;
        // Grab existing speed of RigidBody and multipler it based on value of movementSpeed
        Vector3 vectForce = vectUnit * movementSpeed * Time.deltaTime;

        // Apply force to the player's object
        rb.AddForce(vectForce);
    }

    // Jump
    // Player hops into the air
    // Takes: Nothing
    // Returns: Nothing
    public void Jump()
    {
        if (gm != null)
        {
            // Jump descent speed affected by value in GameManager
            rb.drag = gm.GetComponent<GravityLevel>().gravityLevel;
        }
        else
        {
            // Default to 10.0 if the GM doesn't exist for whatever reason
            rb.drag = 10f;
        }
        rb.AddForce(Vector3.up * jumpSpeed);
    }
}
