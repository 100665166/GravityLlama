using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStabliser : MonoBehaviour
{
    public GameObject player;
    public float playerX;
    public float playerY;
    public float playerZ;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void LateUpdate()
    {
        playerX = player.transform.eulerAngles.x;
        //we want the y axis 0 
        playerY = 0;
        playerZ = player.transform.eulerAngles.z;
        //counter the rotation to keep the cube fixed
        transform.eulerAngles = new Vector3(playerX , playerY,playerZ);
    }
}
