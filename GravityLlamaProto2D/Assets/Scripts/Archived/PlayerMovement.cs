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
 * 10-09    Added movement based on fixed Z value
 * 11-09    Added temporary fix to prevent player falling off lanes
 * 11-09    Archived (still retained for backwards compatibility)
 * 
 * =============================================================================
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    void Reset()
    {
        hideFlags = HideFlags.HideInInspector;
    }

    public enum Lanes
    {
        Left,
        CentreLeft,
        CentreRight,
        Right
    }
    //created a dictionary to make use of the above enum to know the current Z values (horizontal movement)
    static readonly Dictionary<Lanes, double> LaneZ = new Dictionary<Lanes, double>
    {
        {Lanes.Left , 4.5 },
        {Lanes.CentreLeft , 0 },
        {Lanes.CentreRight , -4.5 },
        {Lanes.Right , -9 }
    };

    [Header("GENERAL")]
    [HideInInspector]
    [Tooltip("Which lane does the player start on? (make sure you've defined the lanes first)")]
    public Lanes startingLane;  // Temporarily disabled for now

    [Header("OPTIONS")]
    [Tooltip("How high can the player jump normally (affected by gravity)?")]
    public float jumpStrength = 750f;
    [HideInInspector]
    public char movingTo;   // Which Side is the llama hopping to Left/Right for +-Z values in addforce vector

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
    [HideInInspector]
    public Vector3 moveVector;
    [HideInInspector]
    public bool isChangingLane = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentLane = startingLane;
        
        try
        {
            gm = GameObject.FindGameObjectWithTag("GameManager");
        }
        catch (NullReferenceException)
        {
            Debug.Log("[PLAYERMOVEMENT.CS] No GameManager detected in the scene. Please add it first.");
        }
    }

    void Update()
    {
        rb.drag = gm.GetComponent<GravityLevel>().SetGravityLevel;

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

    private void FixedUpdate()
    {
        //if grounded default forward movement
        if (GetComponent<Player>().IsGrounded)
        {
            rb.AddForce(moveVector * 50);
        }
        //this else used to reset moveVector back after lane switch hop.
        else
        {
            moveVector = new Vector3(1, 0, 0);
            rb.AddForce(moveVector * 50);
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
        if (GetComponent<Player>().IsGrounded && !isChangingLane)
        {
            rb.AddForce(Vector3.up * jumpStrength);

            // Technically not needed but for safety, this will prevent any further jumping until we land
            GetComponent<Player>().IsGrounded = false;
        }
    }

    // SwitchToLane
    // Changes to the desired lane, temporarily fixing velocity and reverting upon landing
    // Takes: Lanes
    // Returns: Nothing
    public void SwitchToLane(Lanes lane)
    {
        // Make sure we're grounded first before attempting to jump to another
        if (GetComponent<Player>().IsGrounded)
        {
            isChangingLane = true;
            currentLane = lane;

            // Dump velocity so that we don't fly off the lanes
            //rb.velocity = new Vector3(0, 10, 0);
            //rb.angularVelocity = new Vector3(0, 10, 0);
            MoveLanes();
            //Debug.Log("Changing to the " + currentLane + " lane.");
        }
    }
    public void MoveLanes()
    {
        //Debug.Log("MovingLanes1 :" + movingTo);
        if (movingTo == 'R' || movingTo == 'L')
        {
            //Debug.Log("MovingLanes :"+movingTo);
            switch (movingTo)
            {
                case 'R':
                    moveVector = new Vector3(1, 5f, -14f);
                    movingTo = 'N';
                    break;
                case 'L':
                    moveVector = new Vector3(1, 5f, 14f);
                    movingTo = 'N';
                    break;
            }
        }
    }
    // ChangeLaneRight
    // Switches the player one lane right
    // Takes: Nothing
    // Returns: Nothing
    public void ChangeLaneRight()
    {
        movingTo = 'R';
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
        movingTo = 'L';
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

    // ResetVelocity
    // Resets the player's original velocity settings after changing lanes
    // Takes: Nothing
    // Returns: Nothing
    public void ResetVelocity()
    {
        isChangingLane = false;
    }

}
