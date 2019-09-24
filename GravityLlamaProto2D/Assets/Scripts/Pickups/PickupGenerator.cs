/*
 * =============================================================================
 * 
 * [Gravity Llama]
 * Alpha
 * 
 * 
 * Script name:
 * PickupGenerator.cs
 * 
 * 
 * Date:
 * 12-09-2019
 * 
 * 
 * Description:
 * Randomly spawns and launches postive or negative gravity pickups in a specified direction
 * 
 * 
 * Parameters:
 * None
 * 
 * 
 * Attaches to:
 * Anything (preferably a dummy GameObject)
 * 
 * 
 * Dependencies:
 * None
 * 
 * 
 * Changelog:
 * 12-09    Initial
 * 22-09    Updated script to automatically delete uncollected pickups not long after spawning
 * 24-09    Fixed spawners not actually resetting to random seeds every time coroutine runs
 * 
 * =============================================================================
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGenerator : MonoBehaviour
{
    // ********************************************************************************************************

    [Tooltip("Drag any pickup prefabs that you wish to include as spawnable objects into this list.")]
    public List<GameObject> pickupsToSpawn = new List<GameObject>();

    private int prefabIndex;

    // ********************************************************************************************************

    void Start()
    {
        prefabIndex = UnityEngine.Random.Range(0, pickupsToSpawn.Count);
        StartCoroutine(SpawnRandomPickups());
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

    IEnumerator SpawnRandomPickups()
    {
        while (true)
        {
            GameObject pickup = Instantiate(pickupsToSpawn[prefabIndex], transform.position, transform.rotation);
            pickup.transform.parent = gameObject.transform;
            pickup.GetComponent<Rigidbody>().AddForce(Vector3.back * 4000f);

            // Wait for random amount of time before spawning again
            yield return new WaitForSeconds(UnityEngine.Random.Range(3, 5));

            // Randomise again
            prefabIndex = UnityEngine.Random.Range(0, pickupsToSpawn.Count);

            // Remove the previous pickup before spawning another if it's still "alive"
            if (pickup != null)
            {
                Destroy(pickup);
            }
        }
    }

    // ********************************************************************************************************
}
