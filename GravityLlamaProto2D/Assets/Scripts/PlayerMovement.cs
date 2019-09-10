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
 * 10-09    simple hard coded vector 3 changes and addforce to make the player move forward and jump between lanes.
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
    //created a dictionary to make use of the above enum to know the current Z values (horizontal movement)
    //used in the future to figure out ways to better calculated forces needed to reach the centre z locations of each lane
    static readonly Dictionary<Lanes, int> LaneZ = new Dictionary<Lanes, int>
    {
        {Lanes.Left , 0 },
        {Lanes.CentreLeft , -4 },
        {Lanes.CentreRight , -8 },
        {Lanes.Right , -12 }
    };

    [Header("GENERAL")]
    [Tooltip("Which lane does the player start on? (make sure you've defined the lanes first)")]
    public Lanes startingLane;

    [Header("OPTIONS")]
    [Tooltip("How high can the player jump normally (affected by gravity)?")]
    public float jumpStrength = 750f;
    //simple movingTo char for L and R to be decide +- z values
    public char movingTo;

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
    //primary moveVector to be used to apply force to the player
    public Vector3 moveVector;

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
    /// <summary>
    /// FixedUpdate for updates per frame for movement.
    /// having player.isGrounded to addforce as normal
    /// else move as normal but in the air reset moveVector to the normal
    /// scrolling movement vector (1,0,0)
    /// </summary>
    private void FixedUpdate()
    {
        //if grounded default forward movement
        if (GetComponent<Player>().isGrounded)
        {
            rb.AddForce(moveVector * 50);
        }
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
            currentLane = lane;
            //MoveLanes funciton moved to keep it tidy
            MoveLanes();
            Debug.Log("Changing to the " + currentLane + " lane.");
        }
    }
    /// <summary>
    /// Main lane Moving method hard coded new Vector3() values atm will work on getting the math to make things smoother towards beta
    /// 
    /// </summary>
    public void MoveLanes()
    {
        Debug.Log("MovingLanes1 :" + movingTo);
        if (movingTo == 'R' || movingTo == 'L')
        {

            Debug.Log("MovingLanes :"+movingTo);
            switch (movingTo)
            {
                case 'R':
                    moveVector = new Vector3(1, 10, -11.1f);
                    movingTo = 'N';
                    break;
                case 'L':
                    moveVector = new Vector3(1, 10, 11.1f);
                    movingTo = 'N';
                    break;
            }
        }
    }
    // ChangeLaneRight
    // Switches the player one lane right
    // Takes: Nothing
    // Returns: Nothing
    // added movingTo to be set in this method
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
    //add movingTo to be set in this method
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
}
