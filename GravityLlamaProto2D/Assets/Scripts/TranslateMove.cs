using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TranslateMove : MonoBehaviour
{
    public double boundsLeft;
    public double boundsRight;


    // Start is called before the first frame update
    void Start()
    {
        boundsLeft = GameObject.Find("BoundsLeft").transform.position.x;
        boundsRight = GameObject.Find("BoundsRight").transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal")<0)
        {
            //transform.Rotate(new Vector3(0, -.5f, 0));
            if (transform.position.x > boundsLeft)
                transform.Translate(Vector3.left * 5 * Time.deltaTime, Space.World);
        }
        else if (Input.GetAxisRaw("Horizontal") >0)
        {
            //transform.Rotate(new Vector3(0, .5f, 0));
            if (transform.position.x < boundsRight)
                transform.Translate(Vector3.right * 5 * Time.deltaTime, Space.World);
        }
    }
}
