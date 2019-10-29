/*
 * =============================================================================
 * 
 * [Gravity Llama]
 * Alpha
 * 
 * 
 * Script name:
 * Player.cs
 * 
 * 
 * Date:
 * 21-08-2019
 * 
 * 
 * Description:
 * Handles all player properties i.e. animations, etc.
 * Also determines whether the player is airborne or flat on the ground
 * 
 * 
 * Parameters:
 * None
 * 
 * 
 * Attaches to:
 * Player object
 * 
 * 
 * Dependencies:
 * GravityLevel.cs
 * 
 * 
 * Changelog:
 * 21-08    Initial placeholder script
 * 31-08    Added OnCollision events for checking llama stability
 * 08-09    Removed some redundant functionality
 * 11-09    Added properties and several new functions
 * 18-09    Added support for lane switching, jumpStrength properties
 * 06-10    Fixed missing (?) IsPlayerJumping variable
 * 19-10    Lane switching behaviours/conditions removed
 * 29-10    Heavy gravity to prevent llama "flying"
 * 
 * =============================================================================
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // ********************************************************************************************************

    [SerializeField]
    [Tooltip("Determines how high the player can jump.")]
    private float jumpStrength = 1000f;

    // For checking whether the llama can jump again or not
    private bool isGrounded = false;
    private bool isJumping = false;

    // Flags
    private bool isBeingDragged = false;

    private GameObject gm;
    private Rigidbody rb;
    private GameObject puller;  // Needed to prevent the llama from "flying"

    // ********************************************************************************************************

    public float GetJumpStrength { get => jumpStrength; }
    public float SetJumpStrength { set => jumpStrength = value; }
    public bool IsGrounded { get => isGrounded; set => isGrounded = value; }
    public bool IsJumping { get => isJumping; set => isJumping = value; }
    public bool IsBeingDragged { get => isBeingDragged; }
    public bool SetDragState { set => isBeingDragged = value; }

    // ********************************************************************************************************

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        puller = GameObject.FindGameObjectWithTag("Puller");

        try
        {
            gm = GameObject.FindGameObjectWithTag("GameManager");
        }
        catch (NullReferenceException)
        {
            Debug.Log("[PLAYER.CS] No GameManager detected within the scene. Please add the prefab to the scene or create one and add GravityLevel.cs to it.");
        }
    }

    // ********************************************************************************************************

    void Update()
    {
        rb.drag = gm.GetComponent<GravityLevel>().SetGravityLevel;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    // ********************************************************************************************************

    void FixedUpdate()
    {
        // This is only performed when the llama needs to be pulled back towards the level's terrain
        if (isBeingDragged)
        {
            rb.AddForce((puller.transform.position - transform.position) * 10000f * Time.smoothDeltaTime);
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

    // For detecting whether the player is grounded
    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "TerrainWall")
        {
            //Debug.Log("Llama is on the ground.");
            IsGrounded = true;
        }
    }

    // ********************************************************************************************************

    // Ditto, but for checking whether the llama's airborne or not
    void OnCollisionExit(Collision c)
    {
        if (c.gameObject.tag == "TerrainWall")
        {
            //Debug.Log("Llama is in the air.");
            IsGrounded = false;
        }
    }

    // ********************************************************************************************************

    // Jump
    // Llama hops into the air
    // Takes: Nothing
    // Returns: Nothing
    public void Jump()
    {
        if (IsGrounded)
        {
            rb.AddForce(Vector3.up * jumpStrength);

            /*// Slightly more powerful jump if we're on medium/high gravity
            if (gm.GetComponent<GravityLevel>().SetGravityLevel > 5)
            {
                rb.AddForce(Vector3.up * (jumpStrength * 2));
            }
            else if (gm.GetComponent<GravityLevel>().SetGravityLevel < 2)
            {
                // Need a very strong jump on low gravity levels
                rb.AddForce(Vector3.up * (jumpStrength * 3));
            }
            else
            {
                // Default power if gravity is regarded as "normal"
                rb.AddForce(Vector3.up * jumpStrength);
            }*/

            // Technically not needed but for safety, this will prevent any further jumping until we land
            IsGrounded = false;
        }
    }

    // ********************************************************************************************************

    // DropInstantly
    // Makes the llama fall quickly (one shot behaviour)
    // Takes: Nothing
    // Returns: Nothing
    public void DropInstantly()
    {
        rb.AddForce((puller.transform.position - transform.position) * 10000f);
    }

    // ********************************************************************************************************
}
