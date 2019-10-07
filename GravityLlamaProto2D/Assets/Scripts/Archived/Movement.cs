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
 * Player
 * 
 * 
 * Dependencies:
 * GravityLevel.cs
 * 
 * 
 * Known issues:
 * - It is still possible to move backwards
 * - Player's rotation does not adjust to face next waypoint (yet)
 * 
 * 
 * Changelog:
 * 17-08    Initial
 * 17-08    Added gravityLevel for horizontal movement
 * 25-08    Increased default value for jumpSpeed (500f to 750f)
 * 31-08    Added constantMovement flag and behaviours
 * 02-09    Archived (kept for backwards compatibility reasons)
 * 
 * =============================================================================
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    void Reset()
    {
        hideFlags = HideFlags.HideInInspector;
    }

    public enum MovementType
    {
        SideScroll, 
        Waypoints
    }

    private int waypointIndex = 0;  // Waypoint list index

    [Tooltip("Float - This value sets how fast the player will move automatically (it does not affect actual movement speed).")]
    public float crawlSpeed = 25f;  // This is for auto movement only
    [Tooltip("Float - Adjusts how quickly the player moves left or right.")]
    public float movementSpeed = 1000f;
    [Tooltip("Float - Adjusts the jump height of the player (vertically).")]
    public float jumpSpeed = 750f; // Technically not needed but we'll separate it for now
    [Tooltip("Determines what movement system the player object will be using for this scene.\n\n[SideScroll] -> Select this option for stages that are exclusively in 2D i.e. Llama moves only on one axis.\n[Waypoints] -> Select this option for stages that have 2.5D environments.")]
    public MovementType movementType = MovementType.SideScroll;
    [Tooltip("Boolean - Enables/disables the ability to chain jumps. If this flag is toggled, you can't jump again until you land on a surface.")]
    public bool disableChainJumps = true;
    [Tooltip("Transform - This is the list of nodes which the player will follow. Only use this for 2.5D scenes!")]
    public Transform[] waypoints;  // Waypoint list

    [HideInInspector]
    public GameObject player;
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
            player = GameObject.FindGameObjectWithTag("Player");    // For the llama
        }
        catch (NullReferenceException)
        {
            Debug.Log("[MOVEMENT.CS] No GameManager detected within the scene. Please add the prefab to the scene or create one and add GravityLevel.cs to it.");
            Debug.Log("[MOVEMENT.CS] There's no player object in the scene! Please add one first before running this script.");
        }
    }

    void Update()
    {
        if (gm != null)
        {
            // Movement speeds affected by value in GameManager
            rb.drag = gm.GetComponent<GravityLevel>().SetGravityLevel;
        }
        else
        {
            // Default to 0.0 if the GM doesn't exist for whatever reason
            rb.drag = 0f;
        }

        // General input
        Move();

        // Always check every frame BEFORE Jump validation to take place
        // to see if we're on a flat surface (use the "TerrainWall" tag for such objects)

        // For jumping (only works if the llama is on a flat surface)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    // We want physics movement to be in FixedUpdate so that it isn't tied to the frame rate
    void FixedUpdate()
    {
        if (movementType == MovementType.SideScroll)
        {
            AlwaysMove();
        }
    }

    // ========================================================================================================

    // ========================================================================================================

    // ========================================================================================================

    // ========================================================================================================

    // ========================================================================================================

    // ========================================================================================================

    // Move
    // Player object moves left or right based on horizontal input
    // Takes: Nothing
    // Returns: Nothing
    public void Move()
    {
        // Failsafe so that waypoints don't conflict with regular automovement
        if (movementType == MovementType.Waypoints)
        {
            // ======================================================================================
            // ENABLES 3D MOVEMENT WHILE LLAMA IS ONRAILS
            // ======================================================================================
            float hdir = Input.GetAxisRaw("Horizontal");
            float vdir = Input.GetAxisRaw("Vertical");
            rb.AddForce(new Vector3(hdir, 0, vdir) * movementSpeed);
            transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed, 0);   // Needs improvement!!!
            // ======================================================================================
            // ======================================================================================

            if (waypointIndex <= waypoints.Length - 1)
            {
                // Move player from current waypoint to next one
                transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, crawlSpeed * Time.deltaTime);

                // Cycle to next index of waypoint path if the player reaches one of the points
                if (transform.position == waypoints[waypointIndex].transform.position)
                {
                    waypointIndex++;
                }
            }
        }
        else
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
    }

    // Jump
    // Player hops into the air
    // Takes: Nothing
    // Returns: Nothing
    public void Jump()
    {
        // Check first if we're in single or chain jump mode
        if (disableChainJumps && player.GetComponent<Player>().IsGrounded)
        {
            //Debug.Log("Llama is airborne. isGrounded has been set to " + player.GetComponent<Player>().isGrounded);
            rb.AddForce(Vector3.up * jumpSpeed);

            // Technically not needed but for safety, this will prevent any further jumping until we land
            player.GetComponent<Player>().IsGrounded = false;
        }

        // Allow chain jumps otherwise
        if (!disableChainJumps)
        {
            rb.AddForce(Vector3.up * jumpSpeed);
        }
    }

    // AlwaysMove
    // Constantly moves the player right (slowly)
    // Takes: Nothing
    // Returns: Nothing
    public void AlwaysMove()
    {
        Vector3 spd = new Vector3(crawlSpeed, 0, 0);
        rb.AddForce(spd);
    }
}
