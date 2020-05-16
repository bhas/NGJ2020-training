using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WheelSetup
{
    public WheelCollider collider;
    public Transform model;
}

public class Car2 : MonoBehaviour
{

    public float maxMoterPower = 30f;
    public float maxSteeringAngle = 25f;
    private float inputX;
    private float inputY;

    public WheelSetup fl;
    public WheelSetup fr;
    public WheelSetup bl;
    public WheelSetup br;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        Steer();
        Accelerate();

        // update wheel poses
        UpdateWheelPose(fr);
        UpdateWheelPose(fl);
        UpdateWheelPose(br);
        UpdateWheelPose(bl);
    }


    private void Steer()
    {
        var steerAngle = maxSteeringAngle * inputX;
        fl.collider.steerAngle = steerAngle;
        fr.collider.steerAngle = steerAngle;
    }

    private void Accelerate()
    {
        var motorPower = maxMoterPower * inputY;
        fl.collider.motorTorque = motorPower;
        fr.collider.motorTorque = motorPower;
        bl.collider.motorTorque = motorPower;
        br.collider.motorTorque = motorPower;
    }

    private void UpdateWheelPose(WheelSetup wheelSetup)
    {
        wheelSetup.collider.GetWorldPose(out Vector3 pos, out Quaternion rotation);
        wheelSetup.model.position = pos;
        wheelSetup.model.rotation = rotation;
    }
}