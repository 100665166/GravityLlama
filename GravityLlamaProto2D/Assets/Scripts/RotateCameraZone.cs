/*
 * =============================================================================
 * 
 * [Gravity Llama]
 * Alpha
 * 
 * 
 * Script name:
 * RotateCameraZone.cs
 * 
 * 
 * Date:
 * 19-10-2019
 * 
 * 
 * Description:
 * Rotates the player's camera to match incline of the level terrain
 * 
 * 
 * Parameters:
 * None
 * 
 * 
 * Attaches to:
 * Any GameObject with a Collider trigger
 * 
 * 
 * Dependencies:
 * None
 * 
 * 
 * Changelog:
 * 21-10    Initial
 * 29-10    Shifted dragging functionality to GravityTriggerZone
 * 30-10    Added range limits to prevent camera from clipping
 * 
 * =============================================================================
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraZone : MonoBehaviour
{
    [Tooltip("What angle should the camera rotate to?\n\nNOTE:\nNegative angles = camera swivels up\nPositive angles = camera swivels down")]
    [Range(-12, 12)]    // Should never be more than 12...
    public float angle = 0f;

    [Tooltip("How long should the camera take to smooth itself? (shouldn't be higher than 1.0 or less than 0.5 for best results)")]
    public float cameraTransitionTime = 1f;

    [Tooltip("How much does the player's jump get boosted? (to accomodate for height slopes)")]
    public float playerNewJumpStrength = 5000f;

    // For the MainCamera
    private GameObject cam;

    // ********************************************************************************************************

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // ********************************************************************************************************

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position, 25f);
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

    void OnTriggerEnter(Collider col)
    {
        // Are we the player?
        if (col.CompareTag("Player"))
        {
            //cam.transform.Rotate(new Vector3(angle, 0, 0));
            StartCoroutine(CameraTransition(Vector3.right * angle, cameraTransitionTime));
            col.GetComponent<Player>().SetJumpStrength = playerNewJumpStrength;
        }
    }

    // ********************************************************************************************************

    // CameraTransition
    // Smoothly rotates the camera to angle specified by trigger box
    // Takes: Vector3, float
    // Returns: Nothing
    IEnumerator CameraTransition(Vector3 angle, float duration)
    {
        var fromAngle = cam.transform.rotation;
        var toAngle = Quaternion.Euler(cam.transform.eulerAngles + angle);
        for (var t = 0f; t < 1; t += Time.deltaTime / duration)
        {
            // TODO: Something better perhaps?
            cam.transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);

            // Stop rotating once we reach angle/time limit
            yield return null;
        }
    }
}
