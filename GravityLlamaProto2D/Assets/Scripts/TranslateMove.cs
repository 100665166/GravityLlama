using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TranslateMove : MonoBehaviour
{
    public double boundsLeft;
    public double boundsRight;
    public float turnSpeed = 0.75f;
    public float sideSpeed = 8f;


    // Start is called before the first frame update
    void Start()
    {
        boundsLeft = GameObject.Find("BoundsLeft").transform.position.x;
        boundsRight = GameObject.Find("BoundsRight").transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.localRotation + " locRot:Rot" + transform.rotation.eulerAngles);
        if (Input.GetAxisRaw("Horizontal")<0)
        {
            //limit the rotation left
            if (transform.rotation.eulerAngles.y >256)
            {
                transform.Rotate(new Vector3(0, -turnSpeed, 0));
            }
            //translate left
            if (transform.position.x > boundsLeft)
                transform.Translate(Vector3.left * sideSpeed * Time.deltaTime, Space.World);
        }
        else if (Input.GetAxisRaw("Horizontal") >0)
        {
            //limit the rotation left
            if (transform.rotation.eulerAngles.y < 284)
                transform.Rotate(new Vector3(0, turnSpeed, 0));
            if (transform.position.x < boundsRight)
                transform.Translate(Vector3.right * sideSpeed * Time.deltaTime, Space.World);
        }
    }
}
