/*
 * =============================================================================
 * 
 * [Gravity Llama]
 * Alpha
 * 
 * 
 * Script name:
 * LanesSystem.cs
 * 
 * 
 * Date:
 * 11-09-2019
 * 
 * 
 * Description:
 * Controls player's movement
 * Enables them to "switch" lanes during gameplay
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
 * None
 * 
 * 
 * Changelog:
 * 11-09    Initial
 * 
 * =============================================================================
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanesSystem : MonoBehaviour
{
    // ********************************************************************************************************

    // A note about these enums: they DO NOT represent the actual lane locations
    public enum Lanes
    {
        left, 
        centreLeft, 
        centreRight, 
        right
    }

    // ********************************************************************************************************

    [Header("LANES")]
    [Tooltip("Drag the left lane's waypoint node here.")]
    public GameObject leftLane;
    [Tooltip("Drag the centre-left lane's waypoint node here.")]
    public GameObject centreLeftLane;
    [Tooltip("Drag the centre-right lane's waypoint node here.")]
    public GameObject centreRightLane;
    [Tooltip("Drag the right lane's waypoint node here.")]
    public GameObject rightLane;

    [Header("OPTIONS")]
    [Tooltip("Which lane does the player start the level on? Drag that lane's GameObject here too.")]
    public GameObject startingLane;

    private GameObject currentLane; // Which lane are we currently riding on?
    private Lanes cLane;    // Ditto, but just for tracking the Lanes enum
    private bool isChangingLane = false;

    private GameObject gm;
    private Player player;
    private Rigidbody rb;

    // ********************************************************************************************************

    public bool IsChangingLane { get => isChangingLane; set => isChangingLane = value; }

    // ********************************************************************************************************

    void Start()
    {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody>();

        // We need to automove the player straight to the chosen starting lane upon the level's start
        currentLane = startingLane;
        transform.position = currentLane.transform.position;
        // Don't forget their RigidBody too
        rb.MovePosition(currentLane.transform.position);

        // This entire block is devoted to making sure the enums are defined from the get-go
        if (currentLane == leftLane)
        {
            cLane = Lanes.left;
        }
        if (currentLane == centreLeftLane)
        {
            cLane = Lanes.centreLeft;
        }
        if (currentLane == centreRightLane)
        {
            cLane = Lanes.centreRight;
        }
        if (currentLane == rightLane)
        {
            cLane = Lanes.right;
        }

        try
        {
            gm = GameObject.FindGameObjectWithTag("EditorOnly");
        }
        catch (NullReferenceException)
        {
            Debug.Log("[LANESSYSTEM.CS] No GameManager detected within the scene. Please add the prefab to the scene or create one and add GravityLevel.cs to it.");
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

    void Update()
    {
        rb.drag = gm.GetComponent<GravityLevel>().SetGravityLevel;

        // Jump to right lane
        if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeLane('R');
        }

        // Jump to left lane
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeLane('L');
        }
    }

    // ********************************************************************************************************

    // ChangeLane
    // Llama switches to a lane depending on their current location (physical relocation is handled by MoveToLane)
    // Takes: char
    // Returns: Nothing
    public void ChangeLane(char direction)
    {
        if (!IsChangingLane)
        {
            switch (direction)
            {
                case 'L':   // For moving to lanes left of the player's current position
                    switch (cLane)
                    {
                        case Lanes.left:
                            break;
                        case Lanes.centreLeft:
                            currentLane = leftLane;
                            cLane = Lanes.left;
                            MoveToLane();
                            break;
                        case Lanes.centreRight:
                            currentLane = centreLeftLane;
                            cLane = Lanes.centreLeft;
                            MoveToLane();
                            break;
                        case Lanes.right:
                            currentLane = centreRightLane;
                            cLane = Lanes.centreRight;
                            MoveToLane();
                            break;
                        default:
                            break;
                    }
                    Debug.Log("Llama is now on the " + cLane + " lane");
                    break;
                case 'R':   // For moving to lanes right of the player's current position
                    switch (cLane)
                    {
                        case Lanes.left:
                            currentLane = centreLeftLane;
                            cLane = Lanes.centreLeft;
                            MoveToLane();
                            break;
                        case Lanes.centreLeft:
                            currentLane = centreRightLane;
                            cLane = Lanes.centreRight;
                            MoveToLane();
                            break;
                        case Lanes.centreRight:
                            currentLane = rightLane;
                            cLane = Lanes.right;
                            MoveToLane();
                            break;
                        case Lanes.right:
                            break;
                        default:
                            break;
                    }
                    Debug.Log("Llama is now on the " + cLane + " lane");
                    break;
                default:
                    break;
            }
        }
    }

    // ********************************************************************************************************

    // MoveToLane
    // This physically relocates the llama to its designated lane
    // Takes: Transform
    // Returns: Nothing
    public void MoveToLane()
    {
        // For testing purposes only; replace this entire function's contents with proper RigidBody-based vector movement
        transform.position = currentLane.transform.position;
        rb.MovePosition(currentLane.transform.position);
    }

    // ********************************************************************************************************
}
