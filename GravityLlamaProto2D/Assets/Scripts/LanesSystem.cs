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
 * Player.cs
 * LaneMagnet.cs
 * 
 * 
 * Changelog:
 * 11-09    Initial
 * 18-09    MoveToLane now uses proper physics-based transitions
 * 22-09    Incorporates LaneMagnet functionality
 * 24-09    Minor adjusting to debug output for clarity
 * 07-10    Player can now swap between lanes while A/D are held down
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
    private LaneMagnet lm;
    public float speed = 50;
    double wpRadius = .02;
    double wpReset = .0002;
    int currentWp = 0;
    public List<Vector3> waypoints;

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

        // -------------------------------------------------------------------------------------------
        // START
        // This entire block is devoted to making sure the enums are defined from the get-go
        if (currentLane == leftLane)
        {
            cLane = Lanes.left;
            lm = currentLane.GetComponent<LaneMagnet>();
        }
        if (currentLane == centreLeftLane)
        {
            cLane = Lanes.centreLeft;
            lm = currentLane.GetComponent<LaneMagnet>();
        }
        if (currentLane == centreRightLane)
        {
            cLane = Lanes.centreRight;
            lm = currentLane.GetComponent<LaneMagnet>();
        }
        if (currentLane == rightLane)
        {
            cLane = Lanes.right;
            lm = currentLane.GetComponent<LaneMagnet>();
        }
        // END
        // -------------------------------------------------------------------------------------------

        // Make the starting lane "active" so that the llama gets pulled to it
        lm.IsActive = true;

        try
        {
            gm = GameObject.FindGameObjectWithTag("GameManager");
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
        lm = currentLane.GetComponent<LaneMagnet>();

        //// Jump to right lane
        //if (Input.GetKeyDown(KeyCode.D))
        //{
        //    //Debug.Log("D down:");
        //   //Debug.Log("Clane:" + cLane + " currentLane:" + currentLane);
        //    ChangeLane('R');
        //    //Debug.Log("Clane:" + cLane + " currentLane:" + currentLane);
        //}

        //// Jump to left lane
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    //Debug.Log("A down:");
        //    //Debug.Log("Clane:" + cLane + " currentLane:" + currentLane);
        //    ChangeLane('L');
        //    //Debug.Log("Clane:" + cLane + " currentLane:" + currentLane);
        //}

        //trying to allow for held down presses
        //using if (Mathf.Abs(currentLane.transform.position.x - player.transform.position.x) < x) to prevent holding down keys
        //to continuely add force in changelane addforce making u jump beyond the next lane.
        //isChangingLane bool seems to not be preventing this.
        //Debug.Log("!isChangingLaneUpdate");
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            //Debug.Log("Horizontal>0=" + Mathf.Abs(Math.Abs(currentLane.transform.position.x) - Mathf.Abs(player.transform.position.x)));
            if (Mathf.Abs(Math.Abs(currentLane.transform.position.x) - Mathf.Abs(player.transform.position.x)) < .3)
            {
                ChangeLane('R');
                //Debug.Log(waypoints.Count);
                //Debug.Log("Update X:X close enough");
            }
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            //Debug.Log("Horizontal<0="+ Mathf.Abs(Math.Abs(currentLane.transform.position.x) - Mathf.Abs(player.transform.position.x)));
            if (Mathf.Abs(Math.Abs(currentLane.transform.position.x) - Mathf.Abs(player.transform.position.x)) < .3)
            {
               //Debug.Log(waypoints.Count);
                ChangeLane('L');
                //Debug.Log("Update X:X close enough");
            }
        }
        //reset player velocity when x reaches lane x
        //if (Mathf.Abs(currentLane.transform.position.x - player.transform.position.x) < .2)
        //{
        //    Debug.Log("Reset rb vvelocity: player x is close to lane x" + Mathf.Abs(currentLane.transform.position.x - player.transform.position.x));
        //    //rb.velocity = Vector3.zero;
        //}
        if (waypoints.Count> 0)
        {
            TranslateWaypoint();
        }
    }

    // ********************************************************************************************************

    void FixedUpdate()
    {
        // We need this to be constantly running so that the llama "sticks" to the lane
        //MoveToLane(currentLane);
    }

    // ********************************************************************************************************

    // ChangeLane
    // Llama switches to a lane depending on their current location (physical relocation is handled by MoveToLane)
    // Takes: char
    // Returns: Nothing
    public void ChangeLane(char direction)
    {
        // Cannot already be changing lane AND must not be mid-air
        if (!IsChangingLane && player.IsGrounded)
        {
            Debug.Log("Llama is now on the <color=red>" + cLane + "</color> lane" + " Direction:"+direction);
            switch (direction)
            {
                case 'L':   // For moving to lanes left of the player's current position
                    switch (cLane)
                    {
                        // TODO: shift case actions to a dedicated function with parameters?
                        case Lanes.left:
                            break;
                        case Lanes.centreLeft:
                            //rb.AddForce(Vector3.up * player.GetJumpStrength);
                            GetWaypoints("LCLarc",direction);
                            currentLane = leftLane;
                            //waypoints.Add(GameObject.Find("LCLarc"));
                            //waypoints.Add(GameObject.Find("LeftLane"));
                            //Debug.Log("ChangeLanewp:" + waypoints[0] + " : " + waypoints[1]);
                            cLane = Lanes.left;
                            break;
                        case Lanes.centreRight:
                            // rb.AddForce(Vector3.up * player.GetJumpStrength);
                            GetWaypoints("CLCRarc", direction);
                            currentLane = centreLeftLane;
                            cLane = Lanes.centreLeft;
                            //waypoints.Add(GameObject.Find("CLCRarc"));
                            //waypoints.Add(GameObject.Find("CentreLeftLane"));
                            //Debug.Log("ChangeLanewp:" + waypoints[0] + " : " + waypoints[1]);
                            break;
                        case Lanes.right:
                            //rb.AddForce(Vector3.up * player.GetJumpStrength);
                            GetWaypoints("CRRarc", direction);
                            currentLane = centreRightLane;
                            cLane = Lanes.centreRight;
                            //waypoints.Add(GameObject.Find("CRRarc"));
                            //waypoints.Add(GameObject.Find("CentreRightLane"));
                            //Debug.Log("ChangeLanewp:" + waypoints[0] + " : " + waypoints[1]);
                            break;
                        default:
                            break;
                    }
                    Debug.Log("Llama is now on the <color=blue>" + cLane + "</color> lane");
                    break;
                case 'R':   // For moving to lanes right of the player's current position
                    switch (cLane)
                    {
                        case Lanes.left:
                            //rb.AddForce(Vector3.up * player.GetJumpStrength);
                            GetWaypoints("LCLarc", direction);
                            currentLane = centreLeftLane;
                            cLane = Lanes.centreLeft;
                            //waypoints.Add(GameObject.Find("LCLarc"));
                            //waypoints.Add(GameObject.Find("CentreLeftLane"));
                            //Debug.Log("ChangeLanewp:" + waypoints[0] + " : " + waypoints[1]);
                            break;
                        case Lanes.centreLeft:
                            //rb.AddForce(Vector3.up * player.GetJumpStrength);
                            GetWaypoints("CLCRarc", direction);
                            currentLane = centreRightLane;
                            cLane = Lanes.centreRight;
                            //waypoints.Add(GameObject.Find("CLCRarc"));
                            //waypoints.Add(GameObject.Find("CentreRightLane"));
                            //Debug.Log("ChangeLanewp:" + waypoints[0] + " : " + waypoints[1]);
                            break;
                        case Lanes.centreRight:
                            //rb.AddForce(Vector3.up * player.GetJumpStrength);
                            GetWaypoints("CRRarc", direction);
                            currentLane = rightLane;
                            cLane = Lanes.right;
                            //waypoints.Add(GameObject.Find("CRRarc"));
                            //waypoints.Add(GameObject.Find("RightLane"));
                            //Debug.Log("ChangeLanewp:" + waypoints[0] + " : " + waypoints[1]);
                            break;
                        case Lanes.right:
                            break;
                        default:
                            break;
                    }
                    Debug.Log("Llama is now on the <color=blue>" + cLane + "</color> lane");
                    break;
                default:
                    break;
            }
        }
        if (player.IsJumping)
        {
            switch (direction)
            {
                case 'L':   // For moving to lanes left of the player's current position
                    switch (cLane)
                    {
                        // TODO: shift case actions to a dedicated function with parameters?
                        case Lanes.left:
                            break;
                        case Lanes.centreLeft:
                            rb.AddForce(Vector3.up * player.GetJumpStrength);
                            currentLane = leftLane;
                            cLane = Lanes.left;
                            break;
                        case Lanes.centreRight:
                            rb.AddForce(Vector3.up * player.GetJumpStrength);
                            currentLane = centreLeftLane;
                            cLane = Lanes.centreLeft;
                            break;
                        case Lanes.right:
                            rb.AddForce(Vector3.up * player.GetJumpStrength);
                            currentLane = centreRightLane;
                            cLane = Lanes.centreRight;
                            break;
                        default:
                            break;
                    }
                    Debug.Log("Llama is now on the <color=blue>" + cLane + "</color> lane");
                    break;
                case 'R':   // For moving to lanes right of the player's current position
                    switch (cLane)
                    {
                        case Lanes.left:
                            rb.AddForce(Vector3.up * player.GetJumpStrength);
                            currentLane = centreLeftLane;
                            cLane = Lanes.centreLeft;
                            break;
                        case Lanes.centreLeft:
                            rb.AddForce(Vector3.up * player.GetJumpStrength);
                            currentLane = centreRightLane;
                            cLane = Lanes.centreRight;
                            break;
                        case Lanes.centreRight:
                            rb.AddForce(Vector3.up * player.GetJumpStrength);
                            currentLane = rightLane;
                            cLane = Lanes.right;
                            break;
                        case Lanes.right:
                            break;
                        default:
                            break;
                    }
                    Debug.Log("Llama is now on the <color=blue>" + cLane + "</color> lane");
                    break;
                default:
                    break;
            }
        }
    }

    public void GetWaypoints(string pArc, char pDir)
    {
        //grab the arc object that contains childs 1-5 waypoints
        //L = 1-5 , R = 5-1
        GameObject arc = GameObject.Find(pArc);
        foreach (Transform child in arc.transform)
        {
            waypoints.Add(child.transform.position);
        }
        if (pDir == 'L')
        {
            waypoints.Reverse();
        }
        Debug.Log("GetWayPoints:\n");
        foreach( Transform child in arc.transform)
        {
            Debug.Log(child +" : "+ child.transform.position);
        }
    }
    /// <summary>
    /// waypoint translation lane movement
    /// </summary>
    public void TranslateWaypoint()
    {
        ////This segment used for GameObjects
        //if (waypoints.Count>0)
        //{
        //    Debug.Log(waypoints[0] + "   :    " + waypoints[1] + "distance:"+ Vector3.Distance(waypoints[currentWp].transform.position, player.transform.position));
        //    if (Vector3.Distance(waypoints[currentWp].transform.position, player.transform.position) < wpRadius)
        //    {
        //        Debug.Log("waypoint ++");
        //        currentWp++;
        //        if (currentWp >= waypoints.Count)
        //        {
        //            currentWp = 0;
        //            Debug.Log("resetWP:" + currentWp);
        //        }
        //    }
        //    player.transform.position = Vector3.MoveTowards(player.transform.position, waypoints[currentWp].transform.position, Time.deltaTime * speed);
        //    rb.MovePosition(waypoints[currentWp].transform.position);
        //    if (Vector3.Distance(waypoints[waypoints.Count-1].transform.position, player.transform.position) < wpReset)
        //    {
        //        waypoints.Clear();
        //        Debug.Log("waypoint.Clear" + "\n count is now:" + waypoints.Count);
        //    }
        //}
        //using game object child's transform
        if (waypoints.Count > 0)
        {
            //Debug.Log(waypoints[0] + "   :    " + waypoints[1] + "distance:" + Vector3.Distance(waypoints[currentWp], player.transform.position));
            Debug.Log("player.pos:" + player.transform.position + "\nHeadingToWp"+currentWp+":"+waypoints[currentWp]);
            if (Vector3.Distance(waypoints[currentWp], player.transform.position) < wpRadius)
            {
                Debug.Log(currentWp+" waypoint ++"+ waypoints.Count);
                currentWp++;
                if (currentWp >= waypoints.Count)
                {
                    currentWp = 0;
                    Debug.Log("resetWP:" + currentWp);
                }
            }
            player.transform.position = Vector3.MoveTowards(player.transform.position, waypoints[currentWp], Time.deltaTime * speed);
            rb.MovePosition(waypoints[currentWp]);
            if (Vector3.Distance(waypoints[waypoints.Count - 1], player.transform.position) < wpReset)
            {
                waypoints.Clear();
                currentWp = 0;
                Debug.Log("waypoint.Clear" + "\n count is now:" + waypoints.Count +"\ncurrentWp:"+currentWp);
            }
        }
    }

    // ********************************************************************************************************

    // MoveToLane
    // Physically relocates the llama to an active lane by pulling them towards it like a magnet
    // Takes: GameObject
    // Returns: Nothing
    public void MoveToLane(GameObject lane)
    {
        //Debug.Log("Clane:" + cLane + " currentLane:" + currentLane);
        //Debug.Log("MoveToLane:" + lane.transform.position.x + " player po x:" + player.transform.position.x);
        //Debug.Log("rb velocity move to lane:" + rb.velocity.ToString());
        // This validator is only needed if gravity is too high so that the llama transitions without getting stuck
        if (gm.GetComponent<GravityLevel>().SetGravityLevel > 5)
        {
            // Vegeta would be proud
            rb.AddForce((lane.transform.position - transform.position) * 9000f * Time.smoothDeltaTime);
        }
        else
        {
            rb.AddForce((lane.transform.position - transform.position) * 1000f * Time.smoothDeltaTime);
        }
        //Debug.Log("Llama is being pulled to the " + lm + " lane.");

        // -------------------------------------------------------------------------------------------
        // DEPRECATED: For testing purposes only; replace this entire function's contents with proper RigidBody-based vector movement
        //transform.position = currentLane.transform.position;
        //rb.MovePosition(currentLane.transform.position);
        // -------------------------------------------------------------------------------------------
    }

    // ********************************************************************************************************
}
