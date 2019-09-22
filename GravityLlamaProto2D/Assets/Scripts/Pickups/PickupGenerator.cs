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
            pickup.AddComponent<Rigidbody>();
            pickup.AddComponent<Despawner>();
            pickup.transform.Rotate(0, 0, 90);
            pickup.GetComponent<Rigidbody>().AddForce(Vector3.back * 4000f);
            yield return new WaitForSeconds(UnityEngine.Random.Range(2, 6));
        }
    }

    // ********************************************************************************************************
}
