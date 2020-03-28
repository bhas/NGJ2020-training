using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car2 : MonoBehaviour
{
    public WheelCollider fl;
    public WheelCollider fr;
    public WheelCollider bl;
    public WheelCollider br;
    public float motorPower = 30f;
    public float steerAngle = 25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var dx = Input.GetAxis("Horizontal");
        var dy = Input.GetAxis("Vertical");

        if (dy != 0)
        {
            bl.motorTorque = dy * motorPower;
            br.motorTorque = dy * motorPower;
        }

        if (dx != 0)
        {
            fl.steerAngle = dx * steerAngle;
            fr.steerAngle = dx * steerAngle;
        }

    }
}
