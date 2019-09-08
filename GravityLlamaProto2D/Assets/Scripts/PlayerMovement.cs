/*
 * =============================================================================
 * 
 * [Gravity Llama]
 * Alpha
 * 
 * 
 * Script name:
 * PlayerMovement.cs
 * 
 * 
 * Date:
 * 08-09-2019
 * 
 * 
 * Description:
 * Player controller
 * Handles movement along lanes as well as switching/transitions
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
 * Player.cs
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

public class PlayerMovement : MonoBehaviour
{
    public enum Lanes
    {
        Left, 
        CentreLeft, 
        CentreRight, 
        Right
    }

    [Header("GENERAL")]
    [Tooltip("Which lane does the player start on? (make sure you've defined the lanes first)")]
    public Lanes startingLane;

    [Header("OPTIONS")]
    [Tooltip("How high can the player jump normally (affected by gravity)?")]
    public float jumpStrength = 750f;

    [Header("LANES")]
    [Tooltip("Drag the left lane's GameObject here.")]
    public GameObject leftLane;
    [Tooltip("Drag the centre-left lane's GameObject here.")]
    public GameObject centreLeftLane;
    [Tooltip("Drag the centre-right lane's GameObject here.")]
    public GameObject centreRightLane;
    [Tooltip("Drag the right lane's GameObject here.")]
    public GameObject rightLane;

    [HideInInspector]
    public Lanes currentLane; // What lane are we on right now
    [HideInInspector]
    public Rigidbody rb;
    [HideInInspector]
    public GameObject gm;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentLane = startingLane;

        try
        {
            gm = GameObject.FindGameObjectWithTag("EditorOnly");
        }
        catch (NullReferenceException)
        {
            Debug.Log("[PLAYERMOVEMENT.CS] No GameManager detected in the scene. Please add it first.");
        }
    }

    void Update()
    {
        rb.drag = gm.GetComponent<GravityLevel>().gravityLevel;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // Jump to right lane
        if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeLaneRight();
        }

        // Jump to left lane
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeLaneLeft();
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

    // Jump
    // Player leaps into the air via addForce
    // Takes: Nothing
    // Returns Nothing
    public void Jump()
    {
        if (GetComponent<Player>().isGrounded)
        {
            rb.AddForce(Vector3.up * jumpStrength);

            // Technically not needed but for safety, this will prevent any further jumping until we land
            GetComponent<Player>().isGrounded = false;
        }
    }

    // SwitchToLane
    // Changes to the desired lane
    // Takes: Lanes
    // Returns: Nothing
    public void SwitchToLane(Lanes lane)
    {
        // Make sure we're grounded first before attempting to jump to another
        if (GetComponent<Player>().isGrounded)
        {
            rb.AddForce(Vector3.up * (jumpStrength * 0.9f));   // We shouldn't be affected by gravity as much unlike regular jumps
            currentLane = lane;
            Debug.Log("Changing to the " + lane + " lane.");
        }
    }

    // ChangeLaneRight
    // Switches the player one lane right
    // Takes: Nothing
    // Returns: Nothing
    public void ChangeLaneRight()
    {
        switch (currentLane)
        {
            case Lanes.Left:
                SwitchToLane(Lanes.CentreLeft);
                //Debug.Log("On centre-left lane.");
                break;
            case Lanes.CentreLeft:
                SwitchToLane(Lanes.CentreRight);
                //Debug.Log("On centre-right lane.");
                break;
            case Lanes.CentreRight:
                SwitchToLane(Lanes.Right);
                //Debug.Log("On right lane.");
                break;
            case Lanes.Right:
                // Do not change lane
                //Debug.Log("At max right; can't switch!");
                break;
            default:
                break;
        }
    }

    // ChangeLaneLeft
    // Switches the player one lane left
    // Takes: Lanes
    // Returns: Nothing
    public void ChangeLaneLeft()
    {
        switch (currentLane)
        {
            case Lanes.Left:
                // Do nothing since we're at the edge
                //Debug.Log("At max left; can't switch!");
                break;
            case Lanes.CentreLeft:
                SwitchToLane(Lanes.Left);
                //Debug.Log("On left lane.");
                break;
            case Lanes.CentreRight:
                SwitchToLane(Lanes.CentreLeft);
                //Debug.Log("On centre-left lane.");
                break;
            case Lanes.Right:
                SwitchToLane(Lanes.CentreRight);
                //Debug.Log("On centre-right lane.");
                break;
            default:
                break;
        }
    }
}
